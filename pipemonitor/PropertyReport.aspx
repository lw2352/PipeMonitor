<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PropertyReport.aspx.cs" Inherits="pipemonitor.PropertyReport" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head >
    <title></title>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
	<meta name="viewport" content="initial-scale=1.0, user-scalable=no" />
    <link rel="stylesheet" href="jqwidgets/jqwidgets/styles/jqx.base.css" type="text/css" />
    <link rel="stylesheet" href="jqwidgets/jqwidgets/styles/jqx.arctic.css" type="text/css" />
    <link rel="stylesheet" href="jqwidgets/jqwidgets/styles/jqx.energyblue.css" type="text/css" />
    <link rel="stylesheet" href="jqwidgets/jqwidgets/styles/jqx.arctic.css" type="text/css" />
    <link rel="stylesheet" href="jqwidgets/jqwidgets/styles/jqx.ui-redmond.css" type="text/css" />
    <link rel="stylesheet" href="Styles/main.css" type="text/css" />
    <script type="text/javascript" src="jqwidgets/scripts/jquery-1.11.1.min.js"></script>
    <script type="text/javascript" src="jqwidgets/jqwidgets/jqxcore.js"></script>
    <script type="text/javascript" src="jqwidgets/jqwidgets/jqxmenu.js"></script>
    <script type="text/javascript" src="jqwidgets/jqwidgets/jqxbuttons.js"></script>
  <script type="text/javascript" src="jqwidgets/jqwidgets/jqxsplitter.js"></script>
   
   <script type="text/javascript" src="jqwidgets/jqwidgets/jqxtabs.js"></script>
    <script type="text/javascript" src="jqwidgets/jqwidgets/jqxwindow.js"></script>
    <script type="text/javascript" src="jqwidgets/jqwidgets/jqxinput.js"></script>
    <script type="text/javascript" src="jqwidgets/jqwidgets/jqxdata.js"></script>
    <script type="text/javascript" src="jqwidgets/jqwidgets/jqxdropdownbutton.js"></script>
    <script type="text/javascript" src="jqwidgets/jqwidgets/jqxscrollbar.js"></script>
    <script type="text/javascript" src="jqwidgets/jqwidgets/jqxbuttons.js"></script>
    <script type="text/javascript" src="jqwidgets/jqwidgets/jqxtree.js"></script>
    <script type="text/javascript" src="jqwidgets/jqwidgets/jqxpanel.js"></script>
    <script type="text/javascript" src="jqwidgets/jqwidgets/jqxcombobox.js"></script>
    <script type="text/javascript" src="jqwidgets/jqwidgets/jqxlistbox.js"></script>
    <script type="text/javascript" src="jqwidgets/jqwidgets/jqxnumberinput.js"></script>
    <script type="text/javascript" src="jqwidgets/jqwidgets/globalization/globalize.js"></script>
    <script type="text/javascript" src="jqwidgets/jqwidgets/jqxgrid.js"></script>
    <script type="text/javascript" src="jqwidgets/jqwidgets/jqxgrid.selection.js"></script> 
    <script type="text/javascript" src="jqwidgets/jqwidgets/jqxgrid.columnsresize.js"></script> 
    <script type="text/javascript" src="jqwidgets/jqwidgets/jqxlistbox.js"></script>
    <script type="text/javascript" src="jqwidgets/jqwidgets/jqxcheckbox.js"></script>
    <script type="text/javascript" src="jqwidgets/jqwidgets/jqxexpander.js"></script>
    <script type="text/javascript" src="jqwidgets/jqwidgets/jqxdata.export.js"></script> 
    <script type="text/javascript" src="jqwidgets/jqwidgets/jqxgrid.export.js"></script> 
    <script type="text/javascript" src="jqwidgets/jqwidgets/jqxgrid.sort.js"></script> 
    <script type="text/javascript" src="jqwidgets/jqwidgets/jqxgrid.grouping.js"></script>
    <script type="text/javascript" src="jqwidgets/jqwidgets/jqxgrid.aggregates.js"></script>
    <script type="text/javascript" src="http://api.map.baidu.com/api?v=2.0&ak=S2WhnhMHOfqn4iQF8CcD7Ajl"></script>
  
      <script type="text/javascript">
          var map = null;
          var WatermeterImage = null;
          var SensorImage = null;
          $(document).ready(function () {
              $('#mainSplitter').jqxSplitter({ width: "100%", theme: 'arctic', height: '1000', panels: [{ size: 400, collapsible: false}] });
              $('#RegionTree').jqxTree({ height: '100%', width: '100%', theme: 'arctic' });
              $("#watermeterPanel").jqxExpander({ width: '100%', theme: 'arctic' });
              $("#sensorPanel").jqxExpander({ width: '100%', theme: 'arctic' });
              $("#pipePanel").jqxExpander({ width: '100%', theme: 'arctic' });
              $("#FirePlugPanel").jqxExpander({ width: '100%', theme: 'arctic' });
              $("#ValvePanel").jqxExpander({ width: '100%', theme: 'arctic' });
              $("input[type='button']").jqxButton({ theme: 'arctic' });
              BindRegion("RegionTree");
            
              $('#mapWindow').jqxWindow({
                  height: 600, width: 800,
                  resizable: false, isModal: true, modalOpacity: 0.3, theme: 'arctic', autoOpen: false
              });
              InitMap();
              SensorImage = new Image();

           
              SensorImage.src = 'Images/sensor22.png';

              WatermeterImage = new Image();

              WatermeterImage.src = 'Images/watermeter22.png';
          });
        
          function DrawDevice() {
           
              var startPoints =$('#mapWindow').attr("data-startpos").split(",");
              var endPoints = $('#mapWindow').attr("data-endpos").split(",");
              var rate = $('#mapWindow').attr("data-rate");
              var second = $('#mapWindow').attr("data-issecondhead");
              var StartPos = new BMap.Point(parseFloat(startPoints[0]), parseFloat(startPoints[1]));
              var EndPos = new BMap.Point(parseFloat(endPoints[0]), parseFloat(endPoints[1]));

              var type = $('#mapWindow').attr("data-type");

              if (type == "1") {

                  DrawPipe(StartPos, EndPos);
              }
              else if (type == "2") {
                  DrawWatermeter(StartPos, EndPos, rate);
              }
              else if (type == "3") {

                  DrawSensor(StartPos, EndPos, second);
              }
//              $('#mapWindow').attr("data-type", 1);
//              $('#mapWindow').attr("data-startpos", data.StartLocation);
//              $('#mapWindow').attr("data-endpos", data.EndLocation);

          }


          function InitMap() {

              map = new BMap.Map("allmap", { enableMapClick: false, minZoom: 16, maxZoom: 19 });
              map.centerAndZoom(new BMap.Point(114.360122, 30.521608), 19);
            //  map.addControl(new BMap.MapTypeControl());
              map.setCurrentCity("武汉");
              map.enableScrollWheelZoom(true);

              map.addEventListener("moving", function (e) {

                  DrawDevice();

                  return false;
              });

              map.addEventListener("dragging", function (e) {

                  DrawDevice();

                  return false;
              });
              map.addEventListener("zoomstart", function (e) {

                  $("#OverlayCanvas").fadeOut(500);

                  return false;
              });
              map.addEventListener("zoomend", function (e) {

                  DrawDevice();
                  $("#OverlayCanvas").fadeIn(100);
                  return false;
              });

          }
          function BindRegion() {

              $.ajax(
            {
                type: "POST",
                contentType: "application/json",
                url: "WebMethods.aspx/getAllArea",
                data: "{}",
                dataType: "json",
                success: function (msg) {

                    //   console.log(msg.d);
                    var RegionData = $.parseJSON(msg.d);
                    CurrentAreas = RegionData.data;
                    var source =
                                    {
                                        datatype: "json",
                                        datafields: [
                                            { name: 'AreaID' },
                                            { name: 'ParentAreaID' },
                                            { name: 'AreaName' }

                                        ],
                                        id: 'AreaID',
                                        localdata: RegionData.data
                                    };

                    var dataAdapter = new $.jqx.dataAdapter(source);

                    dataAdapter.dataBind();

                    var records = dataAdapter.getRecordsHierarchy('AreaID', 'ParentAreaID', 'items', [{ name: 'AreaName', map: 'label' }, { name: 'AreaID', map: 'value'}]);
                    $('#RegionTree').jqxTree({ source: records });
                    FixTree();
                    $('#RegionTree').on('select', function (event) {
                        var args = event.args;
                        var item = $('#RegionTree').jqxTree('getItem', args.element);
                        if (item.label == "传感器") {
                            $("#sensorPanel").show();
                            BindSensorGrid(item.value.substring(3));
                            $("#watermeterPanel").hide();
                            $("#pipePanel").hide();
                            $("#FirePlugPanel").hide();
                            $("#ValvePanel").hide();
                        }
                        else if (item.label == "水表") {
                            $("#watermeterPanel").show();
                            BindWatermeterGrid(item.value.substring(2));
                            $("#sensorPanel").hide();
                            $("#pipePanel").hide();
                            $("#FirePlugPanel").hide();
                            $("#ValvePanel").hide();
                        }
                        else if (item.label == "管道") {
                            $("#pipePanel").show();
                            BindPipeGrid(item.value.substring(2));
                            $("#sensorPanel").hide();
                            $("#watermeterPanel").hide();
                            $("#FirePlugPanel").hide();
                            $("#ValvePanel").hide();
                        }
                        else if (item.label == "消防栓") {
                            $("#FirePlugPanel").show();
                            BindFirePlugGrid(item.value.substring(2));
                            $("#sensorPanel").hide();
                            $("#watermeterPanel").hide();
                            $("#ValvePanel").hide();
                            $("#pipePanel").hide();
                         
                        }
                        else if (item.label == "阀门") {
                            $("#ValvePanel").show();
                            BindValveGrid(item.value.substring(2));
                            $("#sensorPanel").hide();
                            $("#watermeterPanel").hide();
                            $("#pipePanel").hide();
                            $("#FirePlugPanel").hide();
                        }
                        else {
                            $("#pipePanel").show();
                            $("#sensorPanel").show();
                            $("#watermeterPanel").show();
                            $("#FirePlugPanel").show();
                            $("#ValvePanel").show();
                            BindPipeGrid(item.value);
                            BindWatermeterGrid(item.value);
                            BindSensorGrid(item.value);
                            BindFirePlugGrid(item.value);
                            BindValveGrid(item.value);

                        }
                    });
                }
            });


          }


          function print() {

              var item = $('#RegionTree').jqxTree('getSelectedItem');



              var gridContent = "";
              var waterContent = "";
              var pipeContent = "";
              var fireplugContent = "";
              var valveContent = "";
              if (item.value.indexOf("传感器") == 0) {
                  gridContent = "<div>传感器</div>" + $("#sensorGrid").jqxGrid('exportdata', 'html');

              }
              else if (item.value.indexOf("水表") == 0) {

                  waterContent = "<div>水表</div>" + $("#watermeterGrid").jqxGrid('exportdata', 'html');
              }

              else if (item.value.indexOf("管道") == 0) {
                  pipeContent = "<div>管道</div>" + $("#pipeGrid").jqxGrid('exportdata', 'html');
              }
              else if (item.value.indexOf("消防栓") == 0) {
                  fireplugContent = "<div>消防栓</div>" + $("#FirePlugGrid").jqxGrid('exportdata', 'html');
              } else if (item.value.indexOf("阀门") == 0) {
                  valveContent = "<div>阀门</div>" + $("#ValveGrid").jqxGrid('exportdata', 'html');
              }

              else {
                  gridContent = "<div>传感器</div>" + $("#sensorGrid").jqxGrid('exportdata', 'html');
                  waterContent = "<div>水表</div>" + $("#watermeterGrid").jqxGrid('exportdata', 'html');
                  pipeContent = "<div>管道</div>" + $("#pipeGrid").jqxGrid('exportdata', 'html');
                  fireplugContent = "<div>消防栓</div>" + $("#FirePlugGrid").jqxGrid('exportdata', 'html');
                  valveContent = "<div>阀门</div>" + $("#ValveGrid").jqxGrid('exportdata', 'html');
              }


              var newWindow = window.open('', '', 'width=800, height=500'),
                document = newWindow.document.open(),
                pageContent =
                    '<!DOCTYPE html>\n' +
                    '<html>\n' +
                    '<head>\n' +
                    '<meta charset="utf-8" />\n' +
                    '<title></title>\n' +
                    '</head>\n' +
                    '<body>\n' + gridContent + waterContent + pipeContent +fireplugContent+valveContent+ '\n</body>\n</html>';
              document.write(pageContent);
              document.close();
              newWindow.print();

          }

          function DrawPipe(startPos, endPos) {

              var startPixel = map.pointToPixel(startPos);

              var zoom = map.getZoom();
              var linewidth = 12;
              var harfwidth = 6;
              var radious = 7;
              if (zoom == 18) {
                  linewidth = 12;
                  harfwidth = 6;
                  radious = 7;

              }
              else if (zoom == 17) {
                  linewidth = 8;
                  harfwidth = 4;
                  radious = 5;
              }
              else if (zoom == 16) {
                  linewidth = 4;
                  harfwidth = 2;
                  radious = 3;
              }

              var startX = startPixel.x;
              var startY = startPixel.y;

              var endPixel = map.pointToPixel(endPos);
              var endX = endPixel.x;
              var endY = endPixel.y;

              var canvas = document.getElementById('OverlayCanvas');
              var context = canvas.getContext('2d');
              context.clearRect(0, 0, canvas.width, canvas.height);
              context.lineWidth = linewidth;
              context.beginPath();
              context.moveTo(startX, startY);
              context.lineTo(endX, endY);
              context.lineJoin = 'miter';
              var grd = null;
              if (endX != startX) {
                  var tg = (endY - startY) / (endX - startX);
                  var angle = Math.atan(tg);
                  var offsetx = harfwidth * Math.sin(angle);
                  var offsety = harfwidth * Math.cos(angle);
                  grd = context.createLinearGradient(startX + offsetx, startY - offsety, startX - offsetx, startY + offsety);
              }
              else {
                  grd = context.createLinearGradient(startX - harfwidth, startY, startX + harfwidth, startY);
              }

              grd.addColorStop(0, 'rgb(67,66,64)');
              grd.addColorStop(0.25, 'rgb(159,158,156)');
              grd.addColorStop(0.5, 'rgb(254,254,254)');
              grd.addColorStop(0.75, 'rgb(159,158,156)');
              grd.addColorStop(1, 'rgb(67,66,64)');

              context.strokeStyle = grd;
              context.stroke();
          }

          function ClearCanvas() {

              var canvas = document.getElementById('OverlayCanvas');
              var context = canvas.getContext('2d');
              context.clearRect(0, 0, canvas.width, canvas.height);
          }
          function BindPipeGrid(areaid) {

              $.ajax(
                    {
                        type: "POST",
                        contentType: "application/json",
                        url: "WebMethods.aspx/getPipeByAreaIDIncludeSubAreas",
                        data: "{'areaid':'" + areaid + "'}",
                        dataType: "json",
                        success: function (msg) {

                            var ret = $.parseJSON(msg.d);

                            var source =
                                {
                                    localdata: ret.data,
                                    datatype: "array",
                                    datafields:
                                    [
                                      { name: 'PipeID', type: 'number' },
                                      { name: 'PipeName', type: 'string' },
                                      { name: 'PipeNo', type: 'string' },
                                      { name: 'AreaName', type: 'string' },
                                      { name: 'PipeMaterialName', type: 'string' },
                                      { name: 'Remark', type: 'string' },
                                      { name: 'UserName', type: 'string' },
                                      { name: 'PipeSized', type: 'string' },
                                      { name: 'PipeLength', type: 'number' },
                                      { name: 'PipeDepth', type: 'string' },
                                      { name: 'StartLocation', type: 'string' },
                                      { name: 'EndLocation', type: 'string' }


                                    ]
                                };
                            var dataAdapter = new $.jqx.dataAdapter(source);

                            var groupsrenderer = function (text, group, expanded, data) {

                                var number = dataAdapter.formatNumber(group, data.groupcolumn.cellsformat);
                                var text = data.groupcolumn.text + ': ' + number;
                                if (data.subItems.length > 0) {
                                    var aggregate = this.getcolumnaggregateddata("PipeLength", ['sum'], true, data.subItems);
                                }
                                else {
                                    var rows = new Array();
                                    var getRows = function (group, rows) {
                                        if (group.subGroups.length > 0) {
                                            for (var i = 0; i < group.subGroups.length; i++) {
                                                getRows(group.subGroups[i], rows);
                                            }
                                        }
                                        else {
                                            for (var i = 0; i < group.subItems.length; i++) {
                                                rows.push(group.subItems[i]);
                                            }
                                        }
                                    }
                                    getRows(data, rows);
                                    var aggregate = this.getcolumnaggregateddata("PipeLength", ['sum'], true, rows);
                                }

                                return '<div  style="position: absolute;"><span>' + text + ', </span>' + '<span >' + "总长" + ' (' + aggregate.sum + '米)' + '</span></div>';


                            }



                            $("#pipeGrid").jqxGrid(
                                {
                                    width: "90%",
                                    autoheight: true,
                                    source: dataAdapter,
                                    columnsresize: true,
                                    theme: 'arctic',
                                    showemptyrow: false,
                                    groupable: true,
                                    showstatusbar: true,
                                    showaggregates: true,
                                    groupsrenderer: groupsrenderer,
                                    columns: [

                                      { text: '管道名称', datafield: 'PipeName' },
                                      { text: '管道编号', datafield: 'PipeNo' },
                                       { text: '管材', datafield: 'PipeMaterialName' },

                                        { text: '所属区域', datafield: 'AreaName' },
                                      { text: '管径', datafield: 'PipeSized' },
                                        { text: '埋深', datafield: 'PipeDepth' },
                                          { text: '管长', datafield: 'PipeLength' }
                                          ,
                                                { text: '管理员', datafield: 'UserName' },
                                                      { text: '备注', datafield: 'Remark' }


                                    ],
                                    groups: ['PipeMaterialName']
                                });

                                var h = $("#watermeterPanel").height() + $("#sensorPanel").height() + $("#pipePanel").height() + $("#FirePlugPanel").height() + $("#ValvePanel").height();
                            if (h < 1000) {
                                h = 1000;
                            }
                            $('#mainSplitter').jqxSplitter({ width: "100%", theme: 'arctic', height: h, panels: [{ size: 400, collapsible: false}] });

                            $('#pipeGrid').on('rowdoubleclick', function (event) {
                                var args = event.args;

                                var boundIndex = args.rowindex;
                                $('#mapWindow').jqxWindow('open');
                                ClearCanvas();
                                var data = $('#pipeGrid').jqxGrid('getrowdata', boundIndex);

                                var startPoints = data.StartLocation.split(",");

                                var StartPos = new BMap.Point(parseFloat(startPoints[0]), parseFloat(startPoints[1]));
                                $('#mapWindow').attr("data-type", 1);
                                $('#mapWindow').attr("data-startpos", data.StartLocation);
                                $('#mapWindow').attr("data-endpos", data.EndLocation);

                                setTimeout("adjustMap()", 1000);


                            });

                        }

                    });

                }

                function adjustMap() {
                    var startPoints = $('#mapWindow').attr("data-startpos").split(",");

                    var StartPos = new BMap.Point(parseFloat(startPoints[0]), parseFloat(startPoints[1]));
                    map.centerAndZoom(StartPos, 19);
                    DrawDevice()
                   
                }

                 function GetSensorPos(SensorID) {
                     $('#mapWindow').jqxWindow('open');
                     $.ajax(
                    {
                        type: "POST",
                        contentType: "application/json",
                        url: "PropertyReport.aspx/GetSensorPos",
                        data: "{'SensorID':'" + SensorID + "'}",
                        dataType: "json",
                        success: function (msg) {

                            var ret = $.parseJSON(msg.d);
                            if (ret.data != null && ret.data.length > 0) {

                                var startPoints = ret.data[0].StartLocation.split(",");
                                var endPoints = ret.data[0].EndLocation.split(",");
                                var StartPos = new BMap.Point(parseFloat(startPoints[0]), parseFloat(startPoints[1]));
                                var EndPos = new BMap.Point(parseFloat(endPoints[0]), parseFloat(endPoints[1]));


                                $('#mapWindow').attr("data-type", 3);
                                $('#mapWindow').attr("data-startpos", ret.data[0].StartLocation);
                                $('#mapWindow').attr("data-endpos", ret.data[0].EndLocation);
                                $('#mapWindow').attr("data-issecondhead", ret.data[0].SencondHead);


                              
                                var pos = StartPos;
                                if (ret.data[0].SencondHead == "1") {

                                    pos = EndPos;
                                }
                                map.centerAndZoom(pos, 19);
                            
                                DrawDevice();
                               
                            }
                        }
                    });

                 }

                 function GetWatermeterPos(WatermeterID) {
                     $('#mapWindow').jqxWindow('open');
                     $.ajax(
                    {
                        type: "POST",
                        contentType: "application/json",
                        url: "PropertyReport.aspx/GetWatermeterPos",
                        data: "{'WatermeterID':'" + WatermeterID + "'}",
                        dataType: "json",
                        success: function (msg) {

                            var ret = $.parseJSON(msg.d);
                            if (ret.data != null && ret.data.length > 0) {

                                var startPoints = ret.data[0].StartLocation.split(",");
                                var endPoints = ret.data[0].EndLocation.split(",");
                                var StartPos = new BMap.Point(parseFloat(startPoints[0]), parseFloat(startPoints[1]));
                                var EndPos = new BMap.Point(parseFloat(endPoints[0]), parseFloat(endPoints[1]));


                                $('#mapWindow').attr("data-type", 2);
                                $('#mapWindow').attr("data-startpos", ret.data[0].StartLocation);
                                $('#mapWindow').attr("data-endpos", ret.data[0].EndLocation);
                                $('#mapWindow').attr("data-rate", ret.data[0].Position);


                              
                                var startPixel = map.pointToPixel(StartPos);
                                var endPixel = map.pointToPixel(EndPos);

                                var position = parseFloat(ret.data[0].Position);
                                var deltax = endPixel.x - startPixel.x;
                                var deltay = endPixel.y - startPixel.y;

                                var watermeterx = parseInt(startPixel.x + deltax * position);
                                var watermetery = parseInt(startPixel.y + deltay * position);
                                var pixel = new BMap.Pixel(watermeterx, watermetery);
                                var location = map.pixelToPoint(pixel)
                                map.centerAndZoom(location, 19);

                               DrawDevice();

                            }
                        }
                    });
                 }

                 function DrawSensor(startPos, endPos, second) {
                   
                     var pos = startPos;
                     if (second == "1") {
                       
                         pos = endPos;
                     }
                     var zoom = map.getZoom();
                     var width = 20;
                     var harfwidth = 10;
                     if (zoom == 18) {
                         width = 20;
                         harfwidth = 10;
                     }
                     else if (zoom == 17) {
                         width = 12;
                         harfwidth = 6;
                     }
                     else if (zoom == 16) {
                         width = 8;
                         harfwidth = 4;
                     }
                     var startPixel = map.pointToPixel(pos);
                  
                     var canvas = document.getElementById('OverlayCanvas');
                     var context = canvas.getContext('2d');
                     context.clearRect(0, 0, canvas.width, canvas.height);
                     context.drawImage(SensorImage, startPixel.x - harfwidth, startPixel.y - harfwidth, width, width);
                 }



                 function DrawWatermeter(startPos, endPos, rate) {
                     var startPixel = map.pointToPixel(startPos);
                     var endPixel = map.pointToPixel(endPos);

                     var position = parseFloat(rate);
                     var zoom = map.getZoom();
                     var width = 20;
                     var harfwidth = 10;
                     if (zoom == 18) {
                         width = 20;
                         harfwidth = 10;
                     }
                     else if (zoom == 17) {
                         width = 12;
                         harfwidth = 6;
                     }
                     else if (zoom == 16) {
                         width = 8;
                         harfwidth = 4;
                     }
                  

                     var deltax = endPixel.x - startPixel.x;
                     var deltay = endPixel.y - startPixel.y;

                     var watermeterx = parseInt(startPixel.x + deltax * position);
                     var watermetery = parseInt(startPixel.y + deltay * position);
                     var pixel = new BMap.Pixel(watermeterx, watermetery);
                     var location = map.pixelToPoint(pixel)
                   
                     var canvas = document.getElementById('OverlayCanvas');
                     var context = canvas.getContext('2d');
                     context.clearRect(0, 0, canvas.width, canvas.height);
                     context.drawImage(WatermeterImage, watermeterx - harfwidth, watermetery - harfwidth, width, width);
                   

                 }


          function BindWatermeterGrid(areaid) {

              $.ajax(
                    {
                        type: "POST",
                        contentType: "application/json",
                        url: "WebMethods.aspx/GetWatermetersIncludeSubAreas",
                        data: "{'areaid':'" + areaid + "'}",
                        dataType: "json",
                        success: function (msg) {

                            var ret = $.parseJSON(msg.d);

                            var source =
                                {
                                    localdata: ret.data,
                                    datatype: "array",
                                    datafields:
                                    [
                                     { name: 'WaterMeterID', type: 'number' },
                                        { name: 'WaterMeterName', type: 'string' },
                                         { name: 'AreaName', type: 'string' },
                                        { name: 'WaterMeterNo', type: 'string' },
                                        { name: 'WaterMeterType', type: 'string' },
                                        { name: 'Remark', type: 'string' },
                                        { name: 'UserName', type: 'string' },
                                        { name: 'Level', type: 'string' }


                                    ]
                                };
                            var dataAdapter = new $.jqx.dataAdapter(source);

                            $("#watermeterGrid").jqxGrid(
                                {
                                    width: "90%",
                                    autoheight: true,
                                    source: dataAdapter,
                                    columnsresize: true,
                                    theme: 'arctic',
                                    showemptyrow: false,
                                    columns: [

                                      { text: '水表名称', datafield: 'WaterMeterName' },
                                      { text: '水表编号', datafield: 'WaterMeterNo' },
                                       { text: '水表类型', datafield: 'WaterMeterType' },
                                      { text: '水表级别', datafield: 'Level' },
                                      { text: '所属区域', datafield: 'AreaName' },
                                      { text: '备注', datafield: 'Remark' }


                                    ]
                                });

                                var h = $("#watermeterPanel").height() + $("#sensorPanel").height() + $("#pipePanel").height() + $("#FirePlugPanel").height() + $("#ValvePanel").height();
                            if (h < 1000) {
                                h = 1000;
                            }
                            $('#mainSplitter').jqxSplitter({ width: "100%", theme: 'arctic', height: h, panels: [{ size: 400, collapsible: false}] });

                            $('#watermeterGrid').on('rowdoubleclick', function (event) {
                                var args = event.args;

                                var boundIndex = args.rowindex;

                                var data = $('#watermeterGrid').jqxGrid('getrowdata', boundIndex);

                                GetWatermeterPos(data.WaterMeterID);
                            });
                        }

                    });
          }

          function BindFirePlugGrid(areaid) {


              $.ajax(
                    {
                        type: "POST",
                        contentType: "application/json",
                        url: "WebMethods.aspx/getFireplugsByAreaIDIncludeSubAreas",
                        data: "{'areaid':'" + areaid + "'}",
                        dataType: "json",
                        success: function (msg) {

                            var ret = $.parseJSON(msg.d);

                            var source =
                                {
                                    localdata: ret.data,
                                    datatype: "array",
                                    datafields:
                                    [
                                      { name: 'DeviceID', type: 'number' },
                                        { name: 'DeviceName', type: 'string' },
                                     
                                          { name: 'AreaName', type: 'string' },
                                   
                                        { name: 'Remark', type: 'string' },
                                    


                                    ]
                                };
                            var dataAdapter = new $.jqx.dataAdapter(source);

                            $("#FirePlugGrid").jqxGrid(
                                {
                                    width: "90%",
                                    autoheight: true,
                                    source: dataAdapter,
                                    columnsresize: true,
                                    theme: 'arctic',
                                    showemptyrow: false,
                                    columns: [

                                      { text: '消防栓名称', datafield: 'DeviceName' },
                                    
                                   
                                    
                                       { text: '所属区域', datafield: 'AreaName' },
                                      { text: '备注', datafield: 'Remark' }


                                    ]
                                });

                                var h = $("#watermeterPanel").height() + $("#sensorPanel").height() + $("#pipePanel").height() + $("#FirePlugPanel").height() + $("#ValvePanel").height();
                            if (h < 1000) {
                                h = 1000;
                            }
                            $('#mainSplitter').jqxSplitter({ width: "100%", theme: 'arctic', height: h, panels: [{ size: 400, collapsible: false}] });
                           
                        }

                    });


          }

          function BindValveGrid(areaid) {


              $.ajax(
                    {
                        type: "POST",
                        contentType: "application/json",
                        url: "WebMethods.aspx/getValvesByAreaIDIncludeSubAreas",
                        data: "{'areaid':'" + areaid + "'}",
                        dataType: "json",
                        success: function (msg) {

                            var ret = $.parseJSON(msg.d);

                            var source =
                                {
                                    localdata: ret.data,
                                    datatype: "array",
                                    datafields:
                                    [
                                      { name: 'DeviceID', type: 'number' },
                                        { name: 'DeviceName', type: 'string' },

                                          { name: 'AreaName', type: 'string' },

                                        { name: 'Remark', type: 'string' },



                                    ]
                                };
                            var dataAdapter = new $.jqx.dataAdapter(source);

                            $("#ValveGrid").jqxGrid(
                                {
                                    width: "90%",
                                    autoheight: true,
                                    source: dataAdapter,
                                    columnsresize: true,
                                    theme: 'arctic',
                                    showemptyrow: false,
                                    columns: [

                                      { text: '阀门名称', datafield: 'DeviceName' },



                                       { text: '所属区域', datafield: 'AreaName' },
                                      { text: '备注', datafield: 'Remark' }


                                    ]
                                });

                            var h = $("#watermeterPanel").height() + $("#sensorPanel").height() + $("#pipePanel").height() + $("#FirePlugPanel").height() + $("#ValvePanel").height();
                            if (h < 1000) {
                                h = 1000;
                            }
                            $('#mainSplitter').jqxSplitter({ width: "100%", theme: 'arctic', height: h, panels: [{ size: 400, collapsible: false}] });

                        }

                    });


          }

          function BindSensorGrid(areaid) {


              $.ajax(
                    {
                        type: "POST",
                        contentType: "application/json",
                        url: "WebMethods.aspx/getSensorsByAreaIDIncludeSubAreas",
                        data: "{'areaid':'" + areaid + "'}",
                        dataType: "json",
                        success: function (msg) {

                            var ret = $.parseJSON(msg.d);
                         
                            var source =
                                {
                                    localdata: ret.data,
                                    datatype: "array",
                                    datafields:
                                    [
                                      { name: 'SensorID', type: 'number' },
                                        { name: 'SensorName', type: 'string' },
                                        { name: 'SensorNo', type: 'string' },
                                          { name: 'AreaName', type: 'string' },
                                     { name: 'SensorType', type: 'string' },
                                        { name: 'Remark', type: 'string' },
                                        { name: 'UserName', type: 'string' }


                                    ]
                                };
                            var dataAdapter = new $.jqx.dataAdapter(source);

                            $("#sensorGrid").jqxGrid(
                                {
                                    width: "90%",
                                    autoheight: true,
                                    source: dataAdapter,
                                    columnsresize: true,
                                    theme: 'arctic',
                                    showemptyrow: false,
                                    columns: [

                                      { text: '传感器名称', datafield: 'SensorName' },
                                      { text: '传感器编号', datafield: 'SensorNo' },
                                       { text: '传感器类型', datafield: 'SensorType' },
                                      { text: '管理员', datafield: 'UserName' },
                                       { text: '所属区域', datafield: 'AreaName' },
                                      { text: '备注', datafield: 'Remark' }


                                    ]
                                });

                         var h = $("#watermeterPanel").height() + $("#sensorPanel").height() + $("#pipePanel").height() + $("#FirePlugPanel").height() + $("#ValvePanel").height();
                            if (h < 1000) {
                                h = 1000;
                            }
                            $('#mainSplitter').jqxSplitter({ width: "100%", theme: 'arctic', height: h, panels: [{ size: 400, collapsible: false}] });
                            $('#sensorGrid').on('rowdoubleclick', function (event) {
                                var args = event.args;

                                var boundIndex = args.rowindex;

                                var data = $('#sensorGrid').jqxGrid('getrowdata', boundIndex);

                                GetSensorPos(data.SensorID);
                            });
                        }

                    });
          }

          function FixTree() {
              var items = $('#RegionTree').jqxTree('getItems');

              for (var i = 0; i < items.length; i++) {
                 if($(items[i].element).find("li").length==0){
                     if (items[i].label != "传感器" && items[i].label != "水表" && items[i].label != "管道" && items[i].label != "消防栓" && items[i].label != "阀门") {
                         $('#RegionTree').jqxTree('addTo', { label: '传感器', value: '传感器' + items[i].value }, items[i].element);
                         $('#RegionTree').jqxTree('addTo', { label: '水表', value: '水表' + items[i].value }, items[i].element);
                         $('#RegionTree').jqxTree('addTo', { label: '管道', value: '管道' + items[i].value }, items[i].element);
                         $('#RegionTree').jqxTree('addTo', { label: '消防栓', value: '消防栓' + items[i].value }, items[i].element);
                         $('#RegionTree').jqxTree('addTo', { label: '阀门', value: '阀门' + items[i].value }, items[i].element);
                     }
                  }
              }

              RefreshCount();

          }

          function RefreshSensorCount(item) {

              $.ajax(
                    {
                        type: "POST",
                        contentType: "application/json",
                        url: "WebMethods.aspx/getSensorsByAreaIDIncludeSubAreas",
                        data: "{'areaid':'" + item.value.substring(3) + "'}",
                        dataType: "json",
                        success: function (msg) {

                            var ret = $.parseJSON(msg.d);

                            var id = $(item.element).attr("id");
                            if (ret.data != null) {
                                $("#" + id + "> div").html("传感器(" + ret.data.length + ")");
                            }
                        }


                    });


          }
          function RefreshPipeCount(item) {

              $.ajax(
                    {
                        type: "POST",
                        contentType: "application/json",
                        url: "WebMethods.aspx/getPipeByAreaIDIncludeSubAreas",
                        data: "{'areaid':'" + item.value.substring(2) + "'}",
                        dataType: "json",
                        success: function (msg) {

                            var ret = $.parseJSON(msg.d);

                            var id = $(item.element).attr("id");
                            if (ret.data != null) {
                                $("#" + id + "> div").html("管道(" + ret.data.length + ")");
                            }
                        }


                    });


          }

          function RefreshFirePlugCount(item) {

              $.ajax(
                    {
                        type: "POST",
                        contentType: "application/json",
                        url: "WebMethods.aspx/getFireplugsByAreaIDIncludeSubAreas",
                        data: "{'areaid':'" + item.value.substring(2) + "'}",
                        dataType: "json",
                        success: function (msg) {

                            var ret = $.parseJSON(msg.d);

                            var id = $(item.element).attr("id");
                            if (ret.data != null) {
                                $("#" + id + "> div").html("消防栓(" + ret.data.length + ")");
                            }
                        }


                    });


              }

                function RefreshValveCount(item) {

                    $.ajax(
                    {
                        type: "POST",
                        contentType: "application/json",
                        url: "WebMethods.aspx/getValvesByAreaIDIncludeSubAreas",
                        data: "{'areaid':'" + item.value.substring(2) + "'}",
                        dataType: "json",
                        success: function (msg) {

                            var ret = $.parseJSON(msg.d);

                            var id = $(item.element).attr("id");
                            if (ret.data != null) {
                                $("#" + id + "> div").html("阀门(" + ret.data.length + ")");
                            }
                        }


                    });


                }

          function RefreshWatermeterCount(item) {
              $.ajax(
                    {
                        type: "POST",
                        contentType: "application/json",
                        url: "WebMethods.aspx/GetWatermetersIncludeSubAreas",
                        data: "{'areaid':'" + item.value.substring(2) + "'}",
                        dataType: "json",
                        success: function (msg) {

                            var ret = $.parseJSON(msg.d);

                            var id = $(item.element).attr("id");
                            if (ret.data != null) {
                                $("#" + id + "> div").html("水表(" + ret.data.length + ")");
                            }
                        }


                    });

          }
          function RefreshCount() {

              var items = $('#RegionTree').jqxTree('getItems');

              for (var i = 0; i < items.length; i++) {

                  if (items[i].label.indexOf("传感器") == 0) {
                      RefreshSensorCount(items[i]);

                  }
                  else if (items[i].label.indexOf("水表") == 0) {

                      RefreshWatermeterCount(items[i]);
                  }
                  else if (items[i].label.indexOf("管道") == 0) {

                      RefreshPipeCount(items[i]);
                  }
                  else if (items[i].label.indexOf("消防栓") == 0) {

                      RefreshFirePlugCount(items[i]);
                  }
                  else if (items[i].label.indexOf("阀门") == 0) {

                      RefreshValveCount(items[i]);
                  }
              }


          }
    </script>
