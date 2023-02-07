//var urlParam = location.search;
var num = getParam('param');
if(num == null) num = "0"; 
var sheetName = "assets/" + num + ".png";

var img = document.getElementById('q_sheet');
q_sheet.setAttribute('src', sheetName);


/**
 * Get the URL parameter value
 *
 * @param  name {string} パラメータのキー文字列
 * @return  url {url} 対象のURL文字列（任意）
 */
function getParam(name, url) {
    if (!url) url = window.location.href;
    name = name.replace(/[\[\]]/g, "\\$&");
    var regex = new RegExp("[?&]" + name + "(=([^&#]*)|&|#|$)"),
        results = regex.exec(url);
    if (!results) return null;
    if (!results[2]) return '';
    return decodeURIComponent(results[2].replace(/\+/g, " "));
}

//
// API経由でAnswer page URLをPOSTする
//
async function postUpdatedAnsPage(path) {
    // 送信データ'title'にタイトルテキストを追加する
       const body = new FormData();
       body.append("path", path);

    // Fetch APIを使って、Web APIにPOSTでデータを送信する
    return fetch("./api/update", {
        method: "POST", // POSTメソッドで送信する,
        body,
    }).then((response) => response.json());
}

//
//  Page navigation for questionear sheet operation
//   navigateTo: URL strings to be navigated
//   updatedPath: URL tree path w/o host 
//
function OnLinkClick(navigateTo, updatedPath) {
    let host = location.host;  // this hostname and port 
    // API post the URL path
    if(updatedPath)
        postUpdatedAnsPage(host + updatedPath);

    // page navigation
    window.location.href = navigateTo; // 通常のpage遷移

    // target = document.getElementById("output");
    // target.innerHTML = "Penguin";
    return false;
}