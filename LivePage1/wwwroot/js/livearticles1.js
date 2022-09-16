function getAllArticles() {
    //get the stored article list 
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
    getDefaultArticles(); //get articles from default website
    getNewArticlesSort(); //show the new articles
}

function getNewArticlesSort() {
    let uri = "api/Articles/GetNewArticles";
    $(function () {
        $.get(uri).done(
            function (data) {
                data.sort(function (a, b) {
                      return a.publishTime > b.publishTime ? 1 : -1;
                });
                $.each(data, function (key, item) {
                    var txt = "<div class='grid-item'><div class='marksDiv'><i class='fa fa-envelope-o' title='Recommend to a friend' aria-hidden='true' style='margin-right: 5%'></i><i class='fa fa-heart-o' title='Mark as favorite' ></i></div><div class='linkDiv'><a href='" + item.link + "'>" + item.title + "</a></div><div class='labelDiv'><label>" + item.publishTime.toString().replace('T', '') + "</label></div></div>";
                    $("#newestArticles").append(txt);
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
    uri = "api/Articles/GetMyArticles/" + "e24.no";
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
    
    //send website address, get articles 
    var site = $("#webaddress").val();
    let uri;
    if (site != "" && site != 'undefined') {
        uri = "api/Articles/GetMyArticles/" + site;
    }
    else {
        uri = "api/Articles/GetMyArticles/" + "vg.no";
    }
    $(function () {
        $("#myArticlesTable").empty();
        $.get(uri).done(function (data) {
            var txt = "";
            $.each(data, function (key, item) {
                txt += "<tr><td style='width: 460px'><a href='" + item.link + "'>" + item.title + "</a></td><td style='width: 100px;'><button class='bc' /></td></tr > ";
            });
            $("#myArticlesTable").append(txt);
            $(".bc").on("click", function () { //add click event to dynamic htmlbutton
                var row = $(this).parent().parent();
                var article = {
                    Title: row.find("a").text(), Link: row.find("a").attr("href"), PublishTime: new Date(Date.now())
                };
                addArticle(article); //add the chosen article to article list
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
        $.ajax({
            type: "POST",
            url: uri,
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
}



