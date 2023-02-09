const express = require('express'); // expressモジュールを読み込む
const multer = require('multer'); // multerモジュールを読み込む
const { v4: uuidv4 } = require('uuid');

const app = express(); // expressアプリを生成する
app.use(multer().none()); // multerでブラウザ(client)から送信されたデータを解釈する
app.use(express.static('web')); // 'web'フォルダ下の静的ファイルを公開/提供する

let port = 3000;    // port number (default)
let urlString = "";

const port_num = process.argv[2];
if(port_num){
    console.log("arg: " + port_num);
    port = parseInt(port_num);
};

// /url アクセスしてきたときに
// URL stringを返す
app.get('/api/url', (req, res) => {
    console.log('api/url: ' + urlString);
    // JSONを送信する
    res.json(urlString);
});

// /update に対してデータを送信してきたときに
// URL stringを更新する
app.post('/api/update', (req, res) => {
    // クライアントからの送信データを取得する
    const urlData = req.body;
    urlString = urlData.path;
//    urlString = req.body;   // update local data

    // コンソールに出力する
 //   console.log('Updated: ' + JSON.stringify(urlString));
    console.log('api/update: ' + urlString);

    // 追加した項目をクライアントにjson形式で返す
    res.json(urlData);
});

// ポート: port でサーバを立てる
app.listen(port, () => console.log('Listening on port ' + port));