</head>
<body>

     <div id='jqxWidget'>
        <div id="mainSplitter">
            <div class="splitter-panel">
              <div id="RegionTree"></div>
                </div>
            <div class="splitter-panel">
                <input type="button" value="打印" onclick="print()"/>
              <div id='sensorPanel'>
           
                   <div>
                      传感器
                    </div>
               <div>
               <div id="sensorGrid"></div>
              </div>
             </div>

             <div id='watermeterPanel'>
               <div>
                  水表
                    </div>
               <div>
                 <div id="watermeterGrid"></div>
              </div>
             </div>

              <div id='pipePanel'>
               <div>
                  管道
                    </div>
               <div>
                 <div id="pipeGrid"></div>
              </div>
             </div>
               <div id='FirePlugPanel'>
                    <div>
                        消防栓
                    </div>
               <div>
                 <div id="FirePlugGrid"></div>
               </div>
             </div>

                 <div id='ValvePanel'>
                    <div>
                        阀门
                    </div>
               <div>
                 <div id="ValveGrid">
                 
                 </div>
               </div>
             </div>
               
             </div>
        </div>
    </div>
    <form id="form1" runat="server">
  
    </form>

    <div id="mapWindow">
       <div>地图</div>
       <div>
         <div style="position:relative;left:0px;top:0px">
            <div id="allmap"  style="width:780px;height:560px;overflow:hidden;"></div>
            <canvas id="OverlayCanvas" width="780" height="560"   style="pointer-events:none;position:absolute;left:0px;top:0px;"></canvas>
         </div>
       </div>
    
    </div>
</body>
</html>
