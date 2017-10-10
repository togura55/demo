using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

using BaXterX;
using Windows.UI.Xaml.Shapes;
using Windows.Storage;
using Windows.Storage.Pickers;
using Windows.UI.Xaml.Media.Imaging;
using Windows.Graphics.Imaging;
using Windows.Graphics.Display;

// 空白ページのアイテム テンプレートについては、http://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409 を参照してください

namespace ShowHandwritingSample
{
    /// <summary>
    /// それ自体で使用できる空白ページまたはフレーム内に移動できる空白ページ。
    /// </summary>
    public sealed partial class MainPage : Windows.UI.Xaml.Controls.Page
    {
        //手書きデータの読み込みを行うファイル名
        private string mHandwriting = @"CLB Paper Sample.pdf";

        //BaXterのfieldデータ
        private List<Field> mFields;

        //データの全文字列格納用
        private List<string> mDataString = new List<string>();

        //fieldデータ表示フラグ
        private bool mfShowFields;

        //描画しているデータの幅、高さ
        private int mDataWidth = 0;
        private int mDataHeight = 0;

        //筆圧の最大値
        private double mMaxW = 1023;

        //コンストラクター
        public MainPage()
        {
            this.InitializeComponent();
            
            //pdfファイルデータの読み込み
            StreamReader stream = new StreamReader(File.OpenRead(this.mHandwriting));
            this.SetFileData(stream);
        }

        //手書きデータのコンボボックスのイベント
        private void ChangeFieldData(object sender, object e)
        {
            //field表示フラグが下がっていたら処理を抜ける
            if (!this.mfShowFields) return;

            //表示するfieldデータの設定
            this.SetFieldData(this.FieldList.SelectedItem.ToString());
        }

        //ファイル選択ボタンのイベント
        private async void FileButton_Click(object sender, RoutedEventArgs e)
        {
            var picker = new Windows.Storage.Pickers.FileOpenPicker();
            picker.ViewMode = Windows.Storage.Pickers.PickerViewMode.Thumbnail;
            picker.SuggestedStartLocation = Windows.Storage.Pickers.PickerLocationId.PicturesLibrary;
            picker.FileTypeFilter.Add(".pdf");

            StorageFile file = await picker.PickSingleFileAsync();

            if (file != null)
            {
                //pdfファイルが読み込まれた場合、pdfを表示
                if (file.Name.IndexOf(@".pdf") != -1 || file.Name.IndexOf(@".PDF") != -1)
                {
                    //pdfファイルデータの読み込み
                    StreamReader stream = new StreamReader(await file.OpenStreamForReadAsync());
                    this.SetFileData(stream);
                }
            }
        }

        //手書きデータの画像保存ボタンのイベント
        private async void SaveCanvas_Click(object sender, RoutedEventArgs e)
        {
            //field表示フラグが下がっていたら処理を抜ける
            if (!this.mfShowFields) return;
            
            //ペンデータが空なら処理を抜ける
            int fieldNo = this.GetFieldId(this.FieldList.SelectedItem.ToString());   //表示するfieldの番号
            if (this.mFields[fieldNo].penData.Count == 0) return;

            var picker = new FileSavePicker();
            picker.FileTypeChoices.Add("png", new List<string> { ".png" });
            var file = await picker.PickSaveFileAsync();
            if (file == null)   return;

            var size = this.HandwritingCanvas.RenderSize;
            var renderTargetBitmap = new RenderTargetBitmap();
            await renderTargetBitmap.RenderAsync(this.HandwritingCanvas, this.mDataWidth, this.mDataHeight);

            var displayInformation = DisplayInformation.GetForCurrentView();

            using (var s = await file.OpenAsync(FileAccessMode.ReadWrite))
            {
                var encoder = await BitmapEncoder.CreateAsync(BitmapEncoder.PngEncoderId, s);
                encoder.SetPixelData(
                    BitmapPixelFormat.Bgra8,
                    BitmapAlphaMode.Premultiplied,
                    (uint)renderTargetBitmap.PixelWidth,
                    (uint)renderTargetBitmap.PixelHeight,
                    displayInformation.LogicalDpi,
                    displayInformation.LogicalDpi,
                    (await renderTargetBitmap.GetPixelsAsync()).ToArray());
                await encoder.FlushAsync();
            }
        }

        //データ一覧で右クリックが押されたとき
        private void FieldData_RightTapped(object sender, RightTappedRoutedEventArgs e)
        {
            //右クリックが押された個所を選択状態にする
            this.FieldData.SelectedIndex = (int)((ListViewItem)sender).Tag;
        }

        //右クリック時のコピーが押された時
        private void DataCopy_Click(object sender, RoutedEventArgs e)
        {
            //クリップボードに入れる文字列の設定
            string text = this.mDataString[this.FieldData.SelectedIndex];

            //改行コードの変更
            text = text.Replace("\n", "\r\n");

            //クリップボードに文字列を入れる
            var dp = new Windows.ApplicationModel.DataTransfer.DataPackage();
            dp.SetText(text);
            Windows.ApplicationModel.DataTransfer.Clipboard.SetContent(dp);
        }

