function getAllArticles() {
    //send website address, get articles 
    let uri = "api/Articles";
    $(function () {
        $.ajax({
            url: uri,
            type: "GET"
        }).done(
            function (data) {
                var txt = "";
                $.each(data, function (key, item) {
                    txt += "<tr><td><a href='" + item.link + "'>" + item.title + "</a></td></tr>";
                });
                $("#allArticles").append(txt);
            });
    });
     //getMyArticles();

    //getNewArticles();
    getNewArticlesSort();

    setCookie("username", Date.now(), 365);
   // checkCookie();
}

//test function
function testFunction() {
    $(function () {
        let uri = "api/Articles/GetArticles/20141003/20210325";
        $.ajax({
            url: uri,
            type: "GET"
        }).done(
            function (data) {
                var txt = "";
                $.each(data, function (key, item) {
                    txt += "<div><a href='" + item.link + "'>" + item.title + "</a></div>";
                });
                $("#testfield").append(txt);
            });
    });
  }
//show message from message queue
/*function getNewArticles() {
    let uri = "api/Articles/GetNewArticles";
    setInterval(
        function() {
            $.get(uri).done(
                function (data) {
                    if (data != null && data.length > 0) {
                        $("#newestArticles").empty();
                        $.each(data, function (key, item) {
                            var txt = "<div><a href='" + item.link + "'>" + item.title + "</a></div>";
                            setTimeout(function () { $("#newestArticles").append(txt); }, 30000);
                            //
                        }
                        );
                    }
                });
        }, 15000);
    }/*/

function getNewArticles() {
    let uri = "api/Articles/GetNewArticles";
    $(function () {
            $.get(uri).done(
                function (data) {
                    if (data != null && data.length > 0) {
                        $("#newestArticles").empty();
                        $.each(data, function (key, item) {
                            var txt = "<div class='grid-item'><div class='marksDiv' onclick='$(this).children().eq(1).toggle()'><i class='fa fa-envelope-o' title='Recommend to a friend' aria-hidden='true' style='margin-right: 5%'></i><i class='fa fa-heart-o' title='Mark as favorite' onclick='$(this).hide()' ></i></div><div class='linkDiv'><a href='" + item.link + "'>" + item.title + "</a></div><div class='labelDiv'><label>" + item.publishTime.toString().replace('T', '') + "</label></div></div>";
                            $("#newestArticles").append(txt);
                            //()
                        }
                        );
                    }
                }

            );
        });
}

function getNewArticlesSort() {
    let uri = "api/Articles/GetNewArticles";
    $(function () {
        $.get(uri).done(
            function (data) {
                data.sort(function (a, b) {
                      return a.publishTime > b.publishTime ? 1 : -1;
                   // return a.title > b.title ? 1 : -1;
                });
                $.each(data, function (key, item) {
                    var txt = "<div class='grid-item'><div class='marksDiv' onclick='$(this).children().eq(1).toggle()'><i class='fa fa-envelope-o' title='Recommend to a friend' aria-hidden='true' style='margin-right: 5%'></i><i class='fa fa-heart-o' title='Mark as favorite' onclick='$(this).hide()' ></i></div><div class='linkDiv'><a href='" + item.link + "'>" + item.title + "</a></div><div class='labelDiv'><label>" + item.publishTime.toString().replace('T', '') + "</label></div></div>";
                    $("#newestArticles").append(txt);
                    //()
                });
            });
    });
}


function bigSpace() {
    $(function () {
        $("#newestArticles").childen().css("border-color", "red");
    });
}

function getDefaultArticles() {
    //send website address, get articles 
    var site = $("#webaddress").val();
    let uri;
    if (site != "") {
        uri = "api/Articles/GetDefaultArticles/" + $("#webaddress").val();
    }
    else {
        uri = "api/Articles/GetDefaultArticles/" + "vg.no";
    }
    //uri = "api/Articles/GetDefaultArticles/" + "document.no";
    $(function () {
        $.get(uri).done(function (data) {
            var txt = "";
            $.each(data, function (key, item) {
                txt += "<tr><td style='width: 460px'><a href='" + item.link + "'>" + item.title + "</a></td><td  style='width: 100px;'><button onclick='addArticle()'/></td></tr>";
            });
            $("#myArticlesTable").append(txt);
        });
    });
}


function getMyArticles() {
    //alert("1");
    //send website address, get articles 
    var site = $("#webaddress").val();
    let uri;
    if (site != "") {
        uri = "api/Articles/GetMyArticles/" + $("#webaddress").val();
    }
    /*else {
        uri = "api/Articles/GetMyArticles/" + "vg.no";
    }*/
    $(function () {
        $("#myArticlesTable").empty();
        $.get(uri).done(function (data) {
            var txt = "";
            $.each(data, function (key, item) {
                txt += "<tr><td style='width: 460px'><a href='" + item.link + "'>" + item.title + "</a></td><td style='width: 100px;'><button class='bc' /></td></tr > ";
            });
            $("#myArticlesTable").append(txt);
            $(".bc").on("click", function () {
                // $("#webaddress").value = $(this).parent().siblings("td")[1].childen("a").attr();
                var row = $(this).parent().parent();
                //var v = $(this).parent().parent().children().first().children().first().attr("href");
                var article = {
                    Title: row.find("a").text(), Link: row.find("a").attr("href"), PublishTime: new Date(Date.now())
                };
                alert(article.Title);
                addArticle(article);
            });

        });
    });
}

