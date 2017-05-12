<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SensorData.aspx.cs" Inherits="pipemonitor.SensorData" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head  >
    <title></title>
    <link rel="stylesheet" href="../../jqwidgets/styles/jqx.base.css" type="text/css" />
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
    <script type="text/javascript" src="jqwidgets/jqwidgets/jqxgrid.js"></script>
    <script type="text/javascript" src="jqwidgets/jqwidgets/jqxgrid.selection.js"></script> 
    <script type="text/javascript" src="jqwidgets/jqwidgets/jqxgrid.columnsresize.js"></script> 
    <script type="text/javascript" src="Scripts/canvasjs.min.js"></script>
         <script type="text/javascript" src="jqwidgets/jqwidgets/jqxdata.export.js"></script> 
    <script type="text/javascript" src="jqwidgets/jqwidgets/jqxgrid.export.js"></script> 
    <script type="text/javascript" src="jqwidgets/jqwidgets/jqxgrid.sort.js"></script> 

          <script type="text/javascript">
              $(document).ready(function () {
                  // Create a jqxDateTimeInput
                  $("#dtBeginDate").jqxDateTimeInput({ width: '150px', height: '20px', theme: 'arctic', formatString: 'yyyy-MM-dd' });
                  $("#dtEndDate").jqxDateTimeInput({ width: '150px', height: '20px', theme: 'arctic', formatString: 'yyyy-MM-dd' });
                  $("input[type='button']").jqxButton({ theme: 'arctic' });
                  $('#DataTab').jqxTabs({ width: '100%', height: '1000px', position: 'top', theme: 'arctic' });
                  //  var qrySrc = ["按月查询", "按年查询", "按日查询"];
                  $("#cmbSensor1").jqxComboBox({ selectedIndex: 0, width: 180, theme: "arctic", height: 20 });
                  $("#cmbSensor2").jqxComboBox({ selectedIndex: 0, width: 180, theme: "arctic", height: 20 });

                  BindSensor1();
                  BindSensor2();

                  $('#ChartWindow').jqxWindow({
                      height: 850, width: 940,
                      resizable: false, isModal: false, modalOpacity: 0.3, theme: 'arctic', autoOpen: false, maxWidth: 1400, maxHeight: 1000
                  });

              });

              function BindSensor2() {
                  var sensorid = $("#SensorIdValue").val();

                  $.ajax(
                    {
                        type: "POST",
                        contentType: "application/json",
                        url: "WebMethods.aspx/getMatchedSensors",
                        data: "{'sensorid':'" + sensorid + "'}",
                        dataType: "json",
                        success: function (msg) {

                            var ret = $.parseJSON(msg.d);

                            $("#cmbSensor2").jqxComboBox({ source: ret.data, selectedIndex: -1, valueMember: 'SensorID', displayMember: 'SensorName' });

                        }


                    });
              }



              function BindSensor1() {

                  var sensorid = $("#SensorIdValue").val();

                  $.ajax(
                    {
                        type: "POST",
                        contentType: "application/json",
                        url: "WebMethods.aspx/getSensorBySensorID",
                        data: "{'sensorid':'" + sensorid + "'}",
                        dataType: "json",
                        success: function (msg) {

                            var ret = $.parseJSON(msg.d);

                            $("#cmbSensor1").jqxComboBox({ source: ret.data, selectedIndex: 0, disabled: true, valueMember: 'SensorID', displayMember: 'SensorName' });

                        }


                    });


              }
              function Query() {

                  QueryGrid();

              }

              function ProcessPoints(points) {

                  // var p=new Array();

                  var total = 0;
                  var temp = 0;
                  var j = 0;

                  for (var i = 0; i < points.length; i++) {

                      temp = parseFloat(points[i]);
                      if (isNaN(temp)) {

                          // alert(points[i]);

                      }
                      else {
                          total += Math.abs(temp);
                          j++;
                      }


                  }

                  var ave = total / j;
                  return 40 / ave;


              }


              function OnDraw(points, leakPosition, leakMark) {
                  var canvas = document.getElementById('myCanvas');
                  canvas.width = 1300;
                  canvas.height = 800;

                  var context = canvas.getContext('2d');

                  var rate = ProcessPoints(points);



                  context.fillStyle = 'red';
                  var j = parseInt(leakMark);
                  var j1 = 0 - j;
                  var position = parseFloat(leakPosition);

                  context.fillText("漏水点距离A探头" + position + "米", 800, 100);


                  if (j > 0)

                      context.fillText("A探头滞后B探头" + j + "个基点!", 100, 100);
                  //显示标注数据
                  else if (j < 0)

                      context.fillText("A探头超前B探头" + j1.toString() + "个基点!", 100, 100);
                  else if (j == 0)

                      context.fillText("A探头与B探头无偏移!", 100, 100);
                  context.font = 'italic 12pt Calibri';
                  context.fillStyle = 'blue';
                  context.strokeStyle = 'blue';
                  context.lineWidth = 1;
                  context.beginPath();
                  context.moveTo(0, 400);
                  context.lineTo(1300, 400);
                  context.fillText('X轴', 350, 420);

                  context.stroke();
                  context.beginPath();
                  context.moveTo(20, 0);
                  context.lineTo(20, 800);
                  context.stroke();

                  context.fillText('Y轴', 27, 40);

                  context.beginPath();

                  context.moveTo(20, 400 - parseFloat(points[0]) * rate);

                  // alert(points.length);
                  for (var i = 1; i < points.length; i++) {
                      var y = parseFloat(points[i]) * rate;
                      // console.log(y);
                      context.lineTo(i * 15 + 20, 400 - y);

                  }

                  context.lineJoin = 'round';
                  context.stroke();
              }

              function QueryResult(id) {



                  $.ajax(
                    {
                        type: "POST",
                        contentType: "application/json",
                        url: "WebMethods.aspx/getSensorAnalyzeDetailBySensorAnalyzeID",
                        data: "{'sensoranalyzeid':'" + id + "'}",
                        dataType: "json",
                        success: function (msg) {

                            var ret = $.parseJSON(msg.d);
                            var resultstr = ret.data[0].AnalyzeResult;
                            //  alert(resultstr);

                            var points = resultstr.split(",");
                            OnDraw(points, ret.data[0].LeakPosition, ret.data[0].LeakMark);
                        }
                    });



              }


              function print() {


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

                  var sensor1 = $("#SensorIdValue").val();

                  var sensor2 = $("#cmbSensor2").jqxComboBox('val');
                  var begintime = $("#dtBeginDate").jqxDateTimeInput("val");
                  var endtime = $("#dtEndDate").jqxDateTimeInput("val");

                  $.ajax(
                    {
                        type: "POST",
                        contentType: "application/json",
                        url: "WebMethods.aspx/getSensorListByDate",
                        data: "{'sensoraid':'" + sensor1 + "','begincaptime':'" + begintime + "','endcaptime':'" + endtime + "','sensorbid':'" + sensor2 + "'}",
                        dataType: "json",
                        success: function (msg) {

                            var ret = $.parseJSON(msg.d);

                            var source =
                                {
                                    localdata: ret.data,
                                    datatype: "array",
                                    datafields:
                                    [

                                        { name: 'SensorAnalyzeID', type: 'string' },
                                        { name: 'AnalyzeDate', type: 'string' },
                                        { name: 'SensorAName', type: 'string' },
                                        { name: 'SensorBName', type: 'string' },
                                        { name: 'LeakMarkDesc', type: 'string' },
                                        { name: 'ProcessResult', type: 'string' },
                                        { name: 'ProcessDescription', type: 'string' }

                                    ]
                                };
                            var dataAdapter = new $.jqx.dataAdapter(source);

                            $("#historicDataGrid").jqxGrid(
                                {
                                    width: 1500,
                                    autoheight: true,
                                    source: dataAdapter,
                                    columnsresize: true,
                                    theme: 'arctic',
                                    showemptyrow: false,
                                    columns: [
                                      { text: 'id', datafield: 'SensorAnalyzeID', width: 120, hidden: true },
                                      { text: '分析日期', datafield: 'AnalyzeDate', width: 120 },
                                      { text: '传感器A', datafield: 'SensorAName', width: 120 },
                                      { text: '传感器B', datafield: 'SensorBName', width: 180 },
                                      { text: '分析结果', datafield: 'LeakMarkDesc', width: 180 },
                                      { text: '处理结果', datafield: 'ProcessResult', width: 180 },
                                      { text: '处理描述', datafield: 'ProcessDescription', width: 180 }

                                    ]
                                });

                            if (ret.data == null || ret.data.length == 0) {

                                alert("没有可以展示的数据！");
                            }


                            $('#historicDataGrid').on('rowdoubleclick', function (event) {
                                var args = event.args;

                                var boundIndex = args.rowindex;

                                var data = $('#historicDataGrid').jqxGrid('getrowdata', boundIndex);
                                // alert(data.SensorAnalyzeID);
                                $("#ChartWindow").jqxWindow("open");

                                QueryResult(data.SensorAnalyzeID);
                            });



                        }

                    });




              }



           </script>