        //表示する手書きデータの設定
        //引数:表示を行うStorageFile
        private void SetFileData(StreamReader stream)
        {
            try
            {
                Reader reader = new Reader();
                reader.readFromStream(stream.BaseStream);
                var page = reader.document.pages[0];

                //fieldデータの設定
                this.mFields = page.fields;

                //fieldデータ表示フラグの設定
                this.mfShowFields = true;
            }
            catch(Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);

                //fieldデータ表示フラグの設定
                this.mfShowFields = false;
            }

            if (this.mfShowFields)
            {
                //コンボボックスの項目の削除
                this.FieldList.Items.Clear();

                //コンボボックスの設定
                for (int i = 0; i < this.mFields.Count; i++)
                {
                    this.FieldList.Items.Add(this.mFields[i].UUID);
                }
                this.FieldList.SelectedIndex = 0;

                //表示するfieldデータの設定
                this.SetFieldData(this.mFields[0].UUID);
            }
            else
            {
                //コンボボックスの項目の削除
                this.FieldList.Items.Clear();

                //データ一覧表の削除
                this.DeleteFieldData();
                this.FieldData.Items.Add(this.CreateFieldDataItem("手書きデータが存在していませんでした"));

                //キャンバス内の削除
                this.HandwritingCanvas.Children.Clear();
            }
        }

        //表示するfieldデータの設定
        //引数:表示を行うデータのUUID
        private void SetFieldData(String uuid)
        {
            int fieldNo = this.GetFieldId(uuid);   //表示するfieldの番号

            //UUIDが一致していなかったら処理を抜ける
            if (fieldNo == -1) return;

            //データ一覧表の設定
            this.DeleteFieldData();

            this.FieldData.Items.Add(this.CreateFieldDataItem(this.mFields[fieldNo].UUID, "UUID"));
            this.FieldData.Items.Add(this.CreateFieldDataItem(this.mFields[fieldNo].completionTime, "completionTime"));
            this.FieldData.Items.Add(this.CreateFieldDataItem(this.mFields[fieldNo].data, "data"));
            this.FieldData.Items.Add(this.CreateFieldDataItem(this.mFields[fieldNo].encrypted.ToString(), "encrypted"));
            this.FieldData.Items.Add(this.CreateFieldDataItem(this.mFields[fieldNo].keyName, "keyName"));
            this.FieldData.Items.Add(this.CreateFieldDataItem(this.mFields[fieldNo].locationH, "locationH"));
            this.FieldData.Items.Add(this.CreateFieldDataItem(this.mFields[fieldNo].locationW, "locationW"));
            this.FieldData.Items.Add(this.CreateFieldDataItem(this.mFields[fieldNo].locationX, "locationX"));
            this.FieldData.Items.Add(this.CreateFieldDataItem(this.mFields[fieldNo].locationY, "locationY"));
            this.FieldData.Items.Add(this.CreateFieldDataItem(this.mFields[fieldNo].name, "name"));
            this.FieldData.Items.Add(this.CreateFieldDataItem(this.mFields[fieldNo].pdfID, "pdfID"));
            this.FieldData.Items.Add(this.CreateFieldDataItem(this.mFields[fieldNo].reason, "reason"));
            this.FieldData.Items.Add(this.CreateFieldDataItem(this.mFields[fieldNo].required.ToString(), "required"));
            this.FieldData.Items.Add(this.CreateFieldDataItem(this.mFields[fieldNo].tag, "tag"));
            this.FieldData.Items.Add(this.CreateFieldDataItem(this.mFields[fieldNo].type, "type"));

            //データの描画
            //キャンバス内を削除
            this.HandwritingCanvas.Children.Clear();

            //ペンデータが空なら処理を抜ける
            if (this.mFields[fieldNo].penData.Count == 0) return;

            //各座標の最小値、最大値の取得
            int minX = 0;
            int minY = 0;
            int maxX = 0;
            int maxY = 0;
            int.TryParse(this.mFields[fieldNo].penData[0].points[0].x, out minX);
            int.TryParse(this.mFields[fieldNo].penData[0].points[0].y, out minY);
            int.TryParse(this.mFields[fieldNo].penData[0].points[0].x, out maxX);
            int.TryParse(this.mFields[fieldNo].penData[0].points[0].y, out maxY);
            for (int i = 0; i < this.mFields[fieldNo].penData.Count; i++)
            {
                var penData = this.mFields[fieldNo].penData[i];
                int x = 0;
                int y = 0;
                for (int j = 0; j < penData.points.Count; j++)
                {
                    int.TryParse(penData.points[j].x, out x);
                    if (minX > x) minX = x;
                    if (maxX < x) maxX = x;

                    int.TryParse(penData.points[j].y, out y);
                    if (minY > y) minY = y;
                    if (maxY < y) maxY = y;
                }
            }

            //描画しているデータの幅、高さの設定
            this.mDataHeight = (maxY - minY) / 25;
            this.mDataWidth = (maxX - minX) / 25;

            //描画の設定
            for (int i = 0; i < this.mFields[fieldNo].penData.Count; i++)
            {
                var penData = this.mFields[fieldNo].penData[i];
                int x = 0;
                int y = 0;
                int w = 0;

                //描画
                var polyline = new Polyline();
                var points = new PointCollection();
                polyline.Stroke = GetSolidColorBrush(penData.points[0].ink_color);
                int.TryParse(penData.points[0].w, out w);
                polyline.StrokeThickness = (double)w / this.mMaxW;
                int.TryParse(penData.points[0].x, out x);
                int.TryParse(penData.points[0].y, out y);
                points.Add(new Windows.Foundation.Point((x - minX) / 25, (y - minY) / 25));
                for (int j = 1; j < penData.points.Count; j++)
                {
                    //ループ初回時は処理を飛ばす
                    if(j != 1)
                    {
                        //線の太さ、色のどちらかが違っていたら
                        if (penData.points[j].w != penData.points[j - 1].w || penData.points[j].ink_color != penData.points[j - 1].ink_color)
                        {
                            //画面に表示
                            polyline.Points = points;
                            this.HandwritingCanvas.Children.Add(polyline);

                            //新たな表示用データの初期化
                            polyline = new Polyline();
                            points = new PointCollection();
                            polyline.Stroke = GetSolidColorBrush(penData.points[j].ink_color);
                            int.TryParse(penData.points[j].w, out w);
                            polyline.StrokeThickness = (double)w / this.mMaxW;
                            int.TryParse(penData.points[j - 1].x, out x);
                            int.TryParse(penData.points[j - 1].y, out y);
                            points.Add(new Windows.Foundation.Point((x - minX) / 25, (y - minY) / 25));
                        }
                    }

                    int.TryParse(penData.points[j].x, out x);
                    int.TryParse(penData.points[j].y, out y);
                    points.Add(new Windows.Foundation.Point((x - minX) / 25, (y - minY) / 25));
                }
                polyline.Points = points;
                this.HandwritingCanvas.Children.Add(polyline);
            }
        }

