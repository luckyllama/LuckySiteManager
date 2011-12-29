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
        this.model = new ElmahChart;
    },
    render: function () {
        var template = _.template($("#elmah-chart-template").html(), { name: this.model.get("type").name });
        
        console.log(this);
        this.el.html(template);
    }
});

var ElmahView = Backbone.View.extend({
    initialize: function () { },
    render: function () {
        var chartView = new ElmahChartView({ el: this.$(".elmah-chart-area") });
        chartView.render();
    }
});

var SiteRouter = Backbone.Router.extend({
    initialize: function () {
        $("#elmah-list .elmah-information").each(function () {
            var view = new ElmahView({ el: $(this) });
            view.render();
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