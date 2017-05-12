<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WatermeterData.aspx.cs" Inherits="pipemonitor.WatermeterData" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head >
    <title>水表历史数据查询</title>

  
     <link rel="stylesheet" href="jqwidgets/jqwidgets/styles/jqx.base.css" type="text/css" />
    <link rel="stylesheet" href="jqwidgets/jqwidgets/styles/jqx.arctic.css" type="text/css" />
    <link rel="stylesheet" href="jqwidgets/jqwidgets/styles/jqx.arctic.css" type="text/css" />
    <link rel="stylesheet" href="jqwidgets/jqwidgets/styles/jqx.ui-redmond.css" type="text/css" />
    <link rel="stylesheet" href="Styles/main.css" type="text/css" />
    <script type="text/javascript" src="jqwidgets/scripts/jquery-2.0.2.min.js"></script>
    <script type="text/javascript" src="jqwidgets/jqwidgets/jqxcore.js"></script>
    <script type="text/javascript" src="jqwidgets/jqwidgets/jqxmenu.js"></script>
    <script type="text/javascript" src="jqwidgets/jqwidgets/jqxbuttons.js"></script>
    <script type="text/javascript" src="jqwidgets/jqwidgets/jqxwindow.js"></script>
    <script type="text/javascript" src="jqwidgets/jqwidgets/jqxinput.js"></script>
 
    <script type="text/javascript" src="jqwidgets/jqwidgets/jqxdropdownbutton.js"></script>
    <script type="text/javascript" src="jqwidgets/jqwidgets/jqxscrollbar.js"></script>
    <script type="text/javascript" src="jqwidgets/jqwidgets/jqxbuttons.js"></script>
    <script type="text/javascript" src="jqwidgets/jqwidgets/jqxdatetimeinput.js"></script>
    <script type="text/javascript" src="jqwidgets/jqwidgets/jqxcalendar.js"></script>
    <script type="text/javascript" src="jqwidgets/jqwidgets/jqxtooltip.js"></script>
    <script type="text/javascript" src="jqwidgets/jqwidgets/jqxtabs.js"></script>
    <script type="text/javascript" src="jqwidgets/jqwidgets/jqxcheckbox.js"></script>
    <script type="text/javascript" src="jqwidgets/jqwidgets/jqxpanel.js"></script>
    <script type="text/javascript" src="jqwidgets/jqwidgets/jqxcombobox.js"></script>
    <script type="text/javascript" src="jqwidgets/jqwidgets/jqxdata.js"></script>
    <script type="text/javascript" src="jqwidgets/jqwidgets/jqxdraw.js"></script>
    <script type="text/javascript" src="jqwidgets/jqwidgets/jqxchart.core.js"></script>
    <script type="text/javascript" src="jqwidgets/jqwidgets/jqxlistbox.js"></script>
  
    <script type="text/javascript" src="jqwidgets/jqwidgets/jqxgrid.js"></script>
    <script type="text/javascript" src="jqwidgets/jqwidgets/jqxgrid.selection.js"></script> 
    <script type="text/javascript" src="jqwidgets/jqwidgets/jqxgrid.columnsresize.js"></script> 
      <script type="text/javascript" src="jqwidgets/jqwidgets/jqxdata.export.js"></script> 
    <script type="text/javascript" src="jqwidgets/jqwidgets/jqxgrid.export.js"></script> 
    <script type="text/javascript" src="jqwidgets/jqwidgets/jqxgrid.sort.js"></script> 
    <script type="text/javascript" src="Scripts/canvasjs.min.js"></script>
     

       <script type="text/javascript">
           $(document).ready(function () {
               // Create a jqxDateTimeInput
               $("#dtBeginDate").jqxDateTimeInput({ width: '150px', height: '20px', theme: 'arctic', formatString: 'yyyy-MM-dd' });
               $("#dtEndDate").jqxDateTimeInput({ width: '150px', height: '20px', theme: 'arctic', formatString: 'yyyy-MM-dd' });

               $('#DataTab').jqxTabs({ width: '100%', height: '1000px', position: 'top', theme: 'arctic' });
               var qrySrc = ["按月查询", "按年查询", "按日查询"];
               $("#cmbQrytype").jqxComboBox({ selectedIndex: 0, width: 120, source: qrySrc, theme: "arctic", height: 20 });
               $("input[type='button']").jqxButton({ theme: 'arctic' });
           });
           function strToDate(str) {
               var strs = str.split(" ");
               str = strs[0];
               var dateStrs = str.split("/");
               var year = parseInt(dateStrs[0], 10);
               var month = parseInt(dateStrs[1], 10) - 1;
               var day = parseInt(dateStrs[2], 10);

               var date = new Date(year, month, day);
               return date;
           }
           function FixHistoricData(data) {
               var records = new Array();

               for (var i = 0; i < data.length; i++) {

                   records.push({ 'x': strToDate(data[i].CAPTime), 'y': parseFloat(data[i].WaterMeterReading) });

               }
               return records;
           }





           function Query() {
               //               var index = $("#DataTab").jqxTabs('selectedItem');
               //               if (index == 0) {
               QueryGrid();
               //  }
               //               else if (index == 1) {
               QueryChart();
               //               }
               //               else {

               var index2 = $("#cmbQrytype").jqxComboBox("getSelectedIndex");
               if (index2 == 0) {
                   QueryByMonth();
               }
               else if (index2 == 1) {
                   QueryByYear();

               }
               else if (index2 == 2) {
                   QueryByDate();
               }

               //               }

           }


           function QueryByDate() {

               var watermeterid = $("#watermeterIdValue").val();
               var begintime = $("#dtBeginDate").jqxDateTimeInput("val");
               var endtime = $("#dtEndDate").jqxDateTimeInput("val");

               $.ajax(
                    {
                        type: "POST",
                        contentType: "application/json",
                        url: "WebMethods.aspx/getWatermeterReadingDayChartData",
                        data: "{'watermeterid':'" + watermeterid + "','begincaptime':'" + begintime + "','endcaptime':'" + endtime + "'}",
                        dataType: "json",
                        success: function (msg) {


                            var ret = $.parseJSON(msg.d);
                            //   alert(msg.d);

                            var src = ret.data;
                            var dataAdapter = new $.jqx.dataAdapter(src, { async: false, autoBind: true, loadError: function (xhr, status, error) { alert('Error loading "' + source.url + '" : ' + error); } });
                            var settings = {
                                title: "",
                                description: "",
                                enableAnimations: true,
                                showLegend: true,
                                padding: { left: 5, top: 5, right: 10, bottom: 5 },
                                titlePadding: { left: 90, top: 0, right: 0, bottom: 10 },
                                source: dataAdapter,
                                colorScheme: 'scheme05',
                                xAxis:
                {
                    dataField: 'calday',
                    displayText: '日期',
                    type: 'basic',
                    showGridLines: true

                },
                                valueAxis:
                {
                    visible: true,
                    minValue: 0,
                    maxValue: 1000,
                    unitInterval: 100,
                    title: { text: '用水量 ($)<br>' },
                    labels: { horizontalAlignment: 'right' }
                },
                                seriesGroups:
                    [
                        {
                            type: 'line',
                            series: [
                                     { dataField: 'calcount', displayText: '用水量' }

                                ]
                        }
                    ]
                            };
                            // setup the chart
                            $('#columnChart').jqxChart(settings);



                        }
                    });



           }


           function QueryByYear() {

               var watermeterid = $("#watermeterIdValue").val();
               var begintime = $("#dtBeginDate").jqxDateTimeInput("val");
               var endtime = $("#dtEndDate").jqxDateTimeInput("val");

               $.ajax(
                    {
                        type: "POST",
                        contentType: "application/json",
                        url: "WebMethods.aspx/getWatermeterReadingYearChartData",
                        data: "{'watermeterid':'" + watermeterid + "','begincaptime':'" + begintime + "','endcaptime':'" + endtime + "'}",
                        dataType: "json",
                        success: function (msg) {


                            var ret = $.parseJSON(msg.d);


                            var src = ret.data;
                            var dataAdapter = new $.jqx.dataAdapter(src, { async: false, autoBind: true, loadError: function (xhr, status, error) { alert('Error loading "' + source.url + '" : ' + error); } });
                            var settings = {
                                title: "",
                                description: "",
                                enableAnimations: true,
                                showLegend: true,
                                padding: { left: 5, top: 5, right: 10, bottom: 5 },
                                titlePadding: { left: 90, top: 0, right: 0, bottom: 10 },
                                source: dataAdapter,
                                colorScheme: 'scheme05',
                                xAxis:
                {
                    dataField: 'calyear',
                    displayText: '年份',
                    type: 'basic',
                    showGridLines: true

                },
                                valueAxis:
                {
                    visible: true,
                    minValue: 0,
                    maxValue: 10000,
                    unitInterval: 1000,
                    title: { text: '用水量 ($)<br>' },
                    labels: { horizontalAlignment: 'right' }
                },
                                seriesGroups:
                    [
                        {
                            type: 'line',
                            series: [
                                     { dataField: 'calcount', displayText: '用水量' }

                                ]
                        }
                    ]
                            };
                            // setup the chart
                            $('#columnChart').jqxChart(settings);
                            // $('#columnChart').jqxChart('refresh');

                        }
                    });



           }

           function QueryByMonth() {

               var watermeterid = $("#watermeterIdValue").val();
               var begintime = $("#dtBeginDate").jqxDateTimeInput("val");
               var endtime = $("#dtEndDate").jqxDateTimeInput("val");





               $.ajax(
                    {
                        type: "POST",
                        contentType: "application/json",
                        url: "WebMethods.aspx/getWatermeterReadingMonthChartData",
                        data: "{'watermeterid':'" + watermeterid + "','begincaptime':'" + begintime + "','endcaptime':'" + endtime + "'}",
                        dataType: "json",
                        success: function (msg) {


                            var ret = $.parseJSON(msg.d);


                            var src = ret.data;
                            var dataAdapter = new $.jqx.dataAdapter(src, { async: false, autoBind: true, loadError: function (xhr, status, error) { alert('Error loading "' + source.url + '" : ' + error); } });
                            var settings = {
                                title: "",
                                description: "",
                                enableAnimations: true,
                                showLegend: true,
                                padding: { left: 5, top: 5, right: 10, bottom: 5 },
                                titlePadding: { left: 90, top: 0, right: 0, bottom: 10 },
                                source: dataAdapter,
                                colorScheme: 'scheme05',
                                xAxis:
                {
                    dataField: 'calmonth',
                    displayText: '月份',
                    type: 'basic',
                    showGridLines: true

                },
                                valueAxis:
                {
                    visible: true,
                    minValue: 0,
                    maxValue: 10000,
                    unitInterval: 1000,
                    title: { text: '用水量 ($)<br>' },
                    labels: { horizontalAlignment: 'right' }
                },
                                seriesGroups:
                    [
                        {
                            type: 'line',
                            series: [
                                     { dataField: 'calcount', displayText: '用水量' }

                                ]
                        }
                    ]
                            };
                            // setup the chart
                            $('#columnChart').jqxChart(settings);
                            //  $('#columnChart').jqxChart('refresh');

                        }
                    });


           }

           function print() {


               var index = $("#DataTab").jqxTabs('selectedItem');
               if (index == 0) {
                   printGrid();
               }
               else if (index == 1) {

                   printReadingChart();
               }
               else if (index == 2) {

               printColoumChart();

               }

           }



           function printReadingChart() {
               var content = $('#chartContainer')[0].outerHTML;
            
               var newWindow = window.open('', '', 'width=800, height=500'),
                document = newWindow.document.open(),
                pageContent =
                    '<!DOCTYPE html>' +
                    '<html>' +
                    '<head>' +
                    '<link rel="stylesheet"  href="jqwidgets/jqwidgets/styles/jqx.base.css" type="text/css" />' +
                    '<meta charset="utf-8" />' +
                    '<title></title>' +
                    '</head>' +
                    '<body>' + content + '</body></html>';
               document.write(pageContent);
               document.close();
               newWindow.print();


           }

           function printColoumChart() {

               var content = $('#columnChart')[0].outerHTML;
               var newWindow = window.open('', '', 'width=800, height=500'),
                document = newWindow.document.open(),
                pageContent =
                    '<!DOCTYPE html>' +
                    '<html>' +
                    '<head>' +
                    '<link rel="stylesheet"  href="jqwidgets/jqwidgets/styles/jqx.base.css" type="text/css" />' +
                    '<meta charset="utf-8" />' +
                    '<title></title>' +
                    '</head>' +
                    '<body>' + content + '</body></html>';
               document.write(pageContent);
               document.close();
               newWindow.print();

           }

           function printGrid() {


               var gridContent = $("#historicDataGrid").jqxGrid('exportdata', 'html');


               var newWindow = window.open('', '', 'width=800, height=500'),
                document = newWindow.document.open(),
                pageContent =
                    '<!DOCTYPE html>\n' +
                    '<html>\n' +
                    '<head>\n' +
                    '<meta charset="utf-8" />\n' +
                    '<title>管道历史异常信息报表</title>\n' +
                    '</head>\n' +
                    '<body>\n' + gridContent + '\n</body>\n</html>';
               document.write(pageContent);
               document.close();
               newWindow.print();

           }
           function QueryGrid() {

               var watermeterid = $("#watermeterIdValue").val();
               var begintime = $("#dtBeginDate").jqxDateTimeInput("val");
               var endtime = $("#dtEndDate").jqxDateTimeInput("val");

               $.ajax(
                    {
                        type: "POST",
                        contentType: "application/json",
                        url: "WebMethods.aspx/getWatermeterReadingByWatermeterIDandCAPTime",
                        data: "{'watermeterid':'" + watermeterid + "','begincaptime':'" + begintime + "','endcaptime':'" + endtime + "'}",
                        dataType: "json",
                        success: function (msg) {

                            var ret = $.parseJSON(msg.d);



                            var source =
                                {
                                    localdata: ret.data,
                                    datatype: "array",
                                    datafields:
                                    [
                                        { name: 'CAPTime', type: 'string' },
                                        { name: 'WaterMeterReading', type: 'string' },

                                          { name: 'WaterMeterName', type: 'string' },
                                             { name: 'WaterMeterNo', type: 'string' }

                                    ]
                                };
                            var dataAdapter = new $.jqx.dataAdapter(source);

                            $("#historicDataGrid").jqxGrid(
                                {
                                    width: 850,
                                    autoheight: true,
                                    source: dataAdapter,
                                    columnsresize: true,
                                    showemptyrow: false,
                                    theme: 'arctic',
                                    columns: [
                                      { text: '水表名称', datafield: 'WaterMeterName', width: 120 },
                                      { text: '水表编号', datafield: 'WaterMeterNo', width: 120 },
                                      { text: '水表读数', datafield: 'WaterMeterReading', width: 180 },
                                      { text: '获取时间', datafield: 'CAPTime' }

                                    ]
                                });



                        }

                    });




           }
           function QueryChart() {
               var watermeterid = $("#watermeterIdValue").val();
               var begintime = $("#dtBeginDate").jqxDateTimeInput("val");
               var endtime = $("#dtEndDate").jqxDateTimeInput("val");
               $.ajax(
                    {
                        type: "POST",
                        contentType: "application/json",
                        url: "WebMethods.aspx/getWatermeterReadingByWatermeterIDandCAPTime",
                        data: "{'watermeterid':'" + watermeterid + "','begincaptime':'" + begintime + "','endcaptime':'" + endtime + "'}",
                        dataType: "json",
                        success: function (msg) {

                            var ret = $.parseJSON(msg.d);
                            if (ret == null || ret.data == null || ret.data.length == 0) {
                                return;
                            }
                      
                          var src = ret.data;
                            var dataAdapter = new $.jqx.dataAdapter(src, { async: false, autoBind: true, loadError: function (xhr, status, error) { alert('Error loading "' + source.url + '" : ' + error); } });
                            var settings = {
                                title: "",
                                description: "",
                                enableAnimations: true,
                                showLegend: true,
                                padding: { left: 5, top: 5, right: 10, bottom: 5 },
                                titlePadding: { left: 90, top: 0, right: 0, bottom: 10 },
                                source: dataAdapter,
                                colorScheme: 'scheme05',
                                xAxis:
                {
                    dataField: 'InsertTime',
                    displayText: '读取时间',
                    type: 'basic',
                    showGridLines: true

                },
                                valueAxis:
                {
                    visible: true,
                    minValue: 0,
                    maxValue: 3000,
                    unitInterval: 300,
                    title: { text: '读数 ($)<br>' },
                    labels: { horizontalAlignment: 'right' }
                },
                                seriesGroups:
                    [
                        {
                            type: 'line',
                            series: [
                                     { dataField: 'WaterMeterReading', displayText: '读数' }

                                ]
                        }
                    ]
                            };
                            // setup the chart
                    $('#chartContainer').jqxChart(settings);



                        }
                    });



                       
           }

