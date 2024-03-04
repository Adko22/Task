$(document).ready(function () {
    google.charts.load('current', { 'packages': ['bar'] });
    google.charts.setOnLoadCallback(loadChartData);
    let globalChartData;

    function loadChartData() {
        var startDate = $('#startDatePicker').val();
        var endDate = $('#endDatePicker').val();
        var isProject = $('input[name="dataScope"]:checked').val() === "project";
        var chartUrl = $('#topUsersChart').data('chart-url');

        $.ajax({
            url: chartUrl,
            data: { startDate: startDate, endDate: endDate, isProject: isProject },
            dataType: 'json',
            success: function (chartData) {
                globalChartData = chartData;
                drawChart(chartData, null);
            }
        });
    }

    function drawChart(chartData, compareData) {
        console.log('Debug - Chart Data:', chartData);
        if (compareData) {
            console.log('Debug - Compare Data:', compareData);
        } else {
            console.log('Debug - No Compare Data');
        }

        var data = new google.visualization.DataTable();
        data.addColumn('string', 'User');
        data.addColumn('number', 'Hours');
        data.addColumn('number', 'Comparison Hours');

        chartData.forEach(function (row) {
            console.log(`Row entityId: ${row.entityId}, CompareData userId: ${compareData ? compareData.userId : 'N/A'}`);
            var rowContent = [row.entityName, row.totalHours, null];

            if (compareData) {
                console.log(`Matching userId found: ${compareData.userId}. Total hours for comparison:`, compareData.totalHours);
                rowContent[2] = compareData.totalHours;
            }
            data.addRow(rowContent);
        });

        var options = {
            chart: {
           
            },
            bars: 'horizontal'
        };

        var chart = new google.charts.Bar(document.getElementById('topUsersChart'));
        console.log("Drawing chart with data:", data.toJSON());
        chart.draw(data, google.charts.Bar.convertOptions(options));
    }

    loadChartData();

    $(document).on('click', '.compare-btn', function () {
        var userId = $(this).data("userid");
        var startDate = $('#startDatePicker').val();
        var endDate = $('#endDatePicker').val();
        var compareUrl = $(this).data('compare-url');

        $.ajax({
            url: compareUrl,
            type: 'GET',
            data: {
                userId: userId,
                startDate: startDate,
                endDate: endDate
            },
            success: function (response) {
                console.log("Received compare data:", response);
                drawChart(globalChartData, response);
            },
            error: function (xhr, status, error) {
                console.error("Error getting comparison data:", error);
            }
        });
    });

    $('#loadChartButton').click(loadChartData);
});