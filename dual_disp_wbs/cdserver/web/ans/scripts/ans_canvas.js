let penUp = 1;
var prev_X, prev_Y;

window.addEventListener("load", () => {
  const mainCanvas = document.getElementById("canvas_ans");

  mainCanvas.style.border = "2px solid"; //border around the canvas area
  mainCanvas.style.cursor = "url(../assets/pen_u_b.png), auto";

  const context = mainCanvas.getContext("2d");

  function log(tag, e) {
    console.log(tag, e.pointerId, e.offsetX, e.offsetY, e.buttons, e.pressure);
  }

  function drawPointer(e, p) {
    const x = e.offsetX;
    const y = e.offsetY;
    const z = e.pressure;
    const radius = 1.0; //+ e.pressure * 0.0  // 5.0 + ... 10.0
    const red = e.pressure * 255;
    context.beginPath();
    context.moveTo(x + radius, y);
    context.arc(x, y, radius, 0, Math.PI * 2);
    context.closePath();
    context.fill();

    if (e.buttons != 0 || e.pressure > 0.0) {
      var color = `rgba('black')`; // Black
      context.fillStyle = color;

      // If a pen is down, draw line
      if (penUp == 0 && p == 0) {
        // Reset the current path
        context.beginPath();
        context.lineWidth = radius * 2;
        context.strokeStyle = color;
        context.moveTo(x, y);
        context.lineTo(prev_X, prev_Y);
        // Make the line visible
        context.stroke();
      }
      prev_X = x;
      prev_Y = y;
    }
    // in case of drawing the Pen Hover
    else if (e.buttons == 0 && e.pressure == 0.0) {
      penUp = 1;
    }
  }

  mainCanvas.addEventListener("pointerdown", (e) => {
    log("pointerdown", e);
    drawPointer(e, 1);
    penUp = 0;
    e.preventDefault();
  });

  mainCanvas.addEventListener("pointermove", (e) => {
    log("pointermove", e);
    if (penUp == 0) drawPointer(e, 0);
    e.preventDefault();
  });

  mainCanvas.addEventListener("pointerup", (e) => {
    log("pointerup", e);
    penUp = 1;
    e.preventDefault();
  });

//   // Export to a file
//   document.getElementById("buttonExport").onclick = function () {
//     console.log("Export data to a file.");
//     exportTextFile(zArr, "z.txt");
//   };
});


function exportTextFile(data, filename) {
  let blob = new Blob([data], { type: "text/plan" }); // テキスト形式でBlob定義
  let link = document.createElement("a"); // HTMLのaタグを作成
  link.href = URL.createObjectURL(blob); // aタグのhref属性を作成
  link.download = filename; // aタグのdownload属性を作成
  link.click(); // 定義したaタグをクリック（実行）
}
