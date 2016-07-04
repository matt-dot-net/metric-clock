
function MetricClock() {


    this.getTime = function getTime() {
        var secRatio = (25.0 / 18.0);

        var now = new Date();
        var midnight = new Date(now.getFullYear(), now.getMonth(), now.getDate(), 0, 0, 0, 0);
        var startOfDay = new Date(now.getFullYear(), now.getMonth(), now.getDate(), 6, 0, 0, 0);
        var metricSecsSinceStart, totalSecondsToday;

        totalSecondsToday = (now - midnight) / 1000;
        if (now >= midnight && now < startOfDay) {
            //this means we are between 10 and 1 (10,11,12,1)
            metricSecsSinceStart = 90000 + (secRatio * totalSecondsToday);
        }
        else {
            var standardSecs = totalSecondsToday - 21600;// start.TimeOfDay.TotalSeconds;
            metricSecsSinceStart = standardSecs * secRatio;
        }

        var currentMetricHours = Math.floor(metricSecsSinceStart / 10000);

        var currentMetricMinutes = Math.floor((metricSecsSinceStart - (currentMetricHours * 10000)) / 100);
        var currentMetricSeconds = Math.floor(metricSecsSinceStart - (currentMetricHours * 10000) - (currentMetricMinutes * 100));
        if (currentMetricHours == 12)
            currentMetricHours = 0;
        var stringTime = pad(1 + currentMetricHours,2) + ":" + pad(currentMetricMinutes,2) + ":" + pad(currentMetricSeconds,2);

        return stringTime;
    }
}

function pad(num, size) {
    var s = num+"";
    while (s.length < size) s = "0" + s;
    return s;
}

var metricClock = new MetricClock();