        //色情報の変換
        //引数:変換するpenDataのink_color
        private SolidColorBrush GetSolidColorBrush(string hex)
        {
            hex = hex.Replace("#", string.Empty);
            //byte a = (byte)(Convert.ToUInt32(hex.Substring(0, 2), 16));
            byte r = (byte)(255 - Convert.ToUInt32(hex.Substring(0, 2), 16));
            byte g = (byte)(255 - Convert.ToUInt32(hex.Substring(2, 2), 16));
            byte b = (byte)(255 - Convert.ToUInt32(hex.Substring(4, 2), 16));
            SolidColorBrush myBrush = new SolidColorBrush(Windows.UI.Color.FromArgb(255, r, g, b));
            return myBrush;
        }

        //表示する手書きデータの生成
        //引数 1:表示を行う手書きデータ
        //引数 2:表示を行う手書きデータのタイプ
        private ListViewItem CreateFieldDataItem(string content, string type = null)
        {
            ListViewItem listItem = new ListViewItem();

            //表示する文字列
            string text = "";

            //contentがnullの場合は空文字を入れる
            if(content == null)
            {
                content = "";
            }

            //データのタイプに文字が入っていたら
            if(type != null)
            {
                text = type + " : ";
            }

            //引数のデータが255文字より上の場合は255字以上の文字をカットする
            if (content.Length > 255)
            {
                text += content.Substring(0, 255);
            }
            else
            {
                text += content;
            }

            //データの全文字列格納用にデータを入れる
            this.mDataString.Add(content);

            //タグにデータが格納された番号を入れる
            listItem.Tag = this.mDataString.Count - 1;

            //表示する文字列の設定
            listItem.Content = text;

            //右クリック時のイベントを追加
            listItem.RightTapped += this.FieldData_RightTapped;

            return listItem;
        }

        //表示されているデータ関連の削除
        private void DeleteFieldData()
        {
            //データ一覧表の削除
            this.FieldData.Items.Clear();

            //データの全文字列格納用の削除
            this.mDataString.Clear();
        }

        //uuidが一致しているフィールドデータの取得
        //引数  :uuid
        //戻り値:フィールド番号(一致していない場合は-1を返す)
        private int GetFieldId(string uuid)
        {
            for (int i = 0; i < this.mFields.Count; i++)
            {
                if (this.mFields[i].UUID == uuid)
                {
                    return  i;
                }
            }

            return -1;
        }
    }
}
