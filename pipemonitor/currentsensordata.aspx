<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="currentsensordata.aspx.cs" Inherits="pipemonitor.currentsensordata" %>


<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head >
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
    <script type="text/javascript" src="jqwidgets/jqwidgets/jqxprogressbar.js"></script>
    <script type="text/javascript" src="jqwidgets/jqwidgets/jqxlistbox.js"></script>
    <script type="text/javascript" src="jqwidgets/jqwidgets/jqxgrid.js"></script>
    <script type="text/javascript" src="jqwidgets/jqwidgets/jqxgrid.selection.js"></script> 
    <script type="text/javascript" src="jqwidgets/jqwidgets/jqxgrid.columnsresize.js"></script> 
    <script type="text/javascript" src="jqwidgets/jqwidgets/jqxloader.js"></script>
    <script type="text/javascript" src="jqwidgets/jqwidgets/jqxsplitter.js"></script>
    <script type="text/javascript" src="Scripts/canvasjs.min.js"></script>
    <script type="text/javascript" src="Scripts/base64-binary.js"></script>

</head>

<body>

    <div class="SearchOptionPanel" style="width:100%;height:50px;line-height:22px">
        <span style="font-size:12px;float:left">A传感器</span><div style="float:left" id="cmbSensor1"></div>
        <span style="font-size:12px;float:left">B传感器</span><div style="float:left" id="cmbSensor2"></div>
   
        <input type="button" style='float:left' value="立即采样" onclick="Query()" />
        <div style='float:left;margin-left:100px' id='jqxProgress'> </div>  
    </div>

    <div id='DataTab'>
        <ul>
            <li style="margin-left: 30px;">当前分析图表</li>              
       </ul>
       <div id="mainSplitter">  
            <div>
               <div style="height:300px";>
                   <div style="background-color:rgb(224,233,245);height:25px;line-height:25px;">管道信息</div>
                   <div id="PipeInfo" style="margin-top:10px;margin-left:10px;"></div>
               </div>
               <div style="border-top:2px solid #CCCCCC">
                   <div style="background-color:rgb(224,233,245);height:25px;line-height:25px;">分析结果</div>
                   <div id="ResultInfo" style="margin-top:10px;margin-left:10px;"></div>
               </div>
           </div>
           <div>
               <div>
                  
               </div>
               <div>
                 
               </div>
               <div>
                 
               </div>
           </div>
       </div>
   </div> 

    <form id="Form1" runat="server">
        <asp:HiddenField runat="server" ID="SensorIdValue" />
    </form>
</body>
</html>



