function loadTileLeft() {
    let t = $("#roofunderbase").children().length;
    let d1 = 70;
    for (let i = 0; i < t/2; i++) {
        d1 -= 3;
        $("#roofunderbase div:nth-child(" + i +")").css("transform", "skewX(-" + d1 + "deg)");
    }
}

function loadTileRight() {
    let t = $("#roofunderbase").children().length;
    let d1 = 70;
    for (let j = t; j >= t/2; j--) {
        d1 -= 3;
        $("#roofunderbase div:nth-child(" + j + ")").css("transform", "skewX(" + d1 + "deg)");
    }
}

function loadTileLeft2() {
    let t = $("#roofupbase").children().length;
    let d1 = 26;
    let d2 = 30;
    for (let i = 0; i < t / 2; i++) {
        d1 -= 1;
        d2 -= 1;
        $("#roofupbase div:nth-child(" + i + ")").css("transform", "skewX(-" + d1 + "deg)");
    }
}

function loadTileRight2() {
    let t = $("#roofupbase").children().length;
    let d1 = 26;
    for (let j = t; j >= t / 2; j--) {
        d1 -= 1;
        $("#roofupbase div:nth-child(" + j + ")").css("transform", "skewX(" + d1 + "deg)");
    }
}