</head>
<body>
    <div class="SearchOptionPanel" style="width:100%;height:50px;line-height:22px">
    <span style="font-size:12px;float:left">A传感器</span><div style="float:left" id="cmbSensor1"></div>
    <span style="font-size:12px;float:left">B传感器</span><div style="float:left" id="cmbSensor2"></div>
    <span style="font-size:12px;float:left">起始时间</span><div style="float:left" id="dtBeginDate"></div>
    <span style="float:left;font-size:12px">结束时间</span><div style="float:left" id="dtEndDate"></div><input type="button" value="查询" onclick="Query()" /><input type="button" value="打印" onclick="print()" />

    
  </div>
        <div id='DataTab'>
            <ul>
                <li style="margin-left: 30px;">查询列表</li>
                
           </ul>
           
            <div>
                <div id='historicDataGrid' style="width:100%; ">
               </div>
            </div>
          
          <%--  <div>
                 <div id='chartContainer' style="width:100%; height:500px">
                 </div>
            </div>--%>
          
           
        </div>   
        <form id="Form1" runat="server">
        <asp:HiddenField runat="server" ID="SensorIdValue" />
        </form>
          <div id="ChartWindow" style="display:none">
   <div>分析结果</div>
   <div>
      <canvas id="myCanvas"></canvas>
   
   
   
   
   </div>
  
  
   </div>
</body>


</html>
