<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="HistoricExceptionReport.aspx.cs" Inherits="pipemonitor.HistoricExceptionReport" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head >
    <title></title>
    
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
       <script type="text/javascript" src="jqwidgets/jqwidgets/jqxtree.js"></script>
  
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
    <script type="text/javascript" src="Scripts/InfoMaintain.js"></script> 


     <script type="text/javascript">
         $(document).ready(function () {
             $('#DataTab').jqxTabs({ width: '100%', height: '1000px', position: 'top', theme: 'arctic' });
             $("#btnArea").jqxDropDownButton({ width: 120, height: 18, theme: 'arctic' });
             $('#trArea').jqxTree({ height: '200px', width: '330px', theme: 'arctic' });
             $("input[type='button']").jqxButton({ theme: 'arctic' });
             BindRegion("trArea", -1);
             $('#trArea').on('select', function (event) {
                 var args = event.args;
                 var item = $('#trArea').jqxTree('getItem', args.element);
                 var dropDownContent = '<div style="position: relative; margin-left: 3px; margin-top: 2px;">' + item.label + '</div>';
                 $("#btnArea").jqxDropDownButton('setContent', dropDownContent);



             });
         });

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

         function Query() {

             var item = $('#trArea').jqxTree("getSelectedItem");
             if (item == null)
                 return;



             $.ajax(
                    {
                        type: "POST",
                        contentType: "application/json",
                        url: "WebMethods.aspx/getPipeLeakReportByAreaID",
                        data: "{'areaid':'" + item.value + "'}",
                        dataType: "json",
                        success: function (msg) {

                            var ret = $.parseJSON(msg.d);


                            var source =
                                {
                                    localdata: ret.data,
                                    datatype: "array",
                                    datafields:
                                    [

                                        { name: 'PipeID', type: 'string' },
                                        { name: 'PipeName', type: 'string' },
                                        { name: 'LeakDate', type: 'string' },
                                        { name: 'ProcessDesc', type: 'string' },
                                        { name: 'ProcessState', type: 'string' }


                                    ]
                                };
                            var dataAdapter = new $.jqx.dataAdapter(source);

                            $("#historicDataGrid").jqxGrid(
                                {
                                    width: 620,
                                    autoheight: true,
                                    source: dataAdapter,
                                    columnsresize: true,
                                    theme: 'arctic',
                                    showemptyrow: false,
                                    columns: [
                                      { text: 'id', datafield: 'PipeID', width: 120, hidden: true },
                                      { text: '管道名称', datafield: 'PipeName', width: 120 },
                                      { text: '泄露时间', datafield: 'LeakDate', width: 120 },
                                      { text: '处理描述', datafield: 'ProcessDesc', width: 180 },
                                      { text: '处理状态', datafield: 'ProcessState', width: 180 }


                                    ]
                                });

                            if (ret.data == null || ret.data.length == 0) {

                                alert("没有可以展示的数据！");
                            }


                            //                            $('#historicDataGrid').on('rowdoubleclick', function (event) {
                            //                                var args = event.args;

                            //                                var boundIndex = args.rowindex;

                            //                                var data = $('#historicDataGrid').jqxGrid('getrowdata', boundIndex);
                            //                                // alert(data.SensorAnalyzeID);
                            //                                $("#ChartWindow").jqxWindow("open");

                            //                                QueryResult(data.SensorAnalyzeID);
                            //                            });



                        }

                    });



         }

           </script>
</head>
<body>
    <div class="SearchOptionPanel" style="width:100%;height:50px;line-height:22px">
    <span style="font-size:12px;float:left">区域</span><div style="float:left" id="btnArea"><div id="trArea"></div></div>
   
     <input type="button" value="查询" onclick="Query()" />  <input type="button" value="打印" onclick="print()" />

    
  </div>
        <div id='DataTab'>
            <ul>
                <li style="margin-left: 30px;">查询列表</li>
                
           </ul>
           
            <div>
                <div id='historicDataGrid' style="width:100%; ">
               </div>
            </div>
          
      
           
        </div>   
        <form id="Form1" runat="server">
           <asp:HiddenField runat="server" ID="SensorIdValue" />
        </form>
       
</body>
</html>
