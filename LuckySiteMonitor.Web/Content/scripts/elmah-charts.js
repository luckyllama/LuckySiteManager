/// <reference path="/content/scripts/lib/backbone.js" />

var elmahChartType = {
    lastSevenDayCount: {
        name: "Daily Errors Over The Last Seven Days"
    }
};

var ElmahChart = Backbone.Model.extend({
    defaults: {
        type: elmahChartType.lastSevenDayCount
    },
    initialize: function () {
        
    }
});

var ElmahCharts = Backbone.Collection.extend({
    model: ElmahChart
});

var ElmahChartView = Backbone.View.extend({
    initialize: function () {

    },
    render: function () {
        var template = _.template($("#elmah-chart-template").html(), { name: "chart..." });
        console.log(this);
        this.el.html(template);
    }
});

var SiteRouter = Backbone.Router.extend({
    initialize: function () {
        $("#elmah-list .elmah-information .elmah-chart-area").each(function () {
            var chartView = new ElmahChartView({ el: $(this) });
            chartView.render();
        });
    },
    routes: {
        "*actions": "defaultRoute"
    },
    defaultRoute: function (actions) {
    }
});

$(function () {
    var siteRouter = new SiteRouter;
    Backbone.history.start();
});