<script type="text/javascript">

    $(document).ready(function () {

        $("#cmbSensor1").jqxComboBox({ selectedIndex: 0, width: 180, theme: "arctic", height: 20 });
        $("#cmbSensor2").jqxComboBox({ selectedIndex: 0, width: 180, theme: "arctic", height: 20 });
        $("input[type='button']").jqxButton({ theme: 'arctic' });
     
        BindSensor1();
        BindSensor2();

        $('#DataTab').jqxTabs({ width: '100%', height: '1000px', position: 'top', theme: 'arctic' });
        $('#mainSplitter').jqxSplitter({ width: '100%', height: '100%', panels: [{ size: 260}], theme: 'arctic' });

        $('#cmbSensor2').on('change', function (event) {
            var aitem = $("#cmbSensor1").jqxComboBox("getSelectedItem");
            var bitem = $("#cmbSensor2").jqxComboBox("getSelectedItem");
            ShowPipeInfo(aitem.value, bitem.value);
        });

        $("#jqxProgress").jqxProgressBar({ width: 250, height: 20, theme: 'arctic' });
        $("#jqxProgress").hide();

    });



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


    function Query() {
        var aitem = $("#cmbSensor1").jqxComboBox("getSelectedItem");
        var bitem = $("#cmbSensor2").jqxComboBox("getSelectedItem");
        if (bitem == null) {
            alert("请选择B探头！");
            return;
        }

        Sampling(aitem.value);
        Sampling(bitem.value);

        $("#jqxProgress").jqxProgressBar('val', 0);
        $("#jqxProgress").show();

        var interval = setInterval(function () {
            ShowProgress(aitem.value);
            var progressvalue = $("#jqxProgress").val();
            if (progressvalue >= 99) {
                clearInterval(interval);
                $("#jqxProgress").jqxProgressBar('val', 100);
            }
        }, 1000);

    }

    function Sampling(sensorid) {
        $.ajax(
        {
            type: "POST",
            contentType: "application/json",
            url: "Net_Device_DB.aspx/getADNow",
            data: "{'sensorintdeviceID':'" + sensorid + "'}",
            dataType: "json",
            success: function (msg) {
            }
        });
    }

    function ShowProgress(sensorid) {
        $.ajax(
        {
            type: "POST",
            contentType: "application/json",
            url: "Net_Device_DB.aspx/getUploadStatus",
            data: "{'sensorintdeviceID':'" + sensorid + "'}",
            dataType: "json",
            success: function (data) {
                $("#jqxProgress").jqxProgressBar('val', data.d);
            }
        });
    }

    function ShowPipeInfo(aid, bid) {
        var apipe = GetPipeID(aid);
        var bpipe = GetPipeID(bid);

        var pipeid = bpipe[0];
        if (apipe[1] == bpipe[0]) {
            pipeid = apipe[0];
        }

        $.ajax(
        {
            type: "POST",
            contentType: "application/json",
            url: "WebMethods.aspx/getPipeByPipeID",
            data: "{'pipeid':'" + pipeid + "'}",
            dataType: "json",
            success: function (msg) {
                var ret = $.parseJSON(msg.d);

                if (ret.msg == "ok") {
                    var htmlStr = "<p>管道名称： " + ret.data[0].PipeName + "</p><p>管道编号： " + ret.data[0].PipeNo + "</p><p>管道长度： " + ret.data[0].PipeLength
                        + "（米）</p><p>管道半径： " + ret.data[0].PipeSize + "（米）</p><p>管道埋深： " + ret.data[0].PipeDepth + "（米）</p><p>管道材料： " + GetPipeMaterial(ret.data[0].PipeMaterialID)
                        + "</p><p>所属区域： " + GetAreaName(ret.data[0].AreaID) + "</p><p>管道备注： " + ret.data[0].Remark;
                    $("#PipeInfo").html(htmlStr);
                }
            }
        });
    }

    function GetPipeID(sensorid) {
        var pipe = new Array(2);
        $.ajax(
        {
            type: "POST",
            contentType: "application/json",
            url: "WebMethods.aspx/getPipeBySensorID",
            data: "{'sensorid':'" + sensorid + "'}",
            dataType: "json",
            async: false,  //同步加载，会在ajax的success执行完成之后，再执行其它
            success: function (msg) {
                var data = $.parseJSON(msg.d);
                pipe[0] = data[0].PipeID;
                pipe[1] = data[0].PrePipeID;
            }
        });
        return pipe;
    }

    function GetPipeMaterial(pipeMaterialid) {
        var pipeMaterial = null;
        $.ajax(
        {
            type: "POST",
            contentType: "application/json",
            url: "WebMethods.aspx/getPipeMaterialByPipeMaterialID",
            data: "{'pipeMaterialid':'" + pipeMaterialid + "'}",
            dataType: "json",
            async: false,  //同步加载，会在ajax的success执行完成之后，再执行其它
            success: function (msg) {
                var ret = $.parseJSON(msg.d);
                pipeMaterial = ret.data[0].PipeMaterialName;
            }
        });
        return pipeMaterial;
    }

    function GetAreaName(areaid) {
        var areaName = null;
        $.ajax(
        {
            type: "POST",
            contentType: "application/json",
            url: "WebMethods.aspx/getAreaNameByAreaID",
            data: "{'areaid':'" + areaid + "'}",
            dataType: "json",
            async: false,  //同步加载，会在ajax的success执行完成之后，再执行其它
            success: function (msg) {
                var ret = $.parseJSON(msg.d);
                areaName = ret.data[0].AreaName;
            }
        });
        return areaName;
    }


     
</script>