<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WaterBananceReport.aspx.cs" Inherits="pipemonitor.WaterBananceReport" %>

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
    <script type="text/javascript" src="jqwidgets/jqwidgets/jqxdata.js"></script>
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
    <script type="text/javascript" src="jqwidgets/jqwidgets/jqxtree.js"></script>
    
 
    <script type="text/javascript" src="Scripts/InfoMaintain.js"></script>
   

       <script type="text/javascript">
           $(document).ready(function () {
               // Create a jqxDateTimeInput
               $("#dtBeginDate").jqxDateTimeInput({ width: '150px', height: '20px', theme: 'arctic', formatString: 'yyyy-MM-dd' });
               $("#dtEndDate").jqxDateTimeInput({ width: '150px', height: '20px', theme: 'arctic', formatString: 'yyyy-MM-dd' });
               var qrySrc = ["日", "月",  "年"];
               $("#cmbQrytype").jqxComboBox({ selectedIndex: 0, width: 120, source: qrySrc, theme: "arctic", height: 20 });

               $("#btnArea").jqxDropDownButton({ width: 160, height: 18, theme: 'arctic' });
               $('#trArea').jqxTree({ height: '200px', width: '330px', theme: 'arctic' });
               $('#trArea').on('select', function (event) {
                   var args = event.args;
                   var item = $('#trArea').jqxTree('getItem', args.element);
                   var dropDownContent = '<div style="position: relative; margin-left: 3px; margin-top: 2px;">' + item.label + '</div>';
                   $("#btnArea").jqxDropDownButton('setContent', dropDownContent);
               });

               BindRegion("trArea");
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
           function print() {
               var content = $('#mychart')[0].outerHTML;
            
               var newWindow = window.open('', '', 'width=800, height=500'),
                document = newWindow.document.open(),
                pageContent =
                    '<!DOCTYPE html>' +
                    '<html>' +
                    '<head>' +
                    '<link rel="stylesheet" href="jqwidgets/jqwidgets/styles/jqx.base.css" type="text/css" />' +
                    '<meta charset="utf-8" />' +
                    '<title></title>' +
                    '</head>' +
                    '<body>' + content + '</body></html>';
               document.write(pageContent);
               document.close();
               newWindow.print();


           }
        
         function FixHistoricData(data) {
               var records = new Array();

               for (var i = 0; i < data.length; i++) {

                   records.push({ 'calday': data[i].calday, 'calcount': parseFloat(data[i].calcount) });

               }
               return records;
           }


           function Query() {
               var index = $("#cmbQrytype").jqxComboBox("getSelectedIndex");
               if (index == 0) {
                   QueryByDate();
               }
               else if (index == 1) {
                   QueryByMonth();
               }
               else if (index == 2) {
                   QueryByYear();
               }

       }

       function QueryByMonth() {


           var begintime = $("#dtBeginDate").jqxDateTimeInput("val");
           var endtime = $("#dtEndDate").jqxDateTimeInput("val");
           var areaItem = $('#trArea').jqxTree("getSelectedItem");


           $.ajax(
                    {
                        type: "POST",
                        contentType: "application/json",
                        url: "WebMethods.aspx/getWatermeterBalanceReportForMonthChart",
                        data: "{'areaid':'" + areaItem.value + "','begincaptime':'" + begintime + "','endcaptime':'" + endtime + "'}",
                        dataType: "json",
                        success: function (msg) {


                            var ret = $.parseJSON(msg.d);
                            //   alert(msg.d);
                            var src = ret.data;
                            var settings = {
                                title: "水平衡报表（按月统计）",
                                description: "",
                                enableAnimations: true,
                                showLegend: true,
                                padding: { left: 5, top: 5, right: 10, bottom: 5 },
                                titlePadding: { left: 90, top: 0, right: 0, bottom: 10 },
                                source: src,
                                colorScheme: 'scheme05',
                                xAxis:
                {
                    dataField: 'calmonth',

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
                                     { dataField: 'level1calcount', displayText: '一级水表用水量' },
                                     { dataField: 'level2calcount', displayText: '二级水表用水量' },
                                      { dataField: 'level3calcount', displayText: '三级水表用水量' }
                                ]
                        }
                    ]
                            };
                            // setup the chart
                            $('#mychart').jqxChart(settings);


                        }
                    });


       }

       function QueryByYear() {


           var begintime = $("#dtBeginDate").jqxDateTimeInput("val");
           var endtime = $("#dtEndDate").jqxDateTimeInput("val");
           var areaItem = $('#trArea').jqxTree("getSelectedItem");


           $.ajax(
                    {
                        type: "POST",
                        contentType: "application/json",
                        url: "WebMethods.aspx/getWatermeterBalanceReportForYearChart",
                        data: "{'areaid':'" + areaItem.value + "','begincaptime':'" + begintime + "','endcaptime':'" + endtime + "'}",
                        dataType: "json",
                        success: function (msg) {


                            var ret = $.parseJSON(msg.d);
                            //   alert(msg.d);
                          
                            var src = ret.data;
                            var settings = {
                                title: "水平衡报表（按年统计）",
                                description: "",
                                enableAnimations: true,
                                showLegend: true,
                                padding: { left: 5, top: 5, right: 10, bottom: 5 },
                                titlePadding: { left: 90, top: 0, right: 0, bottom: 10 },
                                source: src,
                                colorScheme: 'scheme05',
                                xAxis:
                {
                    dataField: 'calyear',

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
                                     { dataField: 'level1calcount', displayText: '一级水表用水量' },
                                     { dataField: 'level2calcount', displayText: '二级水表用水量' },
                                     { dataField: 'level3calcount', displayText: '三级水表用水量' }
                                ]
                        }
                    ]
                            };
                            // setup the chart
                            $('#mychart').jqxChart(settings);


                        }
                    });


       }

           function QueryByDate() {

              var begintime= $("#dtBeginDate").jqxDateTimeInput("val");
              var endtime = $("#dtEndDate").jqxDateTimeInput("val");
              var areaItem= $('#trArea').jqxTree("getSelectedItem");

          
              $.ajax(
                    {
                        type: "POST",
                        contentType: "application/json",
                        url: "WebMethods.aspx/getWatermeterBalanceReportForDayChart",
                        data: "{'areaid':'" + areaItem.value + "','begincaptime':'" + begintime + "','endcaptime':'" + endtime + "'}",
                        dataType: "json",
                        success: function (msg) {


                            var ret = $.parseJSON(msg.d);
                           
                            var src= ret.data;
                            var settings = {
                                title: "水平衡报表（按日统计）",
                                description: "",
                                enableAnimations: true,
                                showLegend: true,
                                padding: { left: 5, top: 5, right: 10, bottom: 5 },
                                titlePadding: { left: 90, top: 0, right: 0, bottom: 10 },
                                source: src,
                                colorScheme: 'scheme05',
                                xAxis:
                {
                    dataField: 'calday',

                    type :'basic',
                    showGridLines: true
                   
                },
                                valueAxis:
                {
                    visible: true,
                    minValue: 0,
                    maxValue: 2000,
                    unitInterval: 200,
                    title: { text: 'Sales ($)<br>' },
                    labels: { horizontalAlignment: 'right' }
                },
                                seriesGroups:
                    [
                        {
                            type: 'line',
                            series: [
                                    { dataField: 'level1calcount', displayText: '一级水表用水量' },
                                     { dataField: 'level2calcount', displayText: '二级水表用水量' },
                                      { dataField: 'level3calcount', displayText: '三级水表用水量' }
                                ]
                        }
                    ]
                            };
                            // setup the chart
                            $('#mychart').jqxChart(settings);


                        }
                    });

           }
      

        </script>


      
  
</head>
<body>
  <div class="SearchOptionPanel" style="width:100%;height:50px;line-height:22px">
   <span style="font-size:12px;float:left">起始时间</span><div style="float:left" id="dtBeginDate"></div>
    <span style="float:left;font-size:12px">结束时间</span><div style="float:left" id="dtEndDate"></div><input type="button" value="查询" onclick="Query()" /><input type="button" value="打印"  onclick="print()"/>
     <span style="font-size:12px;float:left">统计类别</span><div style="margin-left:20px;float:left" id="cmbQrytype"></div>
     <span style="font-size:12px;float:left">所属区域</span> <div id="btnArea" style="float:left"><div id="trArea"></div></div>
  </div>
  <div id="mychart" style="width:1600px; height:700px">
  
  </div>
 
        <form id="Form1" runat="server">
        <asp:HiddenField runat="server" ID="watermeterIdValue" />
        </form>
</body>

</html>
