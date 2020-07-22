(function ($) {
    var demo = window.demo = {};
    var masterTableView;
    var grid;

    $.extend(demo, {
        onClientSelectedIndexChanged: function (sender, args) {
            document.body.className = "sprite" + sender.get_value();
        },

        onGridCreated: function (sender, args) {
            grid = sender;
            masterTableView = grid.get_masterTableView();
            sender.PagerControl.className = sender.PagerControl.className.replace(" table-hover", "");
            $telerik.$(".rgCommandTable", sender.get_element()).addClass("table table-striped");
            $telerik.$(".rgEditRow input:text", sender.get_element()).addClass("form-control");
            sender.repaint();

            createPagerButtons();
            initPagerEvents();
        }
    });

    function createPagerButtons() {
        for (var i = 0; i < masterTableView.get_pageCount() ; i++) {
            var $page = $("<li>").append($("<a>").attr("href", "#").text(i + 1));
            $(".pagination")
				.children()
				.last()
				.before($page);
        }
    }

    function initPagerEvents() {
        $(".pagination")
			.find("a")
			.click(function (e) {
			    var $target = $(e.target);
			    var pageIndex = parseFloat($target.text());

			    if (!isNaN(pageIndex)) {
			        masterTableView.page(pageIndex);
			        e.preventDefault();
			    }
			})
				.first()
				.click(function () {
				    masterTableView.page("First");
				})
			.end()
				.last()
				.click(function () {
				    masterTableView.page("Last");
				})
			.end()            
			.eq(masterTableView.get_currentPageIndex() + 1)
			.parent()
			.addClass("active");

        $(".rgInfoPart")
            .find(".btn")
            .click(function (e) {
                masterTableView.page(parseInt($(".rgInfoPart .form-control").val(), 10));
                e.preventDefault();
            })
            .end()
            .find(".form-control")
            .val(masterTableView.get_currentPageIndex() + 1);
    }
})($telerik.$);