/*function coverHeart(element) {
    $(function () 
        let text = "<div class='coverDiv'>test</div>";
        $(element).append(text);
    });
}

function removeCover(element) {
    $(function () {
        $("div").remove(".coverDiv");
    });
}*/

function addArticle(article) {
    var uri = "api/Articles";
    $(function () {
        alert(article.Link);
        $.ajax({
            type: "POST",
            url: uri,
            //data: '{"article":"' + JSON.stringify(article) + '"}',
            data: JSON.stringify(article),
            accept: "application/json",
            contentType: "application/json",
            success: function () {
                alert("OK");
            },
            fail: function (status) {
                alert(Statua + ": Something is wrong!");
            }
        });
    });
    /*var uri = "api/Articles";
    var article = {
        Id: 2, Title: "We eat", Link: "vg.no/weeat.html", PublishTime: new Date("July 21, 1983")
    };

    fetch(uri, {
        method: 'POST',
        headers: {
            'Accept': 'application/json',
            'Content-Type': 'application/json'
        },
        body: JSON.stringify(article)
    })
        .then(response => response.json())
        .then(() => {
            //getItems();
            //addNameTextbox.value = '';
        })
        .catch(error => console.error('Unable to add item.', error));*/
}

//get online user number
function getUserNumber() {
    //the user' last visited news website before he leaves
    var lastSite = window.location.hostname;
    setCookie("lastVisitedSite", lastSite, 365);
}

//store the current website in a cookie, run by unload event, the system load news from the website when the user comes back
function storeLastVisitedSite() {
    //the user' last visited news website before he leaves
    var lastSite = window.location.hostname;
    setCookie("lastVisitedSite", lastSite, 365);
}

//store the last visit address with a cookie
function getLastVisitSite() {
    //get the cookie which stores the last visit site
    lastWebAddress = localStorage.getItem("lastSiteCookie");
    return lastWebAddress;
}

//if the cookie exists does not exist, create a new cookie
function checkCookie() {
    var user = getCookie("username");
    if (user != "" && user != undefined) {
        alert("Welcome again " + user);
    } else {
        user = Date.now().toUTCString();
        if (user != "" && user != null) {
            setCookie("username", user, 365);
        }
    }
} 

//set a cookie
function setCookie(cname, cvalue, exdays) {
    var d = new Date();
    d.setTime(d.getTime() + (exdays * 24 * 60 * 60 * 1000));
    var expires = "expires=" + d.toUTCString();
    document.cookie = cname + "=" + cvalue + ";" + expires + ";path=/";
}

function getCookie(cname) {
    var name = cname + "=";
    var ca = document.cookie.split(';');
    for (var i = 0; i < ca.length; i++) {
        var c = ca[i];
        while (c.charAt(0) == ' ') {
            c = c.substring(1);
        }
        if (c.indexOf(name) == 0) {
            return c.substring(name.length, c.length);
        }
    }
    return "";
}

//web worker counting online user, run by onload
function addUser() {
    if (typeof (Worker) !== "undefined") { // Web worker is support in the Browser
        if (typeof (w) == "undefined") {  //check if the work exists, create a new if not
            try {
                document.addEventListener('onload');
                var w = new Worker("countworker.js");
                //listen to the worker
                w.onmessage = function (event) {
                    document.getElementById("onlineNumber").innerHTML = event.data;
                };
            }
            catch (ex) { }
        }
    }
}

//web worker counting online user, run by onunload
function reduceUser() {
    if (typeof (Worker) !== "undefined") { // Web worker is support in the Browser
        if (typeof (w) == "undefined") {  //check if the work exists, create a new if not
            try {
                document.addEventListener('onunload');
                var w = new Worker("countworker.js");
                //listen to the worker
                w.onmessage = function (event) {
                    document.getElementById("onlineNumber").innerHTML = event.data;
                };
            }
            catch (ex) { }
        }
    }
    //store the last visited news website of the user 
    storeLastVisitedSite();
}


//web worker monitoring new article, the function is not finished
function newArticleMessage() {
    if (typeof (Worker) !== "undefined") { // Web worker is support in the Browser
        if (typeof (w) == "undefined") {  //check if the work exists, create a new if not
            var w = new Worker("newarticle.js");
            //listen to the worker
            w.onmessage = function () {
                document.getElementById("newArticle").innerHTML = event.data;
            };
        }
    }
}