//           function QueryChart() {
//               var watermeterid = $("#watermeterIdValue").val();
//               var begintime = $("#dtBeginDate").jqxDateTimeInput("val");
//               var endtime = $("#dtEndDate").jqxDateTimeInput("val");
//               $.ajax(
//                    {
//                        type: "POST",
//                        contentType: "application/json",
//                        url: "WebMethods.aspx/getWatermeterReadingByWatermeterIDandCAPTime",
//                        data: "{'watermeterid':'" + watermeterid + "','begincaptime':'" + begintime + "','endcaptime':'" + endtime + "'}",
//                        dataType: "json",
//                        success: function (msg) {

//                            var ret = $.parseJSON(msg.d);
//                            if (ret == null || ret.data == null || ret.data.length == 0) {
//                                return;
//                            }
//                            var data = ret.data;
//                            var source = FixHistoricData(data);



//                            var chart = new CanvasJS.Chart("chartContainer",
//    {
//        theme: "theme2",
//        title: {
//            text: "用水量（吨）"
//        },
//        animationEnabled: true,
//        axisX: {
//            valueFormatString: "MM-DD",
//            interval: 1,
//            intervalType: "day"

//        },
//        axisY: {
//            includeZero: false

//        },
//        data: [
//      {
//          type: "line",
//          //lineThickness: 3,        
//          dataPoints: source
//      }


//      ]
//    });

//                            chart.render();
//                        }
//                    }
//                    );

//           }
        </script>


      
  
</head>
<body>
  <div class="SearchOptionPanel" style="width:100%;height:50px;line-height:22px">
   <span style="font-size:12px;float:left">起始时间</span><div style="float:left" id="dtBeginDate"></div>
    <span style="float:left;font-size:12px">结束时间</span><div style="float:left" id="dtEndDate"></div><input type="button" value="查询" onclick="Query()" /><input type="button" value="打印" onclick="print();" />
  </div>
  <div id='DataTab'>
            <ul>
                <li style="margin-left: 30px;">网格</li>
                <li>读数图表</li>
                <li>用水量图表</li>
             
            </ul>
            <div>
                <div id='historicDataGrid' style="width:100%; ">
               </div>
           
            </div>
            <div>
                 <div id='chartContainer' style="width:100%; height:500px">
               </div>
            </div>
          
            <div>
                <div style="margin-left:20px;margin-top:10px" id="cmbQrytype"></div>
                <div id='columnChart' style="width:1200px; height:500px">
               </div>
            </div>       
        </div>   
        <form id="Form1" runat="server">
        <asp:HiddenField runat="server" ID="watermeterIdValue" />
        </form>
</body>

</html>
