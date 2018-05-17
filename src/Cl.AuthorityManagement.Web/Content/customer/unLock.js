var sliderel = {
    $: function (selector) {
        return document.getElementById(selector)
    },
    getEvent: function (e) {

        var e = e || window.event
        return e;
    },
    stopBubble: function (e) {
        var e = this.getEvent(e)
        if (typeof e.preventDefault != "undefined") {
            e.preventDefault();
        } else {
            e.returnValue = false;
        }
    }
},
    Elemt = {
        flag: false,
        nowMoseX: 0,
        mx: sliderel.$("slider-box"),
        sd: sliderel.$("slider"),
        st: sliderel.$("slider-text"),
        sb: sliderel.$("slider-bg"),
        se: sliderel.$("slider-Emerge"),
        sg: sliderel.$("stop-go"),
    }
Elemt.sd.onmousedown = function (e) {
    var e = sliderel.getEvent(e)
    sliderel.stopBubble(e);
    Elemt.flag = true
    nowMoseX = e.clientX - Elemt.sd.offsetLeft;
}
//滑块最大移动的距离
maxMove = Elemt.mx.offsetWidth - Elemt.sd.offsetWidth;
//鼠标移动的时候是否成功
Elemt.mx.onmousemove = function (e) {
    var e = sliderel.getEvent(e)
    if (Elemt.flag) {
        var moveX = e.clientX - nowMoseX;
        var oElemLeft = Elemt.sd.offsetLeft;//判断滑块移动的范围  
        if (oElemLeft < 0) { //判断滑块是否超出限制位置
            moveX = 0;
            Elemt.flag = false
        } else if (oElemLeft > maxMove) {
            moveX = maxMove;
            Elemt.sg.style.display = "block";
            Elemt.sd.style.display = "none"
            Elemt.sb.style.width = 300 + "px"
            Elemt.st.innerHTML = "滑动成功"
            Elemt.st.style.color = "#fff"
        }
    }
    Elemt.sd.style.left = moveX + "px"
    Elemt.sb.style.width = oElemLeft + 20 + "px";
}
//当鼠抬起判断是否滑动成功
Elemt.mx.onmouseup = function (e) {
    var e = sliderel.getEvent(e)
    Elemt.flag = false
    if (Elemt.sd.offsetLeft < maxMove) {
        speed = Math.ceil(Elemt.sd.offsetLeft / 40);
        time = setInterval(function () {
            if (Elemt.sd.offsetLeft >= 0) {
                Elemt.sd.style.left = Elemt.sd.offsetLeft - speed + "px";
                Elemt.sb.style.width = Elemt.sb.offsetWidth - speed + "px";
            } else {
                clearInterval(time);
                return false;
            }
        }, 10)
    }
}
//当鼠离开是否滑动成功 
Elemt.sd.onmouseout = function (e) {
    sliderel.stopBubble(e);
    Elemt.flag = false;
    if (Elemt.sd.offsetLeft < maxMove) {
        speed = Math.ceil(Elemt.sd.offsetLeft / 40);
        time = setInterval(function () {
            if (Elemt.sd.offsetLeft >= 0) {
                Elemt.sd.style.left = Elemt.sd.offsetLeft - speed + "px";
                Elemt.sb.style.width = Elemt.sb.offsetWidth - speed + "px";
            } else {
                clearInterval(time);
                return false;
            }
        }, 10);
    }
}
