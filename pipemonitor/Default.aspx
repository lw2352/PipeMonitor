<%@ Page Language="C#" AutoEventWireup="true" Inherits="_Default" Codebehind="Default.aspx.cs" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html>
<head>

 <%--var skinSource = ["light", "dark", "arctic", "web", "bootstrap", "metro", skin, "office", "fresh","energyblue","darkblue","black","shinyblack"];--%>
	<meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
	<meta name="viewport" content="initial-scale=1.0, user-scalable=no" />
	<link rel="stylesheet" href="jqwidgets/jqwidgets/styles/jqx.base.css" type="text/css" />
	<link rel="stylesheet" href="jqwidgets/jqwidgets/styles/jqx.metrodark.css" type="text/css" />
	<link rel="stylesheet" href="jqwidgets/jqwidgets/styles/jqx.arctic.css" type="text/css" />
	<link rel="stylesheet" href="jqwidgets/jqwidgets/styles/jqx.metro.css" type="text/css" />
	<link rel="stylesheet" href="jqwidgets/jqwidgets/styles/jqx.shinyblack.css" type="text/css" />
	<link rel="stylesheet" href="jqwidgets/jqwidgets/styles/jqx.web.css" type="text/css" />
	<link rel="stylesheet" href="jqwidgets/jqwidgets/styles/jqx.bootstrap.css" type="text/css" />
	<link rel="stylesheet" href="jqwidgets/jqwidgets/styles/jqx.office.css" type="text/css" />
	<link rel="stylesheet" href="jqwidgets/jqwidgets/styles/jqx.darkblue.css" type="text/css" />
	<link rel="stylesheet" href="jqwidgets/jqwidgets/styles/jqx.fresh.css" type="text/css" />
	<link rel="stylesheet" href="jqwidgets/jqwidgets/styles/jqx.energyblue.css" type="text/css" />
	<link rel="stylesheet" href="jqwidgets/jqwidgets/styles/jqx.black.css" type="text/css" />
	<link rel="stylesheet" href="jqwidgets/jqwidgets/styles/jqx.mobile.css" type="text/css" />
	<link rel="stylesheet" href="jqwidgets/jqwidgets/styles/jqx.glacier.css" type="text/css" />
	<link rel="stylesheet" href="jqwidgets/jqwidgets/styles/jqx.android.css" type="text/css" />
	<link rel="stylesheet" href="jqwidgets/jqwidgets/styles/jqx.windowsphone.css" type="text/css" />
	<link rel="stylesheet" href="jqwidgets/jqwidgets/styles/jqx.ui-overcast.css" type="text/css" />
	<link rel="stylesheet" href="jqwidgets/jqwidgets/styles/jqx.ui-start.css" type="text/css" />
	<link rel="stylesheet" href="jqwidgets/jqwidgets/styles/jqx.ui-sunny.css" type="text/css" />
	<link rel="stylesheet" href="jqwidgets/jqwidgets/styles/jqx.ui-smoothness.css" type="text/css" />
	<link rel="stylesheet" href="jqwidgets/jqwidgets/styles/jqx.ui-le-frog.css" type="text/css" />
	<link rel="stylesheet" href="jqwidgets/jqwidgets/styles/jqx.ui-redmond.css" type="text/css" />
	<link rel="stylesheet" href="jqwidgets/jqwidgets/styles/jqx.blackberry.css" type="text/css" />
	<link rel="stylesheet" href="Styles/main.css" type="text/css" />
	<script type="text/javascript" src="jqwidgets/scripts/jquery-1.11.1.min.js"></script>
	<script type="text/javascript" src="jqwidgets/jqwidgets/jqxcore.js"></script>
	<script type="text/javascript" src="jqwidgets/jqwidgets/jqxmenu.js"></script>
	<script type="text/javascript" src="jqwidgets/jqwidgets/jqxbuttons.js"></script>
	<script type="text/javascript" src="jqwidgets/jqwidgets/jqxgauge.js"></script>
	<script type="text/javascript" src="jqwidgets/jqwidgets/jqxdraw.js"></script>
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
	<script type="text/javascript" src="jqwidgets/jqwidgets/jqxnavigationbar.js"></script>
	<script type="text/javascript" src="jqwidgets/jqwidgets/jqxsplitter.js"></script>
	<script type="text/javascript" src="jqwidgets/jqwidgets/jqxbuttons.js"></script>
	<script type="text/javascript" src="jqwidgets/jqwidgets/jqxscrollbar.js"></script>
	<script type="text/javascript" src="jqwidgets/jqwidgets/jqxpanel.js"></script>
	<script type="text/javascript" src="jqwidgets/jqwidgets/jqxtabs.js"></script>
	<script type="text/javascript" src="Scripts/InfoMaintain.js"></script>
	<style type="text/css">
	 html body{width:100%;margin:0px;height:100%}
	 div {font-family:"simsun",Georgia,Serif;}
	
	</style>
	<script type="text/javascript" src="http://api.map.baidu.com/api?v=2.0&ak=S2WhnhMHOfqn4iQF8CcD7Ajl"></script>
	<title>地图展示</title>
</head>
<body style="overflow:hidden">
	<div id="head" >
	   <div style="margin:0px;padding:0px;float:left;margin-left:25px;height:100%"><img style="margin-top:5px" src="Images/wp4.png" width=80 height=80 /></div><span style="font-family:微软雅黑,华文细黑;color:rgb(0,78,148);position:relative;font-size:28px;top:20px">供水管网漏水实时监测系统</span>
	<div style="height:100%;float:right;margin-right:20px">
	 <span style="color:rgb(0,78,148);float:left;margin-top:33px;font-size:14px">皮肤</span><div style="float:left;margin-top:30px;margin-left:10px" id="cmbSkinList"></div>
	
	</div>
	</div>
	  <%="<div id='hideValue' style='display:none'  data-skin='"+Request["skin"]+"'   data-defaultarea='" + Session["AreaID"].ToString() + "'></div>"%>
	<%--  <div style="height:80px;background-color:rgb(37,37,38)"></div>--%>
	  <div id="pageBody">
		  <div>
		  <div id="sideBar" style="display:none">
			 <div>
				<div style='margin-top: 2px;'>
				  
					<div style='margin-left: 4px; float: left;'>系统设置</div>
					   
				</div>
			</div>
			<div>
			  <div id="menuSysSetting">
				<ul>
				  
					<li id="miRegionConfig" onclick="ShowAreaConfigWindow()">区域设置</li>
					<li id="miMaterailList" onclick="ShowMaterialList()">管材维护</li>
					<li id="Li3" onclick="ReadGPSTime()" >读取当前GPS时间</li>
				     <li id="Li4" onclick="UploadData()" >上传数据</li>
                     <li id="Li5" onclick="SetTime()" >设定采样时刻</li>
				</ul>
			  </div>
			</div>
			<div>
				<div style='margin-top: 2px;'>
				  
					<div style='margin-left: 4px; float: left;'>报表管理</div>
					 
				</div>
			</div>
			<div>
			   <div id="menuReportManage">
				  <ul>
					<li id="miWaterbanlanceReport" onclick="LoadWaterbanlanceReport();">水平衡报告</li>
					<li id="miPipeHistoricExceptionReport"  onclick="LoadPipeHistoricReport();">管道历史异常信息报表</li>
					<li id="miPropertyReport" onclick="LoadPropertyReport();">固定资产报表</li>
				   
				  </ul>

				</div>
			</div>
			<div>
				<div style='margin-top: 2px;'>
				   
					<div style='margin-left: 4px; float: left;'>用户管理</div>
						
				</div>
			</div>
			<div>
			   <div id="menuUserManage">
				  <ul>
					<li id="miUserMaintain" onclick="ShowUserList()">用户维护</li>
					<li  id="miUserGroupMaintain" onclick="ShowGroupList()">用户组维护</li>
					<li id="miUpdatePwd" onclick="ShowChangePwdWindow()">修改密码</li>
					<li onclick="Exit();">退出</li>
				 
				  </ul>
				</div>
			</div>
		  </div>
		  <div  style="float:left;margin-top:4px" id="RegionButton1"> <div style="border: none;" id='RegionTree1'  > </div></div>
			  
				
						
		 </div>
		 <div class="rightPanel">


		  <div id='leftTabs' >
			<ul style="margin-left: 30px;" >
				<li>官网地图</li>
			</ul>
		   
			<div id="mapContainer" style="position:relative;top:0px;left:0px">
			 
					<div id="allmap"  onmousedown="return BeginDragPipe(event);" onmousemove="return DragingPipe(event);" onmouseup="return EndDragPipe(event);" style="width:1920px;height:1200px;overflow:hidden"></div>
				   <canvas id="OverlayCanvas" width="1920" height="1080"   style="pointer-events:none;position:absolute;left:0px;top:0px;"></canvas>
			 
			</div>
		</div>
	</div>


		  
		 </div>
	
	 <div id='ContextMenu' >
				<ul>
				  
					<li id="miAddRegion" onclick="NewRegion();">新增区域</li>
					<li id="miAddPipe"   onclick="NewEmptyPipe();">新增管线 </li>
					<li id="miNextPipe"  onclick="AddNextPipe();">下一个管线节点</li>
					<li id="miAddValve"  onclick="AddNewValve();">添加阀门</li>
					<li id="miAddFirePlug" onclick="AddNewFirePlug();">添加消防栓</li>
					<li id="miAddWell"   onclick="AddNewWell();">添加井</li>
					<li id="Li2"   onclick="AddNewMark();">添加标注</li>
				   
				</ul>
	 </div>
	 <div id="PipeEditContextMenu" >
			<ul>
				<li id="miEditPipeInfo" onclick="ShowPipeInfo();">编辑信息</li>
				<li id="miEditPipeLayout" onclick="BeginEditPipe();">编辑管线布局</li>
				<li id="miDeletePipe" onclick="DeletePipe();">删除管线</li>
				<li id="miAddWatermeter" onclick="AddWaterMeter();">添加水表</li>
			  
				
		   </ul>
	 </div>

	<div id="SensorContextMenu" >
			<ul>
				<li id="miAdjustPos" onclick="AdjustPosition();">微调经纬度</li>
				<li id="miAddSensor" onclick="AddSensor();">添加传感器</li>
				<li id="miEditSensorInfo" onclick="EditSensorInfo();">编辑传感器信息</li>
				<li id="miDeleteSensor" onclick="DeleteSensor();">删除传感器</li>
				<li id="miSensorHistoricReport" onclick="SensorHistoricQuery();">历史分析数据查询</li>
				<li id="Li1" onclick="SensorCurrentQuery();">当前分析数据查询</li>
			   
		   </ul>
	 </div>
		<div id="WatermeterContextMenu" >
			<ul>
			  
				<li id="miEditWaterInfo" onclick="EditWatermeterInfo();">编辑水表信息</li>
				<li id="miDeleteWatermeter" onclick="DeleteWatermeter();">删除水表</li>
				<li id="miWaterHisroricData" onclick="LoadWatermeterDataPage();">水表历史数据</li>
				<li id="miWaterReading" onclick="ShowWaterReading();">当前读数</li>
			   
		   </ul>
	 </div>

			 <div id="DeviceContextMenu" >
			<ul>
			  
				<li id="miEditDeviceInfo" onclick="EditDeviceInfo();">编辑信息</li>
				<li id="miDeleteDevice" onclick="DeleteDevice();">删除</li>
			  
			   
		   </ul>
	 </div>
	 <div id="PipeWindow" style="width:700px;display:none">
	  <div >新增管线</div>
	   <div>
		 <table style="width:100%">
		   <tr ><th style="width:16%">管线名称*</th><td style="width:16%"><input id="txtPipeName" type="text"/></td><th style="width:16%">管线编号*</th><td style="width:16%"><input id="txtPipeNo" type="text"/></td><th style="width:16%">管理员*</th><td style="width:16%"><div id="cmbAdmins"></div></td>  </tr>
		   <tr ><th style="width:16%">管径</th><td style="width:16%"><div id="txtPipeWidth" ></div></td><th style="width:16%">所属区域*</th><td style="width:16%"><div id="btnPipeArea"><div id="trPipeArea"></div></div></td><th style="width:16%">管材*</th><td style="width:16%"><div id="cmbMaterial"></div></td>  </tr>
		   <tr ><th style="width:16%">埋深</th><td style="width:16%"><div id="txtPipeDepth" ></div></td><th style="width:16%">管长</th><td style="width:16%"><div id="txtPipeLength" ></div></td><th style="width:16%">备注</th><td style="width:16%"><input id="txtPipeRemark" type="text"/></td>  </tr>

		   <tr ><td colspan="6" style="text-align:center"><input onclick="SavePipeInfo();" type="button" style="width:120px" value="保存" /></td></tr>
			  
		 </table>
	   
	   
	   </div>
	  
	 </div>


	 <div id="MaterialWindow" style="width:700px;display:none">
	  <div >管材列表</div>
	   <div>
		  <div id="toolbar"><input type="button" value="新增" id='btnAddMaterial' onclick="ShowAddMaterialWindow()"/><input type="button" value="修改" id='btnEditMaterial' onclick="ShowEditMaterialWindow();" /><input type="button" value="删除" id='btnDelMaterial' onclick="DeleteMaterial();"/></div>
		  <div id="materialGrid" style="margin-top:15px">
		 
		 
		  </div>
	 
	   </div>
	 
	 </div>

	 <div id="UserWindow" style="width:700px;display:none">
	  <div >用户列表</div>
	   <div>
		  <div id="userToolbar"><input type="button" value="新增" id='btnAddUser' onclick="ShowAddUserWindow()"/><input type="button" value="修改" id='btnEditUser' onclick="ShowEditUserWindow();" /><input type="button" value="删除" id='btnDelUser' onclick="DeleteUser();"/></div>
		  <div id="userGrid" style="margin-top:15px">
		 
		 
		  </div>
	 
	   </div>
	 
	 </div>

	 <div id="AreaWindow" style="width:700px;display:none">
		<div >区域设置</div>
		<div>
		 <div ><input type="button" value="修改" id='btnEditArea' onclick="ShowEditAreaWindow()"/><input type="button" value="删除" id='btnDelArea' onclick="DeleteArea();" /></div>
		
		   <div id="trAreaSet">
		 
		   </div>
	 
		 </div>
	 
	 </div>

	 <div id="EditAreaWindow" style="width:360px;display:none">
		<div >编辑区域</div>
		<div>
			<div style="margin-left:80px;margin-top:10px"><span style="width:70px;display:inline-block;">区域名称</span><span><input type="text" id="txtEditAreaName"/></span></div>
		  <div style="margin-left:80px;margin-top:10px"><span style="width:70px;display:inline-block;">经纬度</span><span><input type="text" id="txtEditAreaPosition"/></span></div>
		  <div style="margin-top:10px"><input type="button" value="保存" style="width:120px;margin-left:130px" onclick="SaveEditArea()" /></div>
	 
		 </div>
	 
	 </div>



	 <div id="AddMaterialWindow"  style="width:700px;display:none">
	  <div >新增管材</div>
	   <div>
		  
		  <div style="margin-left:80px;margin-top:10px"><span>管材名称</span><span><input type="text" id="txtMaterialName2"/></span></div>
		  <div style="margin-left:80px;margin-top:10px"><span>管材参数</span><span><input type="text" id="txtMaterialParameter"/></span></div>

		  <div style="margin-top:10px"><input type="button" value="添加" style="width:120px;margin-left:130px" onclick="AddMaterial()" /></div>
	   </div>
	 
	 </div>

	  <div id="AddUserWindow"  style="width:700px;display:none">
	  <div >新增用户</div>
	   <div>
		  
	   
	  
		  <table style="width:100%">
		   <tr ><th style="width:16%">用户名</th><td style="width:16%"><input id="txtAddUserUserName" type="text"/></td><th style="width:16%">真实姓名</th><td style="width:16%"><input id="txtAddUserRealName" type="text"/></td>  </tr>
		   <tr ><th style="width:16%">性别</th><td style="width:16%"><span>男</span><input type="radio" checked='checked' name="a" id="rbMan"/><span>女</span><input name="a" value="女" type="radio" id="rbWoman"/></td><th style="width:16%">所属区域*</th><td style="width:16%"><div id="btnUserArea1"><div id="trUserArea1"></div></div></td> </tr>
		   <tr ><th style="width:16%">用户组</th><td style="width:16%"><div id="cmbUserGroup" ></div></td><th style="width:16%">手机</th><td style="width:16%"><input type="text" id="txtAddUserPhoneNumber"/></td> </tr>

		   <tr ><td colspan="6" style="text-align:center"><input onclick="AddUser();" type="button" style="width:120px" value="确定" /></td></tr>
			  
		 </table>
	  
	  
	  
	   </div>
	 
	 </div>


	 <div id="GroupWindow" style="display:none">
	   <div>用户组</div>
	   <div>
			<div id='GroupMenu' >
				  <ul>
					<li><a onclick="ShowAddGroupWindow()" >新增</a></li>
					<li><a  onclick="SavePermission()">保存</a></li>
				  </ul>
			</div>
			<div>
			  <div id="GroupContainer" style="float:left;width:200px;height:500px;margin-top:20px">
			 
			 
			 
			 
			  </div>
				<div style="float:left;width:500px;height:500px;margin-top:20px">
				  <div  class="GroupButtonList" style="float:left;width:120px;height:400px">
					<div id='cbAddWatermeter' class="permissionbutton"  style='margin-left: 10px; float: left;'>新增水表</div>
					 
					<div id='cbUpdateWatermeter'  class="permissionbutton" style='margin-left: 10px; float: left;'>修改水表</div>
			  
					<div id='cbDeleteWatermeter' class="permissionbutton" style='margin-left: 10px; float: left;'>删除水表</div>
			   
					<div id='cbAddSensor' class="permissionbutton" style='margin-left: 10px; float: left;'>新增传感器</div>
				
					<div id='cbUpdateSensor' class="permissionbutton" style='margin-left: 10px; float: left;'>修改传感器</div>
			   
					<div id='cbDeleteSensor'  class="permissionbutton" style='margin-left: 10px; float: left;'>删除传感器</div>
			 

					<div id='cbAddPipe'  class="permissionbutton" style='margin-left: 10px; float: left;'>新增管线</div>
			  
					<div id='cbAdjustPipePos'  class="permissionbutton"  style='margin-left: 10px; float: left;'>微调经纬度</div>
			 
					<div id='cbUpdatePipeInfo'  class="permissionbutton" style='margin-left: 10px; float: left;'>修改管线信息</div>
			  
				 
				 
				 
				 </div>
			 
				  <div class="GroupButtonList"    style="float:left;width:120px;height:400px">
					<div id='cbUpdatePipeLayout'  class="permissionbutton" style='margin-left: 10px; float: left;'>编辑管线布局</div>
					 
					<div id='cbDeletePipe'  class="permissionbutton" style='margin-left: 10px; float: left;'>删除管线</div>
			  
					<div id='cbAddMaterial'   class="permissionbutton"  style='margin-left: 10px; float: left;'>新增管材</div>
			   
					<div id='cbUpdateMaterial'  class="permissionbutton"  style='margin-left: 10px; float: left;'>修改管材</div>
				
					<div id='cbDeleteMaterial'   class="permissionbutton"  style='margin-left: 10px; float: left;'>删除管材</div>
			   
					<div id='cbAddArea'   class="permissionbutton" style='margin-left: 10px; float: left;'>新增区域</div>
			 

					<div id='cbUpdateArea'   class="permissionbutton"  style='margin-left: 10px; float: left;'>修改区域</div>
			  
					<div id='cbDeleteArea'  class="permissionbutton"  style='margin-left: 10px; float: left;'>删除区域 </div>
			 
				
				 
				 
				 
				 </div>
				   <div class="GroupButtonList" style="float:left;width:120px;height:400px">
				  
			  
					<div id='cbWaterbalanceQuery'  class="permissionbutton" style='margin-left: 10px; float: left;'>水平衡报表查询</div>

					<div id='cbWaterHistoricDataReport'  class="permissionbutton" style='margin-left: 10px; float: left;'>水表历史数据查询</div>
			   
					<div id='cbSensorQuery'  class="permissionbutton" style='margin-left: 10px; float: left;'>传感器数据分析查询</div>
				
					<div id='cbHistoricQuery'  class="permissionbutton" style='margin-left: 10px; float: left;'>传感器历史数据分析查询</div>
			   
				  
				 
				 </div>
			 
			 
			   </div>
			
			
			</div>
	 
	   </div>
	 
	 
	 </div>

	   <div id="EditMaterialWindow"  style="width:700px;display:none">
	  <div >修改管材</div>
	   <div>
		  
		  <div style="margin-left:80px;margin-top:10px"><span>管材名称</span><span><input type="text" id="txtMaterialName3"/></span></div>
		  <div style="margin-left:80px;margin-top:10px"><span>管材参数</span><span><input type="text" id="txtMaterialParameter3"/></span></div>

		  <div style="margin-top:10px"><input type="button" value="修改" style="width:120px;margin-left:130px" onclick="EditMaterial()" /></div>
	   </div>
	 
	 </div>

	  <div id="SensorWindow" style="width:700px;display:none">
	  <div >传感器信息</div>
	   <div>
		 <table style="width:100%">
		   <tr ><th style="width:16%">传感器名称*</th><td style="width:16%"><input id="txtSensorName" type="text"/></td><th style="width:16%">传感器编号*</th><td style="width:16%"><input id="txtSensorNo" type="text"/></td><th style="width:16%">管理员*</th><td style="width:16%"><div id="cmbSensorAdmin"></div></td>  </tr>
		   <tr ><th style="width:16%">传感器类型</th><td style="width:16%"><input id="txtSensorType" type="text"/></td><th style="width:16%">所属区域*</th><td style="width:16%"><div id="btnSensorArea"><div id="trSensorArea"></div></div></td><th style="width:16%">备注</th><td style="width:16%"><input id="txtSensorRemark" type="text" /></td>  </tr>
		  

		   <tr ><td colspan="6" style="text-align:center"><input onclick="SaveSensorInfo();" type="button" style="width:120px" value="保存" /></td></tr>
			  
		 </table>
	   
	   
	   </div>
	  
	 </div>

	  <div id="DeviceWindow" style="width:700px;display:none">
	  <div >设备信息</div>
	   <div>
		 <table style="width:100%">
		   <tr ><th style="width:25%">名称</th><td style="width:25%"><input id="txtDeviceName" type="text"/></td><th style="width:25%">所属区域*</th><td style="width:25%"><div id="btnDeviceArea"><div id="trDeviceArea"></div></div></td></tr>
		   <tr ><th style="width:25%">备注</th><td style="width:50%"><input id="txtDeviceRemark" type="text" /></td>  </tr>
		  

		   <tr ><td colspan="6" style="text-align:center"><input onclick="SaveDeviceInfo();" type="button" style="width:120px" value="保存" /></td></tr>
			  
		 </table>
	   
	   
	   </div>
	  
	 </div>



  
	 <div id="WatermeterWindow" style="width:700px;display:none">
	  <div >水表信息</div>
	   <div>
		 <table style="width:100%">
		   <tr ><th style="width:16%">水表名称*</th><td style="width:16%"><input id="txtWatermeterName" type="text"/></td><th style="width:16%">水表编号*</th><td style="width:16%"><input id="txtWatermeterNo" type="text"/></td><th style="width:16%">管理员*</th><td style="width:16%"><div id="cmbWatermeterAdmin"></div></td>  </tr>
		   <tr ><th style="width:16%">水表类型</th><td style="width:16%"><input id="txtWatermeterType" type="text"/></td><th style="width:16%">所属区域*</th><td style="width:16%"><div id="btnWatermeterArea"><div id="trWatermeterArea"></div></div></td><th style="width:16%">备注</th><td style="width:16%"><input id="txtWatermeterRemark" type="text" /></td>  </tr>
			<tr ><th style="width:16%">上级水表</th><td style="width:16%"><div id="cmbPreWater"></div></td><th style="width:16%">水表级数</th><td style="width:16%"><div id="cmbWaterLevel"></div></td></tr>
		  

		   <tr ><td colspan="6" style="text-align:center"><input onclick="SaveWatermeterInfo();" type="button" style="width:120px" value="保存" /></td></tr>
			  
		 </table>
	   
	   
	   </div>
	  
	 </div>

	   <div id="AdjustPosWindow" style="width:700px;display:none">
	
		 <div >微调经纬度</div>
		<div style="text-align:center">
		   <div> <span>经纬度</span><input id="txtNewPos" type="text" /></div>

		   <div> <input type="button" style="margin-top:10px;width:120px" value="保存" onclick="SavePosition()" /></div>
		
		</div>
	  
	 </div>


	 <div id="AddGroupWindow">
	 
		<div >新增用户组</div>
		<div style="text-align:center">
		   <div> <span>用户组名</span><input id="txtGroupName" type="text" /></div>

		   <div> <input type="button" style="margin-top:10px;width:120px" value="保存" onclick="AddGroup()" /></div>
		
		</div>
	 
	 
	 </div>


	   <div id="ChangePwdWindow">
	 
		<div >修改密码</div>
		<div style="text-align:center">
		   <div style="height:28px"> <span style="display:inline-block;width:70px;height:28px">原密码</span><span></span><input id="txtOldPwd" type="password" /></span></div>
			 <div style="height:28px"> <span style="display:inline-block;width:70px;height:28px">新密码</span><span><input id="txtNewPwd" type="password" /></span></div>
			 <div style="height:28px"> <span style="display:inline-block;width:70px;height:28px">确认密码</span><span><input id="txtNewPwd2" type="password" /></span></div>

		   <div> <input type="button" style="margin-top:10px;width:120px" value="保存" onclick="ChangePwd()" /></div>
		
		</div>
	 
	 
	 </div>
	 <div id="GaugeWindow">
	 <div>水表读数</div>
	 <div>
	   
		<div id="gauge" ></div>
		<div><a id="waterLink" href="#">水表历史记录</a></div>
	 
	 
	 </div>
	 
	 </div>

	 <div id="RegionWindow" style="display:none">
	  <div >新建区域</div>
			   
		<div style="text-align:center">   
		<table>  
		 <tr> <th>上级区域</th><td><div id="btnSelectRegion">

			  <div style="border: none;" id='RegionTreeList'>
			  
			  </div>
		 
		 </div></td></tr>
		 
		 <tr><th>区域名</th><td><input id="txtRegionName" type="text" /></td></tr>
		 <tr><th>经纬度</th><td><input id="txtRegionPos" type="text" /></td></tr>
		 </table> 
		 <input onclick="SaveRegion();" type="button"  id="btnSaveRegion" value="保存" style="margin-top:6px"/>
	   </div>  
	 </div>


	 <div id="TooltipWindow" style="font-size:12px;font-family: Verdana,Arial,sans-serif;position:absolute;color:red;border:1px solid  #a4bed4;background-color:white;display:none;width:200px;height:110px">
		 <div style="border-bottom:1px solid  #a4bed4;width:200px;background-color:rgb(224,233,245);height:20px">异常信息</div>
		 <div style="background-color:white">
	  
			<div style="width:190px;height:24px"><span>处理结果：</span><span id="lbProcessResult"></span></div>
			<div style="width:190px;height:24px"><span>处理描述：</span><span id="lbProcessDesc"></span></div>
		 </div>
	 </div>

</body>
</html>
<script type="text/javascript">

	var map = null;
	var contextMenu = null;
	var CurrentPos = "";
	var CurrentPipePoints;
	var CurrentPolyline;
	var PolylineContextMenu;
	var pipeContextMenu = null;
	var sensorContextMenu = null;
	var watermeterContextMenu = null;
	var deviceContextMenu = null;
	var CurrentPipeHeaderMark = null;
	var CurrentAreas = null;
	var Pipes;
	var Sensors;
	var Devices;
	var SensorImage;
	var WatermeterImage;
	var ValveImage;
	var FirePlugImage;
	var WellImage;
	var skin = "android";

	function Pipe(startPos, endPos, id) {

		this.startPos = startPos;
		this.endPos = endPos;
		this.id = id;
	}


	function Device(Location, DeviceType, id,Name) {

		this.Location = Location;
		this.DeviceType = DeviceType;
		this.id = id;
		this.Name = Name;
	}

	function Sensor(PipeID, isSecondHead, id,Name) {

		this.pipeID = PipeID;
		this.secondHead = isSecondHead;
		this.id = id;
		this.Name = Name;
	}


	function Watermeter(PipeID, Position, id,Name) {
		this.pipeid = PipeID;
		this.position = Position;
		this.id = id;
		this.Name = Name;
	}

	function InitMap() {

		map = new BMap.Map("allmap", { enableMapClick: false, minZoom: 16, maxZoom: 19 });
		map.centerAndZoom(new BMap.Point(114.360122, 30.521608), 19);
		map.addControl(new BMap.MapTypeControl());
		map.setCurrentCity("武汉");
		map.enableScrollWheelZoom(true);
	}

	function NewRegion() {
		$('#RegionWindow').jqxWindow('open');
		$("#txtRegionPos").jqxInput('val', CurrentPos);
	}

	function ReadGPSTime() {
	    $.ajax({
	        type: "POST",
	        contentType: "application/json",
	        url: "Net_DB.aspx/ReadGPSTime",
	        data: "",
	        dataType: "json",
	        success: function (msg) {

	            var ret = $.parseJSON(msg.d);
	            if (ret.msg == "ok") {

	                alert("上传命令发送成功!");
	            }
	            else { alert("失败")}
	        }
	    });
	}

	function SetTime() {
	    $.ajax({
	        type: "POST",
	        contentType: "application/json",
	        url: "Net_DB.aspx/SetTime",
	        data: "",
	        dataType: "json",
	        success: function (msg) {

	            var ret = $.parseJSON(msg.d);
	            if (ret.msg == "ok") {

	                alert("上传命令发送成功!");
	            }
	            else { alert("失败") }
	        }
	    });
	}

	function UploadData() {
	    $.ajax({
	        type: "POST",
	        contentType: "application/json",
	        url: "Net_DB.aspx/UploadData",
	        data: "",
	        dataType: "json",
	        success: function (msg) {

	            var ret = $.parseJSON(msg.d);
	            if (ret.msg == "ok") {

	                alert("上传命令发送成功!");
	            }
	            else { alert("失败") }
	        }
	    });
	}


	function RefreshUserList() {

		$.ajax(
					{
						type: "POST",
						contentType: "application/json",
						url: "WebMethods.aspx/getAllUser",
						data: "",
						dataType: "json",
						success: function (msg) {

							var ret = $.parseJSON(msg.d);
							var source =
								{
									localdata: ret.data,
									datatype: "array",
									datafields:
									[
										{ name: 'UserName', type: 'string' },
										{ name: 'RealName', type: 'string' },
										{ name: 'UserID', type: 'string' },
										{ name: 'GroupName', type: 'string' },
										{ name: 'Gender', type: 'string' },
										{ name: 'AreaName', type: 'string' },
										{ name: 'PhoneNumber', type: 'string' },
										{ name: 'GroupID', type: 'string' },
										{ name: 'AreaID', type: 'string' }
									]
								};
							var dataAdapter = new $.jqx.dataAdapter(source);

							$("#userGrid").jqxGrid(
								{
									width: 790,
									autoheight: true,
									source: dataAdapter,
									columnsresize: true,
									theme: skin,
									showemptyrow: false,
									columns: [
									  { text: '用户名', datafield: 'UserName', width: 120 },
									  { text: '真实姓名', datafield: 'RealName', width: 120 },
									  { text: 'UserID', datafield: 'UserID', width: 120, hidden: true },
									  { text: '用户组', datafield: 'GroupName', width: 120, hidden: false },
									  { text: '性别', datafield: 'Gender', width: 120, hidden: false },
									  { text: '区域', datafield: 'AreaName', width: 120, hidden: false },
									  { text: '手机号码', datafield: 'PhoneNumber', width: 120, hidden: false },
									  { text: 'GroupID', datafield: 'GroupID', width: 120, hidden: true },
									  { text: 'AreaID', datafield: 'AreaID', width: 120, hidden: true }

									]
								});
							$('#userGrid').on('rowdoubleclick', function (event) {

								ShowEditUserWindow();

							});

						}
					});


	}

	function RefreshMaterialList() {

		$.ajax(
					{
						type: "POST",
						contentType: "application/json",
						url: "WebMethods.aspx/getAllPipeMaterial",
						data: "",
						dataType: "json",
						success: function (msg) {

							var ret = $.parseJSON(msg.d);



							var source =
								{
									localdata: ret.data,
									datatype: "array",
									datafields:
									[
										{ name: 'PipeMaterialName', type: 'string' },
										{ name: 'PipeMaterialParameter', type: 'string' },

										  { name: 'PipeMaterialID', type: 'string' }


									]
								};
							var dataAdapter = new $.jqx.dataAdapter(source);

							$("#materialGrid").jqxGrid(
								{
									width: 590,
									autoheight: true,
									source: dataAdapter,
									columnsresize: true,
									theme: skin,
									showemptyrow: false,
									columns: [
									  { text: '参数', datafield: 'PipeMaterialParameter', width: 120 },
									  { text: '名称', datafield: 'PipeMaterialName', width: 120 },
									 { text: 'ID', datafield: 'PipeMaterialID', width: 120, hidden: true }

									]
								});

							$('#materialGrid').on('rowdoubleclick', function (event) {
								ShowEditMaterialWindow();
							});
						}
					});

	}
	function ShowAreaConfigWindow() {

		$("#AreaWindow").jqxWindow("open");
		BindRegion("trAreaSet");
	}

	function ShowMaterialList() {
		$("#MaterialWindow").jqxWindow("open");

		RefreshMaterialList();

	}

	function SetPermissionChecked(permission) {
		$(".permissionbutton").each(function () {

			if (permission.indexOf($(this).text()) >= 0) {
				$(this).jqxCheckBox("check");
			}
			else {
				$(this).jqxCheckBox("uncheck");
			}

		});


	}

	function RefreshPermission() {


		var item = $('#GroupContainer').jqxListBox("getSelectedItem");
		if (item == null)
			return;

		$.ajax(
			{
				type: "POST",
				contentType: "application/json",
				url: "WebMethods.aspx/getGroupPermissionByGroupID",
				data: "{'groupid':'" + item.value + "'}",
				dataType: "json",
				success: function (msg) {

					var ret = $.parseJSON(msg.d);
					if (ret.msg == "ok") {

						SetPermissionChecked(ret.data[0].SysPermission);
					}

				}
			}

				  );


	}

	function RefreshGroupList() {
		$.ajax(
			{
				type: "POST",
				contentType: "application/json",
				url: "WebMethods.aspx/GetAllGroups",
				data: "{}",
				dataType: "json",
				success: function (msg) {

					var ret = $.parseJSON(msg.d);
					if (ret.msg == "ok") {

						$('#GroupContainer').jqxListBox({ selectedIndex: -1, source: ret.data, displayMember: "GroupName", valueMember: "GroupID", theme: skin, itemHeight: 70, height: '500', width: '200',
							renderer: function (index, label, value) {
								var datarecord = ret.data[index];
								var imgurl = 'Images/usergroup.png';
								var img = '<img height="50" width="40" src="' + imgurl + '"/>';
								var table = '<table style="min-width: 130px;"><tr><td style="width: 40px;" rowspan="2">' + img + '</td><td>' + datarecord.GroupName + '</td></tr></table>';
								return table;
							}
						});

						$('#GroupContainer').on('select', function (event) {
							var args = event.args;
							if (args) {
								RefreshPermission();
							}
						});




					}
				}
			});


	}


	function ShowAddGroupWindow() {

		$("#AddGroupWindow").jqxWindow("open");

	}


	function AddGroup() {

		var str = $("#txtGroupName").val().trim();

		if (str == "") {
			alert("请输入用户组名！");
			return;
		}
		if (str == "Administrator") {
			alert("该名称已被占用！");
			return;
		}
		$.ajax(
			{
				type: "POST",
				contentType: "application/json",
				url: "WebMethods.aspx/addGroup",
				data: "{'groupname':'" + str + "','syspermission':''}",
				dataType: "json",
				success: function (msg) {

					var ret = $.parseJSON(msg.d);
					if (ret.msg == "ok") {


						RefreshGroupList();
						$("#AddGroupWindow").jqxWindow("close");
					}
				}
			});
	}

	function SavePermission() {

		var item = $('#GroupContainer').jqxListBox("getSelectedItem");
		if (item == null)
			return;

		var str = "";
		$(".permissionbutton").each(function () {


			if ($(this).jqxCheckBox("checked")) {
				if (str != "") {
					str += "|";
				}
				str += $(this).text();
			}
		});

		$.ajax(
			{
				type: "POST",
				contentType: "application/json",
				url: "WebMethods.aspx/updateGroupByGroupID",
				data: "{'groupid':'" + item.value + "','syspermission':'" + str + "'}",
				dataType: "json",
				success: function (msg) {

					var ret = $.parseJSON(msg.d);
					if (ret.msg == "ok") {

						alert("保存成功");

					}
				}
			});

	}

	function ShowChangePwdWindow() {

		$("#ChangePwdWindow").jqxWindow("open");
	}

	function ChangePwd() {

		var newpwd1 = $("#txtNewPwd").val();
		var newpwd2 = $("#txtNewPwd2").val();
		var oldpwd = $("#txtOldPwd").val();
		if (newpwd1.length < 6) {
			alert("新密码长度不能少于6位!");
			return;
		}
		if (oldpwd.length < 6) {

			alert("密码长度不能少于6位!");
			return;
		}

		if (newpwd1 != newpwd2) {

			alert("两次输入的新密码不一致!");
			return;
		}


		$.ajax(
			{
				type: "POST",
				contentType: "application/json",
				url: "WebMethods.aspx/modifyUserPassword",
				data: "{'oldpassword':'" + oldpwd + "','newpassword':'" + newpwd1 + "'}",
				dataType: "json",
				success: function (msg) {

					var ret = $.parseJSON(msg.d);
					if (ret.msg == "ok") {

						$("#ChangePwdWindow").jqxWindow("close");

					}
				}
			});
	}


	function ShowGroupList() {

		$("#GroupWindow").jqxWindow("open");

		$(".GroupButtonList >div").jqxCheckBox({ width: 120, height: 25, theme: skin });
		RefreshGroupList();
	}

	function ShowUserList() {

		$("#UserWindow").jqxWindow("open");
		RefreshUserList();
	}

	function ShowAddMaterialWindow() {

		$("#AddMaterialWindow").jqxWindow("open");

	}


	function DeleteArea() {
		var item = $("#trAreaSet").jqxTree("getSelectedItem");

		if (item == null) {
			alert("请选择您要删除的区域!");
			return;
		}
		if (!confirm("您确定要删除这个区域吗？此项操作将导致该区域所属的设备也将被删除，您要继续吗？")) {
			return;
		}
		$.ajax(
			{
				type: "POST",
				contentType: "application/json",
				url: "WebMethods.aspx/deleteAreaByAreaID",
				data: "{'areaid':'" + item.value + "'}",
				dataType: "json",
				success: function (msg) {

					var ret = $.parseJSON(msg.d);
					if (ret.msg == "ok") {

						BindRegion("trAreaSet");
					}
				}
			});
	}

	function SaveEditArea() {

		var id = $("#EditAreaWindow").attr("data-id");
		var areaName = $("#txtEditAreaName").val();
		var pos = $("#txtEditAreaPosition").val();
		 $.ajax(
			{
				type: "POST",
				contentType: "application/json",
				url: "WebMethods.aspx/updateAreaByAreaID",
				data: "{'areaid':'" + id + "','areaname':'" + areaName + "','location':'" + pos + "'}",
				dataType: "json",
				success: function (msg) {

					var ret = $.parseJSON(msg.d);
					if (ret.msg == "ok") {

						BindRegion("trAreaSet");
					}
				}
			});

	}

	function Exit() {

		window.location.href = "signin.aspx";
	}


	function ShowEditAreaWindow() {

		var item = $("#trAreaSet").jqxTree("getSelectedItem");

		if (item == null) {
			alert("请选择您要更改的区域!");
			return;
		}

		$("#EditAreaWindow").jqxWindow("open");

		$("#EditAreaWindow").attr("data-id", item.value);

		RefreshAreaWindow(item.value);


	}



	function RefreshAreaWindow(areaid) {
		$.ajax(
			{
				type: "POST",
				contentType: "application/json",
				url: "WebMethods.aspx/getAreaByAreaID",
				data: "{'areaid':'" + areaid + "'}",
				dataType: "json",
				success: function (msg) {

					var ret = $.parseJSON(msg.d);
					if (ret.msg == "ok") {

						$("#txtEditAreaName").val(ret.data[0].AreaName);

						$("#txtEditAreaPosition").val(ret.data[0].Location);


					}
				}
			});
	}

	function ShowAddUserWindow() {

		$("#AddUserWindow").attr("data-type", "adduser");
		$("#AddUserWindow").jqxWindow("open");
		$("#AddUserWindow").jqxWindow({ title: '新增用户' });
		BindUserGroup();
	}

	function DeleteUser() {

		var rowindex = $('#userGrid').jqxGrid('getselectedrowindex');
		if (rowindex == -1) {
			alert("请选择您要删除的用户！");
			return;
		}
		if (!confirm("您确定要删除该用户吗？")) {
			return;
		}
		var userid = $('#userGrid').jqxGrid('getcellvalue', rowindex, "UserID");

		$.ajax(
			{
				type: "POST",
				contentType: "application/json",
				url: "WebMethods.aspx/DeleteUser",
				data: "{'UserID':'" + userid + "'}",
				dataType: "json",
				success: function (msg) {

					var ret = $.parseJSON(msg.d);
					if (ret.msg == "ok") {


						RefreshUserList();
					}
				}
			});

	}

	function ShowEditUserWindow() {

		var rowindex = $('#userGrid').jqxGrid('getselectedrowindex');
		if (rowindex == -1) {
			alert("请选择您要修改的用户！");
			return;
		}
		$("#AddUserWindow").jqxWindow({ title: '更新用户' });
		var username = $('#userGrid').jqxGrid('getcellvalue', rowindex, "UserName");

		var realname = $('#userGrid').jqxGrid('getcellvalue', rowindex, "RealName");
		var phone = $('#userGrid').jqxGrid('getcellvalue', rowindex, "PhoneNumber");
		var gender = $('#userGrid').jqxGrid('getcellvalue', rowindex, "Gender");
		var areaid = $('#userGrid').jqxGrid('getcellvalue', rowindex, "AreaID");
		var groupid = $('#userGrid').jqxGrid('getcellvalue', rowindex, "GroupID");
		var userid = $('#userGrid').jqxGrid('getcellvalue', rowindex, "UserID");
		$("#AddUserWindow").attr("data-id", userid);
		$("#AddUserWindow").attr("data-type", "edituser");
		$("#AddUserWindow").jqxWindow("open");
		BindUserGroup(groupid);
		SelectArea("trUserArea1", areaid);
		$("#txtAddUserUserName").val(username);
		$("#txtAddUserUserName").attr("readonly", "readonly");
		$("#txtAddUserPhoneNumber").val(phone);
		$("#txtAddUserRealName").val(realname);
		if (gender == "男") {
			$("#rbMan").click();
		}
		else {
			$("#rbWoman").click();
		}
	}



	function AddUser() {

		var username = $("#txtAddUserUserName").val();
		var realName = $("#txtAddUserRealName").val();
		var areaitem = $("#trUserArea1").jqxTree("getSelectedItem");

		if (areaitem == null) {
			alert("请选择用户所属区域！");
			return;
		}

		var phone = $("#txtAddUserPhoneNumber").val();
		var groupItem = $("#cmbUserGroup").jqxComboBox("getSelectedItem");
		if (groupItem == null) {
			alert("请选择用户所在的组！");
			return;
		}
		var userid = $("#AddUserWindow").attr("data-id");
		var sex = "男";

		if (!$("#cbMan").is(":checked")) {
			sex = "女";
		}
		var optype = $("#AddUserWindow").attr("data-type");
		if (optype == "adduser") {
			$.ajax(
			{
				type: "POST",
				contentType: "application/json",
				url: "WebMethods.aspx/AddUser",
				data: "{'UserName':'" + username + "','RealName':'" + realName + "','GroupID':'" + groupItem.value + "','AreaID':'" + areaitem.value + "','PhoneNum':'" + phone + "','Sex':'" + sex + "'}",
				dataType: "json",
				success: function (msg) {

					var ret = $.parseJSON(msg.d);
					if (ret.msg == "ok") {

						$("#AddUserWindow").jqxWindow("close");
						RefreshUserList();
					}
				}
			});

		}
		else {

			$.ajax(
			{
				type: "POST",
				contentType: "application/json",
				url: "WebMethods.aspx/UpdateUser",
				data: "{'RealName':'" + realName + "','GroupID':'" + groupItem.value + "','AreaID':'" + areaitem.value + "','PhoneNum':'" + phone + "','Sex':'" + sex + "','UserID':'" + userid + "'}",
				dataType: "json",
				success: function (msg) {

					var ret = $.parseJSON(msg.d);
					if (ret.msg == "ok") {

						$("#AddUserWindow").jqxWindow("close");
						RefreshUserList();
					}
				}
			});

		}
	}

	function ShowEditMaterialWindow() {

		$("#EditMaterialWindow").jqxWindow("open");

		var rowindex = $('#materialGrid').jqxGrid('getselectedrowindex');
		if (rowindex == -1) {
			alert("请选择您要修改的管材！");
			return;
		}
		var materialName = $('#materialGrid').jqxGrid('getcellvalue', rowindex, "PipeMaterialName");

		var materialParameter = $('#materialGrid').jqxGrid('getcellvalue', rowindex, "PipeMaterialParameter");

		var id = $('#materialGrid').jqxGrid('getcellvalue', rowindex, "PipeMaterialID");

		$("#EditMaterialWindow").attr("data-id", id);

		$("#txtMaterialName3").val(materialName);
		$("#txtMaterialParameter3").val(materialParameter);
	}

	function DeleteMaterial() {
		var rowindex = $('#materialGrid').jqxGrid('getselectedrowindex');
		if (rowindex == -1) {
			alert("请选择您要删除的管材！");
			return;
		}

		if (!confirm("您确定要删除该管材吗？")) {
			return;
		}
		var id = $('#materialGrid').jqxGrid('getcellvalue', rowindex, "PipeMaterialID");
		$.ajax(
			{
				type: "POST",
				contentType: "application/json",
				url: "WebMethods.aspx/deletePipeMaterialByPipeMaterialID",
				data: "{'pipematerialid':'" + id + "'}",
				dataType: "json",
				showemptyrow: false,
				success: function (msg) {

					var ret = $.parseJSON(msg.d);
					if (ret.msg == "ok") {


						RefreshMaterialList();
					}
				}
			});
	}


	function EditMaterial() {
		var materialName = $("#txtMaterialName3").val();

		var materialPara = $("#txtMaterialParameter3").val();

		var id = $("#EditMaterialWindow").attr("data-id");

		$.ajax(
			{
				type: "POST",
				contentType: "application/json",
				url: "WebMethods.aspx/updatePipeMaterialByPipeMaterialID",
				data: "{'pipematerialname':'" + materialName + "','pipematerialparameter':'" + materialPara + "','pipematerialid':'" + id + "'}",
				dataType: "json",
				success: function (msg) {

					var ret = $.parseJSON(msg.d);
					if (ret.msg == "ok") {

						$("#EditMaterialWindow").jqxWindow("close");
						RefreshMaterialList();
					}
				}
			});
	}

	function BindUserGroup(groupid) {

		$.ajax(
			{
				type: "POST",
				contentType: "application/json",
				url: "WebMethods.aspx/GetAllGroups",
				data: "{}",
				dataType: "json",
				success: function (msg) {

					var ret = $.parseJSON(msg.d);
					if (ret.msg == "ok") {

						$("#cmbUserGroup").jqxComboBox({ width: 160, valueMember: "GroupID", displayMember: "GroupName", source: ret.data, theme: skin, height: 20 });
						if (groupid != undefined) {
							$("#cmbUserGroup").jqxComboBox("val", groupid);
						}
					}
				}
			});

	}

	function AddMaterial() {

		var materialName = $("#txtMaterialName2").val();

		var materialPara = $("#txtMaterialParameter").val();

		$.ajax(
			{
				type: "POST",
				contentType: "application/json",
				url: "WebMethods.aspx/addPipeMaterial",
				data: "{'PipeMaterialName':'" + materialName + "','PipeMaterialParameter':'" + materialPara + "'}",
				dataType: "json",
				success: function (msg) {

					var ret = $.parseJSON(msg.d);
					if (ret.msg == "ok") {

						$("#AddMaterialWindow").jqxWindow("close");
						RefreshMaterialList();
					}
				}
			});
	}

	function AddWaterMeter() {

		var pipe = pipeContextMenu.target;

		var areaItem = $("#RegionTree1").jqxTree("getSelectedItem");
		var areaid = areaItem.value;

		$.ajax(
					{
						type: "POST",
						contentType: "application/json",
						url: "WebMethods.aspx/addWatermeterSimple",
						data: "{'areaid':'" + areaid + "','pipeid':'" + pipe.id + "','position':'" + pipeContextMenu.position + "'}",
						dataType: "json",
						success: function (msg) {

							var ret = $.parseJSON(msg.d);
							if (ret.msg == "ok") {

								LoadAllPipes();
							}
						}
					});


	}

	function LoadWaterbanlanceReport() {

	   var index=$('#leftTabs').jqxTabs('length');

	   var c = '<div  id="tabContent' + index + '"><iframe style="width:1920px; height:1080px" src="WaterBananceReport.aspx"  scrolling="no" frameborder="0"> </iframe> </div>';

	   $('#leftTabs').jqxTabs('addAt', index, "水平衡报表", c);

	}

	function LoadPipeHistoricReport() {

		
		var index = $('#leftTabs').jqxTabs('length');

		var c = '<div  id="tabContent' + index + '"><iframe style="width:1920px; height:1080px" src="HistoricExceptionReport.aspx"  scrolling="no" frameborder="0"> </iframe> </div>';

		$('#leftTabs').jqxTabs('addAt', index, "管道历史异常信息报表", c);
	}

	function LoadPropertyReport() {
		
		var index = $('#leftTabs').jqxTabs('length');

		var c = '<div  id="tabContent' + index + '"><iframe style="width:1920px; height:1080px" src="PropertyReport.aspx"  scrolling="no" frameborder="0"> </iframe> </div>';

		$('#leftTabs').jqxTabs('addAt', index, "固定资产报表", c);

	}

	function GetWaterLevel(id) {

		for (var i = 0; i < waterSource.length; i++) {

			if (waterSource[i].WaterMeterID == id) {

				return waterSource[i].Level;
			}
		}

		return 0;
	}

	function RefreshTooltip(PipeID) {

		$.ajax(
					{
						type: "POST",
						contentType: "application/json",
						url: "WebMethods.aspx/getPipeTips",
						data: "{'pipeid':'" + PipeID + "'}",
						dataType: "json",
						success: function (msg) {

							var ret = $.parseJSON(msg.d);
							if (ret.data == null || ret.data.length == 0) {
								//                                $("#TooltipWindow").html("无法获取异常处理信息！");
								return;
							}

							//  $("#TooltipWindow").html("处理结果：" + ret.data[0].ProcessResult + "<p>" + "处理描述:" + ret.data[0].ProcessDescription);
							$("#lbProcessResult").html(ret.data[0].ProcessResult);
							$("#lbProcessDesc").html(ret.data[0].ProcessDescription);

						}
					});


	}


	function SaveWatermeterInfo() {

		var watermeter = watermeterContextMenu.watermeter;

		var waterName = $("#txtWatermeterName").val().trim();
		if (waterName == "") {
			alert("请输入水表名称！");
			return;
		}
		var waterNo = $("#txtWatermeterNo").val().trim();
		if (waterNo == "") {
			alert("请输入传感器编号!");
			return;
		}
		var waterType = $("#txtWatermeterType").val().trim();

		var Admin = $("#cmbWatermeterAdmin").jqxComboBox("val");
		if (Admin == "") {
			alert("请输入管理员!");
			return;
		}

		var areaItem = $("#trWatermeterArea").jqxTree("getSelectedItem");
		if (areaItem == null) {
			alert("请输入所属区域!");
			return;
		}

		var levelItem = $("#cmbWaterLevel").jqxComboBox("getSelectedItem");

		if (levelItem == null) {
			alert("请输入水表级数!");
			return;
		}
		var level = levelItem.value;

		var preWater = $("#cmbPreWater").jqxComboBox("getSelectedItem");
		var preWaterID = 0;

		if (preWater != null) {

			preWaterID = preWater.value;
		}


		var waterRemark = $("#txtWatermeterRemark").val();

		$.ajax(
					{
						type: "POST",
						contentType: "application/json",
						url: "WebMethods.aspx/updateWatermeterByWatermeterID",
						data: "{'watermeterid':'" + watermeter.id + "','watermetername':'" + waterName + "','watermeterno':'" + waterNo + "','watermetertype':'" + waterType + "','areaid':'" + areaItem.value + "','adminuserid':'" + Admin + "','remark':'" + waterRemark + "','FatherWater':'" + preWaterID + "','Level':'" + level + "'}",
						dataType: "json",
						success: function (msg) {
							$("#WatermeterWindow").jqxWindow("close");
							var ret = $.parseJSON(msg.d);
							if (ret.msg == "ok") {
								alert("保存成功！");
							}
							else {
								alert("保存失败！");
							}

						}
					});

	}

	function SaveDeviceInfo() {

		var device = deviceContextMenu.device;
	 

		var deviceName = $("#txtDeviceName").val().trim();
		if (deviceName == "") {
			alert("请输入设备名称！");
			return;
		}
  
		var areaItem = $("#trDeviceArea").jqxTree("getSelectedItem");
		if (areaItem == null) {
			alert("请输入所属区域!");
			return;
		}

		var deviceremark = $("#txtDeviceRemark").val();

		$.ajax(
					{
						type: "POST",
						contentType: "application/json",
						url: "WebMethods.aspx/updateDevice",
						data: "{'deviceid':'" + device.id + "','devicename':'" + deviceName + "','areaid':'" + areaItem.value + "','remark':'" + deviceremark + "'}",
						dataType: "json",
						success: function (msg) {
							$("#DeviceWindow").jqxWindow("close");
							var ret = $.parseJSON(msg.d);
							if (ret.msg == "ok") {
								alert("保存成功！");
							}
							else {
								alert("保存失败！");
							}

						}
					});

	}

	function SaveSensorInfo() {

		var pipe = sensorContextMenu.target;
		var head = sensorContextMenu.head;
		var sensor = sensorContextMenu.sensor;

		var sensorName = $("#txtSensorName").val().trim();
		if (sensorName == "") {
			alert("请输入传感器名称！");
			return;
		}
		var sensorNo = $("#txtSensorNo").val().trim();
		if (sensorNo == "") {
			alert("请输入传感器编号!");
			return;
		}
		var sensorType = $("#txtSensorType").val().trim();

		var Admin = $("#cmbSensorAdmin").jqxComboBox("val");
		if (Admin == "") {
			alert("请输入管理员!");
			return;
		}

		var areaItem = $("#trSensorArea").jqxTree("getSelectedItem");
		if (areaItem == null) {
			alert("请输入所属区域!");
			return;
		}

		var sensorRemark = $("#txtSensorRemark").val();

		$.ajax(
					{
						type: "POST",
						contentType: "application/json",
						url: "WebMethods.aspx/updateSensorBySensorID",
						data: "{'sensorid':'" + sensor.id + "','sensorname':'" + sensorName + "','sensorno':'" + sensorNo + "','sensortype':'" + sensorType + "','sensorlocation':'" + "" + "','areaid':'" + areaItem.value + "','adminuserid':'" + Admin + "','remark':'" + sensorRemark + "'}",
						dataType: "json",
						success: function (msg) {
							$("#SensorWindow").jqxWindow("close");
							var ret = $.parseJSON(msg.d);
							if (ret.msg == "ok") {
								alert("保存成功！");
							}
							else {
								alert("保存失败！");
							}

						}
					});

	}

	function AdjustPosition() {

		var pipe = sensorContextMenu.target;
		var head = sensorContextMenu.head;

		var pos = "";
		if (head == 2) {
			pos = pipe.endPos.lng + "," + pipe.endPos.lat;
		}
		else {
			pos = pipe.startPos.lng + "," + pipe.startPos.lat;
		}

		$("#txtNewPos").val(pos);

		$("#AdjustPosWindow").jqxWindow("open");
	}

	function AddSensor() {

		var pipe = sensorContextMenu.target;
		var head = sensorContextMenu.head;
		var areaItem = $("#RegionTree1").jqxTree("getSelectedItem");
		var areaid = areaItem.value;
		var sencondhead = 0;
		if (head == 2) {
			sencondhead = 1;
		}

		$.ajax(
					{
						type: "POST",
						contentType: "application/json",
						url: "WebMethods.aspx/addSensorSimple",
						data: "{'areaid':'" + areaid + "','pipeid':'" + pipe.id + "','sencondhead':'" + sencondhead + "'}",
						dataType: "json",
						success: function (msg) {

							var ret = $.parseJSON(msg.d);
							if (ret.msg == "ok") {
								LoadAllPipes();
							}
						}
					});


	}


	function DeleteWatermeter() {
		if (!confirm("您确定要删除该水表吗?")) {
			return;
		}
		var watermeter = watermeterContextMenu.watermeter;
		$.ajax(
					{
						type: "POST",
						contentType: "application/json",
						url: "WebMethods.aspx/deleteWatermeterByWatermeterID",
						data: "{'watermeterid':'" + watermeter.id + "'}",
						dataType: "json",
						success: function (msg) {

							var ret = $.parseJSON(msg.d);

							if (ret.msg == "ok") {
								LoadAllPipes();
							}
						}
					});

	}

	function DeleteDevice() {
		if (!confirm("您确定要删除该设备吗?")) {
			return;
		}
		var device = deviceContextMenu.device;
		$.ajax(
					{
						type: "POST",
						contentType: "application/json",
						url: "WebMethods.aspx/deleteDevice",
						data: "{'deviceid':'" + device.id + "'}",
						dataType: "json",
						success: function (msg) {

							var ret = $.parseJSON(msg.d);

							if (ret.msg == "ok") {
								LoadAllPipes();
							}
						}
					});

	}

	function DeleteSensor() {
		var pipe = sensorContextMenu.target;
		var head = sensorContextMenu.head;
		var sensor = sensorContextMenu.sensor;

		if (!confirm("您确定要删除该传感器吗?")) {
			return;
		}
		$.ajax(
					{
						type: "POST",
						contentType: "application/json",
						url: "WebMethods.aspx/deleteSensorBySensorID",
						data: "{'sensorid':'" + sensor.id + "'}",
						dataType: "json",
						success: function (msg) {

							var ret = $.parseJSON(msg.d);

							if (ret.msg == "ok") {
								LoadAllPipes();
							}
						}
					});



	}

	function DeletePipe() {

		if (!confirm("您确定要删除该管道吗？！")) {
			return;
		}

		var pipe = pipeContextMenu.target;

		$.ajax(
					{
						type: "POST",
						contentType: "application/json",
						url: "WebMethods.aspx/deletePipeByPipeID",
						data: "{'pipeid':'" + pipe.id + "'}",
						dataType: "json",
						success: function (msg) {

							var ret = $.parseJSON(msg.d);


							if (ret.msg == "ok") {
								LoadAllPipes();
							}


						}
					});

	}

	function ShowPipeInfo() {

		var pipe = pipeContextMenu.target;

		$.ajax(
					{
						type: "POST",
						contentType: "application/json",
						url: "WebMethods.aspx/getPipeByPipeID",
						data: "{'pipeid':'" + pipe.id + "'}",
						dataType: "json",
						success: function (msg) {


							$('#PipeWindow').jqxWindow('open');
							var ret = $.parseJSON(msg.d);


							if (ret.msg == "ok") {
								$("#txtPipeName").val(ret.data[0].PipeName);
								$("#txtPipeNo").val(ret.data[0].PipeNo);

								var item1 = $("#cmbAdmins").jqxComboBox('getItemByValue', ret.data[0].AdminUserID);
								if (item1 != null) {
									$("#cmbAdmins").jqxComboBox("selectItem", item1);
								}
								else {
									$("#cmbAdmins").jqxComboBox("selectIndex", -1);
								}


								// $('#trPipeArea').jqxTree('selectItem', $("#trPipeArea").find('#' + ret.data[0].AreaID)[0]);
								SelectArea("trPipeArea", ret.data[0].AreaID);
								$("#txtPipeWidth").jqxNumberInput("val", ret.data[0].PipeSize);
								$("#txtPipeDepth").jqxNumberInput("val", parseFloat(ret.data[0].PipeDepth));
								$("#txtPipeLength").jqxNumberInput("val", parseFloat(ret.data[0].PipeLength));


								// $("#cmbMaterial").jqxComboBox("val", ret.data[0].PipeMaterialID);
								var item2 = $("#cmbMaterial").jqxComboBox('getItemByValue', ret.data[0].PipeMaterialID);
								if (item2 != null) {
									$("#cmbMaterial").jqxComboBox("selectItem", item2);
								}
								else {
									$("#cmbMaterial").jqxComboBox("selectIndex", -1);
								}

								$("#txtPipeRemark").val(ret.data[0].Remark);
							}


						}
					});

	}


	function SelectArea(treeid, areaid) {

		var items = $('#' + treeid).jqxTree("getItems");  // $('#trPipeArea').jqxTree("getItems");

		for (var i = 0; i < items.length; i++) {
			if (items[i].value == areaid) {

				$('#' + treeid).jqxTree("selectItem", items[i].element);

			}
		}
	}






	function SavePipeInfo() {
		var pipe = pipeContextMenu.target;

		var pipeName = $("#txtPipeName").val().trim();
		if (pipeName == "") {
			alert("请输入管道名称！");
			return;
		}
		var pipeNo = $("#txtPipeNo").val().trim();
		if (pipeNo == "") {
			alert("请输入管道编号!");
			return;
		}

		var Admin = $("#cmbAdmins").jqxComboBox("val");
		if (Admin == "") {
			alert("请输入管理员!");
			return;
		}

		var pipeWidth = $("#txtPipeWidth").jqxNumberInput("val");

		var areaItem = $("#trPipeArea").jqxTree("getSelectedItem");
		if (areaItem == null) {
			alert("请输入所属区域!");
			return;
		}

		var Material = $("#cmbMaterial").jqxComboBox("val");
		if (Material == undefined || Material == null || Material == "") {
			alert("请输入管材!");
			return;
		}
		var pipeDepth = $("#txtPipeDepth").jqxNumberInput("val");
		var pipeLength = $("#txtPipeLength").jqxNumberInput("val");
		var pipeRemark = $("#txtPipeRemark").val();

		$.ajax(
					{
						type: "POST",
						contentType: "application/json",
						url: "WebMethods.aspx/updatePipeByPipeID",
						data: "{'PipeID':'" + pipe.id + "','PipeName':'" + pipeName + "','PipeNo':'" + pipeNo + "','AreaID':'" + areaItem.value + "','PipeMaterialID':'" + Material + "','PipeSize':'" + pipeWidth + "','PipeDepth':'" + pipeDepth + "','PipeLength':'" + pipeLength + "','StartLocation':'" + "0" + "','EndLocation':'" + "0" + "','AdjoinPipeIDs':'" + "0" + "','AdjoinPipeNames':'" + "0" + "','AdminUserID':'" + Admin + "','Remark':'" + pipeRemark + "','PrePipeID':'" + pipe.PrePipe + "'}",
						dataType: "json",
						success: function (msg) {
							$("#PipeWindow").jqxWindow("close");
							var ret = $.parseJSON(msg.d);
							if (ret.msg == "ok") {
								alert("保存成功！");
							}
							else {
								alert("保存失败！");
							}

						}
					});
	}

	function DragPipe(e) {

		var polyline = e.target;
		//  alert(typeof(polyline);
		CurrentPipePoints = polyline.getPath();
		//  UpdatePipeLocation();
		MovePipeNodes(polyline);

	}

	function EditWatermeterInfo() {
		var watermeter = watermeterContextMenu.watermeter;


		$.ajax(
					{
						type: "POST",
						contentType: "application/json",
						url: "WebMethods.aspx/getWatermeterByWatermeterID",
						data: "{'watermeterid':'" + watermeter.id + "'}",
						dataType: "json",
						success: function (msg) {


							$('#WatermeterWindow').jqxWindow('open');
							var ret = $.parseJSON(msg.d);

							if (ret.msg == "ok") {
								$("#txtWatermeterName").val(ret.data[0].WaterMeterName);
								$("#txtWatermeterNo").val(ret.data[0].WaterMeterNo);

								var item1 = $("#cmbWatermeterAdmin").jqxComboBox('getItemByValue', ret.data[0].AdminUserID);
								if (item1 != null) {
									$("#cmbWatermeterAdmin").jqxComboBox("selectItem", item1);
								}
								else {
									$("#cmbWatermeterAdmin").jqxComboBox("selectIndex", -1);
								}
								if (ret.data[0].AreaID != "-1") {
									SelectArea("trWatermeterArea", ret.data[0].AreaID);
								}
								$("#txtWatermeterType").val(ret.data[0].WaterMeterType);
								$("#txtWatermeterRemark").val(ret.data[0].Remark);
								$("#cmbWaterLevel").jqxComboBox('val', ret.data[0].Level);
								BindPreWaters(parseInt(ret.data[0].FatherWaterMeterID));
							}


						}
					});
	}



	function EditDeviceInfo() {

		var device = deviceContextMenu.device;


		$.ajax(
					{
						type: "POST",
						contentType: "application/json",
						url: "WebMethods.aspx/getDeviceInfo",
						data: "{'deviceid':'" + device.id + "'}",
						dataType: "json",
						success: function (msg) {

					   

							$('#DeviceWindow').jqxWindow('open');
							var ret = $.parseJSON(msg.d);

							if (ret.msg == "ok") {
								$("#txtDeviceName").val(ret.data[0].DeviceName);
								$("#txtDeviceReamrk").val(ret.data[0].Remark);

								if (ret.data[0].AreaID != "-1") {
									SelectArea("trDeviceArea", ret.data[0].AreaID);
								}

							}


						}
					});



	}


	function EditSensorInfo() {

		var pipe = sensorContextMenu.target;
		var head = sensorContextMenu.head;
		var sensor = sensorContextMenu.sensor;

		$.ajax(
					{
						type: "POST",
						contentType: "application/json",
						url: "WebMethods.aspx/getSensorBySensorID",
						data: "{'sensorid':'" + sensor.id + "'}",
						dataType: "json",
						success: function (msg) {


							$('#SensorWindow').jqxWindow('open');
							var ret = $.parseJSON(msg.d);

							if (ret.msg == "ok") {
								$("#txtSensorName").val(ret.data[0].SensorName);
								$("#txtSensorNo").val(ret.data[0].SensorNo);
								var item1 = $("#cmbSensorAdmin").jqxComboBox('getItemByValue', ret.data[0].AdminUserID);

								if (item1 != null) {
									$("#cmbSensorAdmin").jqxComboBox("selectItem", item1);
								}
								else {
									$("#cmbSensorAdmin").jqxComboBox("selectIndex", -1);
								}
								if (ret.data[0].AreaID != "-1") {
									SelectArea("trSensorArea", ret.data[0].AreaID);
								}
								$("#txtSensorType").val(ret.data[0].SensorType);
								$("#txtSensorRemark").val(ret.data[0].Remark);
							}


						}
					});



	}

	function BeginEditPipe() {
		var pipe = pipeContextMenu.target;
		ActivePipe(pipe);

	}

	function GetLocationStr(Position) {

		return Position.lng + "," + Position.lat;

	}


	function MovePipeNodes(polyline) {

		$.ajax(
					{
						type: "POST",
						contentType: "application/json",
						url: "WebMethods.aspx/updatePipeLocation",
						data: "{'pipeid':'" + polyline.id + "','location':'" + GetPipeLocationStr(polyline) + "'}",
						dataType: "json",
						success: function (msg) {


						}
					});
	}


	var CurrentEndPixel = null;
	var CurrentStartPixel = null;
	var nodewidth = 8;
	function DrawPipe(pipe) {//圆球形节点

		var startPixel = map.pointToPixel(pipe.startPos);

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

		//  CurrentStartPixel = startPixel;
		var startX = startPixel.x;
		var startY = startPixel.y;

		var endPixel = map.pointToPixel(pipe.endPos);
		var endX = endPixel.x;
		var endY = endPixel.y;

		//  CurrentEndPixel = endPixel;
		var canvas = document.getElementById('OverlayCanvas');
		var context = canvas.getContext('2d');
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
		if (pipe.status == 1) {
			grd.addColorStop(0, 'rgb(67,66,64)');
			grd.addColorStop(0.25, 'rgb(159,158,156)');
			grd.addColorStop(0.5, 'rgb(254,254,254)');
			grd.addColorStop(0.75, 'rgb(159,158,156)');
			grd.addColorStop(1, 'rgb(67,66,64)');
		}
		else if (pipe.status == 0) {



			grd.addColorStop(0, '#750000');
			grd.addColorStop(0.25, '#ff2d2d');
			grd.addColorStop(0.5, '#ff9797');
			grd.addColorStop(0.75, '#ff2d2d');
			grd.addColorStop(1, '#750000');

		}
		else if (pipe.status == 2) {
			grd.addColorStop(0, '#796400');
			grd.addColorStop(0.25, '#d9b300');
			grd.addColorStop(0.5, '#ffe153');
			grd.addColorStop(0.75, '#d9b300');
			grd.addColorStop(1, '#796400');

		}
		context.strokeStyle = grd;
		context.stroke();

		if (pipe.PrePipe != "0") {
			context.beginPath();
			context.arc(startX, startY, radious, 0, 2 * Math.PI, false);
			var grd3 = context.createRadialGradient(startX, startY, 0, startX, startY, radious);

			grd3.addColorStop(1, 'rgb(67,66,64)');
			grd3.addColorStop(0.5, 'rgb(159,158,156)');
			grd3.addColorStop(0, 'rgb(254,254,254)');

			context.fillStyle = grd3;
			context.fill();
		}

		if (pipe.isActive) {



			context.beginPath();
			context.rect(startX - nodewidth - 2, startY - nodewidth - 2, 4, 4);
			context.lineWidth = 1;
			context.strokeStyle = 'blue';
			context.stroke();
			context.beginPath();
			context.rect(startX - nodewidth - 2, startY + nodewidth - 2, 4, 4);
			context.stroke();
			context.beginPath();
			context.rect(startX + nodewidth - 2, startY + nodewidth - 2, 4, 4);
			context.stroke();
			context.beginPath();
			context.rect(startX + nodewidth - 2, startY - nodewidth - 2, 4, 4);
			context.stroke();
			context.beginPath();
			context.rect(endX - nodewidth - 2, endY - nodewidth - 2, 4, 4);
			context.stroke();
			context.beginPath();
			context.rect(endX - nodewidth - 2, endY + nodewidth - 2, 4, 4);
			context.stroke();
			context.beginPath();
			context.rect(endX + nodewidth - 2, endY + nodewidth - 2, 4, 4);
			context.stroke();
			context.beginPath();
			context.rect(endX + nodewidth - 2, endY - nodewidth - 2, 4, 4);

			context.stroke();
		}

	}


	function CusorInPipeHead(pos, pipe) {

		var startPixel = map.pointToPixel(pipe.startPos);
		var startX = startPixel.x;
		var startY = startPixel.y;

		var endPixel = map.pointToPixel(pipe.endPos);
		var endX = endPixel.x;
		var endY = endPixel.y;

		if (pos.x > startX - 6 && pos.x < startX + 6 && pos.y > startY - 6 && pos.y < startY + 6) {
			return 1;
		}

		if (pos.x > endX - 6 && pos.x < endX + 6 && pos.y > endY - 6 && pos.y < endY + 6) {
			return 2;
		}

		return false;
	}

	var MovingPipe = null;
	var MovingStartNode = true;
	var MovingSensor = null;
	function BeginDragPipe(evt) {

		var pos = getMousePos(evt);
		if (CurrentActivePipe == null) {
			return;
		}
		var ret = CusorInPipeHead(pos, CurrentActivePipe);
		if (ret == 1) {

			MovingPipe = CurrentActivePipe;
			MovingStartNode = true;
			map.disableDragging();
		}
		else if (ret == 2) {

			MovingPipe = CurrentActivePipe;
			MovingStartNode = false;
			map.disableDragging();

		}
		else {

			MovingPipe = null;
			map.enableDragging();
		}

		MovingSensor = CursorInSensor(pos);


	}


	function RedrawPipes() {
		var canvas = document.getElementById('OverlayCanvas');
		var context = canvas.getContext('2d');
		context.clearRect(0, 0, canvas.width, canvas.height);

		for (var i = 0; i < Pipes.length; i++) {
			DrawPipe(Pipes[i]);
		}

		for (var i = 0; i < Sensors.length; i++) {
			DrawSensor(Sensors[i]);
		}

		for (var i = 0; i < Watermeters.length; i++) {
			DrawWatermeter(Watermeters[i]);
		}

		for (var i = 0; i < Devices.length; i++) {
			DrawDevice(Devices[i]);
		}
	}



	function GetSensorPosition(sensor) {

		for (var i = 0; i < Pipes.length; i++) {
			//     console.log(Pipes[i].id + ":" + sensor.pipeID);
			if (Pipes[i].id == sensor.pipeID) {

				if (sensor.secondHead == "0") {
					return Pipes[i].startPos;
				}
				else {
					return Pipes[i].endPos;
				}

			}
		}

		return false;

	}


	function DrawWatermeter(watermeter) {

		var pipe = GetPipe(watermeter.pipeid);
		if (pipe == null)
			return;
		var position = parseFloat(watermeter.position);
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
		var startPixel = map.pointToPixel(pipe.startPos);
		var endPixel = map.pointToPixel(pipe.endPos);

		var deltax = endPixel.x - startPixel.x;
		var deltay = endPixel.y - startPixel.y;

		var watermeterx = parseInt(startPixel.x + deltax * position);
		var watermetery = parseInt(startPixel.y + deltay * position);

		var canvas = document.getElementById('OverlayCanvas');
		var context = canvas.getContext('2d');
		context.drawImage(WatermeterImage, watermeterx - harfwidth, watermetery - harfwidth, width, width);
		watermeter.x = watermeterx;
		watermeter.y = watermetery;

		if (watermeter.Name != null && watermeter.Name != undefined && watermeter.Name != "" && zoom == 19) {
			context.font = '7pt Calibri';
			context.fillStyle = 'blue';
			context.fillText(watermeter.Name, watermeter.x - 10, watermeter.y - 12);
			console.log(watermeter.Name);
		}

	}


	function DrawSensor(sensor) {

		var pos = GetSensorPosition(sensor);

		if (pos == false)
			return;

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
		context.drawImage(SensorImage, startPixel.x - harfwidth, startPixel.y - harfwidth, width, width);
		//context.drawImage(SensorImage, startPixel.x - 18, startPixel.y - 18, 36, 36);


		if (sensor.Name != null && sensor.Name != undefined && sensor.Name != ""&&zoom==19) {
			context.font = '7pt Calibri';
			context.fillStyle = 'blue';
			context.fillText(sensor.Name, startPixel.x - 10, startPixel.y - 12);
		}
	}


	function GetPipe(PipeId) {

		for (var i = 0; i < Pipes.length; i++) {

			if (Pipes[i].id == PipeId) {
				return Pipes[i];
			}
		}

		return null;
	}

	function FixAllSubPipesStartLocation(PrePipeId, location, isSave) {
		for (var i = 0; i < Pipes.length; i++) {

			if (Pipes[i].PrePipe == PrePipeId) {
				Pipes[i].startPos = location;
				if (isSave) {
					UpdatePipeLocation(Pipes[i]);
				}
			}
		}

	}
	function SavePosition() {

		SaveAdjustPos();

		$("#AdjustPosWindow").jqxWindow("close");

	}

	function SaveAdjustPos() {
		var pipe = sensorContextMenu.target;
		var head = sensorContextMenu.head;

		var sencondhead = 0;
		var startPosStr = $("#txtNewPos").val().split(",");

		var NewPos = new BMap.Point(parseFloat(startPosStr[0]), parseFloat(startPosStr[1]));

		if (pipe == null)
			return;

		if (head == 1) {
			pipe.startPos = NewPos;

			if (pipe.PrePipe != "0") {
				var prePipe = GetPipe(pipe.PrePipe);
				prePipe.endPos = startPos;
				UpdatePipeLocation(prePipe);
				FixAllSubPipesStartLocation(prePipe.id, startPos, true);

			}

		}
		else if (head == 2) {

			pipe.endPos = NewPos;
			UpdatePipeLocation(pipe);
			FixAllSubPipesStartLocation(pipe.id, NewPos, true);

		}
		RedrawPipes();

	}



	function DragingPipe(evt) {
		var pos = getMousePos(evt);
		var pixel = new BMap.Pixel(pos.x, pos.y);
		var location = map.pixelToPoint(pixel)
		if (MovingPipe != null) {
			if (MovingStartNode) {

				MovingPipe.startPos = location;

				if (MovingPipe.PrePipe != "0") {
					var prePipe = GetPipe(MovingPipe.PrePipe);
					prePipe.endPos = location;
					//   UpdatePipeLocation(prePipe);
					FixAllSubPipesStartLocation(prePipe.id, location, false);

				}
			}
			else {
				MovingPipe.endPos = location;
				//  UpdatePipeLocation(MovingPipe);
				FixAllSubPipesStartLocation(MovingPipe.id, location, false);
			}
			if (MovingSensor != null) {

				MovingSensor.location = location;
			}
			RedrawPipes();

		}
	}

	function PipeHitTest(x, y, pipe) {

		var startPixel = map.pointToPixel(pipe.startPos);
		var x1 = startPixel.x;
		var y1 = startPixel.y;
		var endPixel = map.pointToPixel(pipe.endPos);
		var x2 = endPixel.x;
		var y2 = endPixel.y;
		var temp = 0;
		if (x1 > x2) {
			temp = x1;
			x1 = x2;
			x2 = temp;

			temp = y1;
			y1 = y2;
			y2 = temp;
		}


		var k = (y1 - y2) / (x1 - x2);
		var d = 6 * Math.sqrt(k * k + 1);
		var In = false;
		if (x1 != x2 && y1 == y2) {
			if (x1 > x2) {
				x1 = x1 + x2;
				x2 = x1 - x2;
				x1 = x1 - x2;
			}
			if (x1 < x && x < x2 && y1 - 6 < y && y < y1 + 6)
				In = true;
		}
		else if (x1 == x2 && y1 != y2) {
			if (y1 > y2) {
				y1 = y1 + y2;
				y2 = y1 - y2;
				y1 = y1 - y2;
			}
			if (x1 - 6 < x && x < x1 + 6 && y1 < y && y < y2)
				In = true;
		}
		else if (x1 != x2 && y1 != y2) {

			if (k * (y - y1) + x - x1 > 0 && k * (y - y2) + x - x2 < 0 && y - y1 - k * (x - x1) + d > 0 && y - y1 - k * (x - x1) - d < 0)
				In = true;
		}

		return In;
	}

	function EndDragPipe(evt) {
		var pos = getMousePos(evt);
		var pixel = new BMap.Pixel(pos.x, pos.y);
		var location = map.pixelToPoint(pixel)

		if (MovingPipe != null) {
			if (MovingStartNode) {

				MovingPipe.startPos = location;

				if (MovingPipe.PrePipe != "0") {
					var prePipe = GetPipe(MovingPipe.PrePipe);
					prePipe.endPos = location;
					UpdatePipeLocation(prePipe);
					FixAllSubPipesStartLocation(prePipe.id, location, true);
				}
			}
			else {
				MovingPipe.endPos = location;
				UpdatePipeLocation(MovingPipe);
				FixAllSubPipesStartLocation(MovingPipe.id, location, true);
			}
			if (MovingSensor != null) {
				MovingSensor.location = location;
			}
			RedrawPipes();

		}
		MovingSensor = null;
		MovingPipe = null;
		MovingStartNode = true;
		map.enableDragging();
		UpdatePipeLocation(CurrentActivePipe);
	}



	function AddNewValve() {

		var areaItem = $("#RegionTree1").jqxTree("getSelectedItem");
		if (areaItem == null) {
			alert("请选择区域!");
			return;
		}

		$.ajax(
				{
					type: "POST",
					contentType: "application/json",
					url: "WebMethods.aspx/addDevice",
					data: "{'areaid':'" + areaItem.value + "','location':'" + GetLocationStr(CurrentLocation) + "','deviceType':'1' }",
					dataType: "json",
					success: function (msg) {
						var ret = $.parseJSON(msg.d);

						if (ret.msg == "ok") {
							LoadAllPipes();
						}

					}
				});
	}


	function AddNewFirePlug() {

		var areaItem = $("#RegionTree1").jqxTree("getSelectedItem");
		if (areaItem == null) {
			alert("请选择区域!");
			return;
		}

		$.ajax(
				{
					type: "POST",
					contentType: "application/json",
					url: "WebMethods.aspx/addDevice",
					data: "{'areaid':'" + areaItem.value + "','location':'" + GetLocationStr(CurrentLocation) + "','deviceType':'2' }",
					dataType: "json",
					success: function (msg) {
						var ret = $.parseJSON(msg.d);

						if (ret.msg == "ok") {
							LoadAllPipes();
						}

					}
				});
	}


	function AddNewWell() {

		var areaItem = $("#RegionTree1").jqxTree("getSelectedItem");
		if (areaItem == null) {
			alert("请选择区域!");
			return;
		}

		$.ajax(
				{
					type: "POST",
					contentType: "application/json",
					url: "WebMethods.aspx/addDevice",
					data: "{'areaid':'" + areaItem.value + "','location':'" + GetLocationStr(CurrentLocation) + "','deviceType':'3' }",
					dataType: "json",
					success: function (msg) {
						var ret = $.parseJSON(msg.d);

						if (ret.msg == "ok") {
							LoadAllPipes();
						}

					}
				});
	}

	function AddNewMark() {

		var areaItem = $("#RegionTree1").jqxTree("getSelectedItem");
		if (areaItem == null) {
			alert("请选择区域!");
			return;
		}

		$.ajax(
				{
					type: "POST",
					contentType: "application/json",
					url: "WebMethods.aspx/addDevice",
					data: "{'areaid':'" + areaItem.value + "','location':'" + GetLocationStr(CurrentLocation) + "','deviceType':'4' }",
					dataType: "json",
					success: function (msg) {
						var ret = $.parseJSON(msg.d);

						if (ret.msg == "ok") {
							LoadAllPipes();
						}

					}
				});
	}
  
	function DrawDevice(device) {

		var pos = device.Location;

		if (pos == null)
			return;

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

		if (device.DeviceType == 1) {
			context.drawImage(ValveImage, startPixel.x - harfwidth, startPixel.y - harfwidth, width, width);
		}
		else if (device.DeviceType == 2) {
			context.drawImage(FirePlugImage, startPixel.x - harfwidth, startPixel.y - harfwidth, width, width);
		}
		else if (device.DeviceType == 3) {
			context.drawImage(WellImage, startPixel.x - harfwidth, startPixel.y - harfwidth, width, width);
		}
		else if (device.DeviceType == 4) {
			context.drawImage(MarkImage, startPixel.x - harfwidth, startPixel.y - harfwidth, width, width);
		}
	 
		if (device.Name != null && device.Name != undefined && device.Name != ""&&zoom==19) {
			context.font = '7pt Calibri';
			context.fillStyle = 'blue';
			context.fillText(device.Name, startPixel.x-10, startPixel.y-12);
		}
	}


	function LoadAllDevices()
	{
	   Devices = new Array();
		var areaitem = $('#RegionTree1').jqxTree("getSelectedItem");
		var areaid = -1;
		if (areaitem != null) {
			areaid = areaitem.value;
		}

		$.ajax(
			{
				type: "POST",
				contentType: "application/json",
				url: "WebMethods.aspx/getDevicesIncludeSubAreas",
				data: "{'areaid':'" + areaid + "'}",
				dataType: "json",
				success: function (msg) {
			   
					var ret = $.parseJSON(msg.d);
					if (ret == null || ret.data == null)
						return;
					for (var i = 0; i < ret.data.length; i++) {

						var startPosStr = ret.data[i].Location.split(",");


						var StartPos = new BMap.Point(parseFloat(startPosStr[0]), parseFloat(startPosStr[1]));

						var device = new Device(StartPos, ret.data[i].TypeFlag, ret.data[i].DeviceID, ret.data[i].DeviceName);

						Devices.push(device);
						DrawDevice(device);
					}
				}
			}
			);



	}

	function NewEmptyPipe() {

		CurrentStartPixel = map.pointToPixel(CurrentLocation);
		var endPos = new BMap.Point(CurrentLocation.lng + 0.001, CurrentLocation.lat);
		CurrentEndPixel = map.pointToPixel(endPos);
		var pipe = new Pipe(CurrentLocation, endPos, 0);
		// pipe.isActive = true;
		pipe.PrePipe = 0;
		pipe.status = 1;

		var areaItem = $("#RegionTree1").jqxTree("getSelectedItem");
		if (areaItem == null) {
			alert("请选择区域!");
			return;
		}


		$.ajax(
				{
					type: "POST",
					contentType: "application/json",
					url: "WebMethods.aspx/addPipe",
					data: "{'AreaID':'" + areaItem.value + "','StartLocation':'" + GetLocationStr(pipe.startPos) + "','EndLocation':'" + GetLocationStr(pipe.endPos) + "','PrePipeID':'0'" + "}",
					dataType: "json",
					success: function (msg) {
						var ret = $.parseJSON(msg.d);

						if (ret.msg == "ok") {
							pipe.id = ret.data[0].NewPipeID;
							pipe.PrePipe = "0";
							Pipes.push(pipe);
							ActivePipe(pipe);
						}

					}
				});


		// DrawPipe(pipe);
	}


	var CurrentActivePipe = null;
	function ActivePipe(pipe) {

		for (var i = 0; i < Pipes.length; i++) {
			Pipes[i].isActive = false;
		}
		pipe.isActive = true;
		CurrentActivePipe = pipe;
		RedrawPipes();
	}

	function AddNextPipe() {

		if (CurrentActivePipe == null)
			return;

		var pipe = new Pipe(CurrentActivePipe.endPos, CurrentLocation, 0);

		var areaItem = $("#RegionTree1").jqxTree("getSelectedItem");
		if (areaItem == null) {
			alert("请选择区域!");
			return;
		}


		$.ajax(
				{
					type: "POST",
					contentType: "application/json",
					url: "WebMethods.aspx/addPipe",
					data: "{'AreaID':'" + areaItem.value + "','StartLocation':'" + GetLocationStr(pipe.startPos) + "','EndLocation':'" + GetLocationStr(pipe.endPos) + "','PrePipeID':'" + CurrentActivePipe.id + "'" + "}",
					dataType: "json",
					success: function (msg) {
						var ret = $.parseJSON(msg.d);

						if (ret.msg == "ok") {
							pipe.id = ret.data[0].NewPipeID;
							pipe.PrePipe = CurrentActivePipe.id;
							pipe.status = 1;
							Pipes.push(pipe);
							RedrawPipes();
							//   ActivePipe(pipe);
						}

					}
				});



	}



	function ContactLocation() {
		var s = "";

		for (var i = 0; i < CurrentPipePoints.length; i++) {
			s = s + CurrentPipePoints[i].lng + "," + CurrentPipePoints[i].lat;

			if (i < CurrentPipePoints.length - 1) {
				s += "@";
			}
		}
		return s;
	}


	function getMousePos(evt) {
		var canvas = document.getElementById('OverlayCanvas');
		var rect = canvas.getBoundingClientRect();
		return {
			x: evt.clientX - rect.left,
			y: evt.clientY - rect.top
		};
	}

	function BindMaterial(id) {

		$.ajax(
			{
				type: "POST",
				contentType: "application/json",
				url: "WebMethods.aspx/getAllPipeMaterial",
				data: "{}",
				dataType: "json",
				success: function (msg) {


					var RegionData = $.parseJSON(msg.d);

					$("#" + id).jqxComboBox({ selectedIndex: 0, width: 120, valueMember: "PipeMaterialID", displayMember: "PipeMaterialName", source: RegionData.data, theme: skin, height: 20 });



				}
			});


	}

	function UpdatePipeLocation(pipe) {
		if (pipe == null)
			return;
		$.ajax(
					{
						type: "POST",
						contentType: "application/json",
						url: "WebMethods.aspx/updatePipeByPipeIDForPosition",
						data: "{'PipeID':'" + pipe.id + "','StartLocation':'" + GetLocationStr(pipe.startPos) + "','EndLocation':'" + GetLocationStr(pipe.endPos) + "'}",
						dataType: "json",
						success: function (msg) {
							var ret = $.parseJSON(msg.d);
							//                          
							//                            if (ret.msg == "ok") {

							//                                // CurrentPolyline.id = ret.data[0].PipeID;
							//                            }

						}
					});


	}

	function AddPipeLocation() {
		var areaitem = $('#RegionTree1').jqxTree("getSelectedItem");
		var areaid = -1;
		if (areaitem != null) {
			areaid = areaitem.value;
		}

		$.ajax(
					{
						type: "POST",
						contentType: "application/json",
						url: "WebMethods.aspx/addPipeLocation",
						data: "{'location':'" + ContactLocation() + "','areaid':'" + areaid + "'}",
						dataType: "json",
						success: function (msg) {


							var ret = $.parseJSON(msg.d);
							if (ret.msg == "ok") {

								CurrentPolyline.id = ret.data[0].PipeID;
							}

						}
					});

	}


	function BindUsers(id) {

		$.ajax(
			{
				type: "POST",
				contentType: "application/json",
				url: "WebMethods.aspx/getAllUser",
				data: "{}",
				dataType: "json",
				success: function (msg) {
					var RegionData = $.parseJSON(msg.d);

					$("#" + id).jqxComboBox({ selectedIndex: 0, width: 120, valueMember: "UserID", displayMember: "UserName", source: RegionData.data, theme: skin, height: 20 });
				}
			});

	}

	function ShowPipeContextMenu(pipe, pixel) {

		//  var pixel = map.pointToPixel(e.point);
		var y = $("#allmap").offset().top;
		var x = $("#allmap").offset().left;
		pipeContextMenu.jqxMenu('open', parseInt(pixel.x) + 5+x, parseInt(pixel.y) + 5+y);
		pipeContextMenu.target = pipe;

		var startPixel = map.pointToPixel(pipe.startPos);
		var endPixel = map.pointToPixel(pipe.endPos);

		var length = Math.sqrt((startPixel.x - endPixel.x) * (startPixel.x - endPixel.x) + (startPixel.y - endPixel.y) * (startPixel.y - endPixel.y))
		var lenth2 = Math.sqrt((startPixel.x - pixel.x) * (startPixel.x - pixel.x) + (startPixel.y - pixel.y) * (startPixel.y - pixel.y))

		var rate = lenth2 / length;

		pipeContextMenu.position = rate;

	}

	function ShowWatermeterMenu(watermeter, pixel) {

		var y = $("#allmap").offset().top;
		var x = $("#allmap").offset().left;

		watermeterContextMenu.jqxMenu('open', parseInt(pixel.x) + 5+x, parseInt(pixel.y) + 5+y);

		watermeterContextMenu.watermeter = watermeter;
	}

	function ShowDeviceMenu(device, pixel) {

		var y = $("#allmap").offset().top;
		var x = $("#allmap").offset().left;

		deviceContextMenu.jqxMenu('open', parseInt(pixel.x) + 5 + x, parseInt(pixel.y) + 5 + y);

		deviceContextMenu.device = device;
	}

	function ShowSensorMenu(pipe, pixel, node, sensor) {
		var y = $("#allmap").offset().top;
		var x = $("#allmap").offset().left;
		sensorContextMenu.jqxMenu('open', parseInt(pixel.x) + 5+x, parseInt(pixel.y) + 5+y);
		sensorContextMenu.target = pipe;

		sensorContextMenu.head = node;
		sensorContextMenu.sensor = sensor;
	}




	function GetAreaLocationByID(AreaID) {

		for (var i = 0; i < CurrentAreas.length; i++) {
			if (CurrentAreas[i].AreaID == AreaID) {
				return CurrentAreas[i].Location;
			}
		}
		return "1";
	}

	function GetSensorByID(SensorID) {
		for (var i = 0; i < Sensors.length; i++) {
			if (Sensors[i].id == SensorID)
				return Sensors[i];
		}

		return null;
	}

	function GetPipeByID(PipeID) {
		for (var i = 0; i < Pipes.length; i++) {
			if (Pipes[i].id == PipeID)
				return Pipes[i];
		}

		return null;
	}

	var Watermeters = null;

	function LoadAllWatermeters() {
		Watermeters = new Array();
		var areaitem = $('#RegionTree1').jqxTree("getSelectedItem");
		var areaid = -1;
		if (areaitem != null) {
			areaid = areaitem.value;
		}

		$.ajax(
					{
						type: "POST",
						contentType: "application/json",
						url: "WebMethods.aspx/GetWatermetersIncludeSubAreas",
						data: "{'areaid':'" + areaid + "'}",
						dataType: "json",
						success: function (msg) {
						   
							var ret = $.parseJSON(msg.d);
							if (ret == null || ret.data == null)
								return;
							for (var i = 0; i < ret.data.length; i++) {
								var watermeter = new Watermeter(ret.data[i].PipeID, ret.data[i].Position, ret.data[i].WaterMeterID, ret.data[i].WaterMeterName);

								Watermeters.push(watermeter);
								DrawWatermeter(watermeter);
							}
						}
					}
					);

	}



	function SensorHistoricQuery() {
		var sensor = sensorContextMenu.sensor;
	  //  window.open("SensorData.aspx?sensor=" + sensor.id);



		var index = $('#leftTabs').jqxTabs('length');

		var c = '<div  id="tabContent' + index + '"><iframe style="width:1920px; height:1080px" src="SensorData.aspx?sensor=' + sensor.id+'"  scrolling="no" frameborder="0"> </iframe> </div>';

		$('#leftTabs').jqxTabs('addAt', index, "传感器历史数据查询", c);
	}

	function SensorCurrentQuery() {
		var sensor = sensorContextMenu.sensor;
	   // window.open("currentsensordata.aspx?sensor=" + sensor.id);

		var index = $('#leftTabs').jqxTabs('length');

		var c = '<div  id="tabContent' + index + '"><iframe style="width:1920px; height:1080px" src="currentsensordata.aspx?sensor=' + sensor.id + '"  scrolling="no" frameborder="0"> </iframe> </div>';

		$('#leftTabs').jqxTabs('addAt', index, "传感器当前数据查询", c);
	}

	function LoadWatermeterDataPage() {
		var watermeter = watermeterContextMenu.watermeter;

	  //  window.open("WatermeterData.aspx?watermeter=" + watermeter.id);

		var index = $('#leftTabs').jqxTabs('length');

		var c = '<div  id="tabContent' + index + '"><iframe style="width:1920px; height:1080px" src="WatermeterData.aspx?watermeter=' + watermeter.id + '"  scrolling="no" frameborder="0"> </iframe> </div>';

		$('#leftTabs').jqxTabs('addAt', index, "水表历史数据查询", c);

	}

	function LoadAllSensor() {

		Sensors = new Array();
		var areaitem = $('#RegionTree1').jqxTree("getSelectedItem");
		var areaid = -1;
		if (areaitem != null) {
			areaid = areaitem.value;
		}

		$.ajax(
			{
				type: "POST",
				contentType: "application/json",
				url: "WebMethods.aspx/getSensorsByAreaIDIncludeSubAreas",
				data: "{'areaid':'" + areaid + "'}",
				dataType: "json",
				success: function (msg) {

					var ret = $.parseJSON(msg.d);
					if (ret == null || ret.data == null)
						return;
					for (var i = 0; i < ret.data.length; i++) {
						var sensor = new Sensor(ret.data[i].PipeID, ret.data[i].SencondHead, ret.data[i].SensorID, ret.data[i].SensorName);
						var pipe = GetPipeByID(ret.data[i].PipeID);
						if (pipe == null)
							continue;
						if (ret.data[i].SencondHead == "1") {
							sensor.location = pipe.endPos;
						}
						else {
							sensor.location = pipe.startPos;
						}
						Sensors.push(sensor);
						DrawSensor(sensor);
					}
				}
			}
			);


	}

	function CursorInSensor(pos) {

		for (var i = 0; i < Sensors.length; i++) {

			var startPixel = map.pointToPixel(Sensors[i].location);
			var startX = startPixel.x;
			var startY = startPixel.y;

			if (pos.x > startX - 6 && pos.x < startX + 6 && pos.y > startY - 6 && pos.y < startY + 6) {
				return Sensors[i];
			}
		}

		return null;

	}
	var waterSource = null;
	function BindPreWaters(value) {
		var areaitem = $('#RegionTree1').jqxTree("getSelectedItem");
		var areaid = -1;
		if (areaitem != null) {
			areaid = areaitem.value;
		}
		else {

			return;
		}
		$.ajax(
			{
				type: "POST",
				contentType: "application/json",
				url: "WebMethods.aspx/GetWatermeters",
				data: "{'areaid':'" + areaid + "'}",
				dataType: "json",
				success: function (msg) {

					var ret = $.parseJSON(msg.d);
					if (ret.msg == "ok") {
						waterSource = ret.data;

						$("#cmbPreWater").jqxComboBox({ selectedIndex: -1, width: 120, valueMember: "WaterMeterID", dropDownWidth: '250', displayMember: "WaterMeterName", source: ret.data, theme: skin, height: 20 });
						if (value == 0) {
							$("#cmbPreWater").jqxComboBox({ selectedIndex: -1 });
						}
						else {
							$("#cmbPreWater").jqxComboBox('val', value);
						}
					}

				}
			});


	}


	function ShowWaterReading() {

		var reading = parseInt(Math.random() * 220);
		var watermeter = watermeterContextMenu.watermeter;

		// window.open("WatermeterData.aspx?watermeter=" + watermeter.id);
		var labels = { visible: true, position: 'inside' };
		$('#gauge').jqxGauge({
			ranges: [{ startValue: 0, endValue: 90, style: { fill: '#e2e2e2', stroke: '#e2e2e2' }, startDistance: '5%', endDistance: '5%', endWidth: 13, startWidth: 13 },
						 { startValue: 90, endValue: 140, style: { fill: '#f6de54', stroke: '#f6de54' }, startDistance: '5%', endDistance: '5%', endWidth: 13, startWidth: 13 },
						 { startValue: 140, endValue: 180, style: { fill: '#db5016', stroke: '#db5016' }, startDistance: '5%', endDistance: '5%', endWidth: 13, startWidth: 13 },
						 { startValue: 180, endValue: 220, style: { fill: '#d02841', stroke: '#d02841' }, startDistance: '5%', endDistance: '5%', endWidth: 13, startWidth: 13 }
				],
			cap: { radius: 0.04 },
			caption: { offset: [0, -25], value: reading + "吨", position: 'bottom' },
			value: reading,
			style: { stroke: '#ffffff', 'stroke-width': '1px', fill: '#ffffff' },
			animationDuration: 1500,
			colorScheme: 'scheme05',
			labels: labels,
			ticksMinor: { interval: 5, size: '5%' },
			ticksMajor: { interval: 10, size: '10%' }
		});
		$("#GaugeWindow").jqxWindow("open");
		$("#waterLink").removeAttr("href");

		$("#waterLink").css("cursor","pointer");

		$("#waterLink").click(function(){
			 var index = $('#leftTabs').jqxTabs('length');

			var c = '<div  id="tabContent' + index + '"><iframe style="width:1920px; height:1080px" src="WatermeterData.aspx?watermeter=' + watermeter.id + '"  scrolling="no" frameborder="0"> </iframe> </div>';

			$('#leftTabs').jqxTabs('addAt', index, "水表历史数据查询", c);
		 });

		 
	}




	function ApplyPrivilete() {


		$.ajax(
					{
						type: "POST",
						contentType: "application/json",
						url: "WebMethods.aspx/getCurrentPermission",
						data: "{}",
						dataType: "json",
						success: function (msg) {


							var ret = $.parseJSON(msg.d);
							var result = ret.data[0].Result;

							if (result == "admin") {
								return;
							}

							if (result.indexOf("新增水表") < 0) {
								//    $("#miAddWatermeter").attr("disabled", "disabled");

								$("#miAddWatermeter").css("color", "lightgray");
								$("#miAddWatermeter").removeAttr("onclick");

							}
							if (result.indexOf("删除水表") < 0) {
								//  $("#miDeleteWatermeter").attr("disabled", "disabled");



								$("#miDeleteWatermeter").css("color", "lightgray");
								$("#miDeleteWatermeter").removeAttr("onclick");
							}
							if (result.indexOf("修改水表") < 0) {
								//   $("#miEditWaterInfo").attr("disabled", "disabled");

								$("#miEditWaterInfo").css("color", "lightgray");
								$("#miEditWaterInfo").removeAttr("onclick");
							}

							if (result.indexOf("新增传感器") < 0) {
								//  $("#miAddSensor").attr("disabled", "disabled");

								$("#miAddSensor").css("color", "lightgray");
								$("#miAddSensor").removeAttr("onclick");
							}

							if (result.indexOf("修改传感器") < 0) {
								//  $("#miEditSensorInfo").attr("disabled", "disabled");

								$("#miEditSensorInfo").css("color", "lightgray");
								$("#miEditSensorInfo").removeAttr("onclick");

							}

							if (result.indexOf("删除传感器") < 0) {
								//  $("#miDeleteSensor").attr("disabled", "disabled");

								$("#miDeleteSensor").css("color", "lightgray");
								$("#miDeleteSensor").removeAttr("onclick");
							}

							if (result.indexOf("新增管线") < 0) {
								//                                $("#miAddPipe").attr("disabled", "disabled");
								//                                $("#miNextPipe").attr("disabled", "disabled");

								$("#miAddPipe").css("color", "lightgray");
								$("#miAddPipe").removeAttr("onclick");

								$("#miNextPipe").css("color", "lightgray");
								$("#miNextPipe").removeAttr("onclick");
							}

							if (result.indexOf("微调经纬度") < 0) {
								//  $("#miAdjustPos").attr("disabled", "disabled");

								$("#miAdjustPos").css("color", "lightgray");
								$("#miAdjustPos").removeAttr("onclick");
							}

							if (result.indexOf("修改管线信息") < 0) {
								//   $("#miEditPipeInfo").attr("disabled", "disabled");

								$("#miEditPipeInfo").css("color", "lightgray");
								$("#miEditPipeInfo").removeAttr("onclick");
							}
							if (result.indexOf("编辑管线布局") < 0) {
								// $("#miEditPipeLayout").attr("disabled", "disabled");

								$("#miEditPipeLayout").css("color", "lightgray");
								$("#miEditPipeLayout").removeAttr("onclick");
							}

							if (result.indexOf("删除管线") < 0) {
								//  $("#miDeletePipe").attr("disabled", "disabled");


								$("#miDeletePipe").css("color", "lightgray");
								$("#miDeletePipe").removeAttr("onclick");
							}

							if (result.indexOf("新增管材") < 0) {
								$("#btnAddMaterial").attr("disabled", "disabled");
							}

							if (result.indexOf("修改管材") < 0) {
								$("#btnEditMaterial").attr("disabled", "disabled");
							}
							if (result.indexOf("删除管材") < 0) {
								$("#btnDelMaterial").attr("disabled", "disabled");
							}

							if (result.indexOf("新增区域") < 0) {


								$("#miAddRegion").css("color", "lightgray");
								$("#miAddRegion").removeAttr("onclick");
							}
							if (result.indexOf("删除区域") < 0) {
								$("#btnDelArea").attr("disabled", "disabled");
							}

							if (result.indexOf("修改区域") < 0) {
								$("#btnEditArea").attr("disabled", "disabled");
							}
							if (result.indexOf("水平衡报表查询") < 0) {
								// $("#miWaterbanlanceReport").attr("disabled", "disabled");



								$("#miWaterbanlanceReport").css("color", "lightgray");
								$("#miWaterbanlanceReport").removeAttr("onclick");
							}

							if (result.indexOf("传感器历史数据分析查询") < 0) {
								//    $("#miSensorHistoricReport").attr("disabled", "disabled");

								$("#miSensorHistoricReport").css("color", "lightgray");
								$("#miSensorHistoricReport").removeAttr("onclick");


							}

							if (result.indexOf("水表历史数据查询") < 0) {
								//   $("#miWaterHisroricData").attr("disabled", "disabled");

								$("#miWaterHisroricData").css("color", "lightgray");
								$("#miWaterHisroricData").removeAttr("onclick");
							}

							//$("#btnAddUser").attr("disabled", "disabled");
							// $("#btnEditUser").attr("disabled", "disabled");
							//                            $("#btnDelUser").attr("disabled", "disabled");
							//                            $('#TopMenu').jqxMenu('disable', 'miUserMaintain', true);
							//                            $("#miUserMaintain").removeAttr("onclick");

							$("#miUserMaintain").css("color", "lightgray");
							$("#miUserMaintain").removeAttr("onclick");

							$("#miUserGroupMaintain").css("color", "lightgray");
							$("#miUserGroupMaintain").removeAttr("onclick");


						}
					});
	}



	function LoadAllPipes() {
		map.clearOverlays();
		var areaitem = $('#RegionTree1').jqxTree("getSelectedItem");
		var areaid = -1;
		if (areaitem != null) {
			areaid = areaitem.value;
		}
		Pipes = new Array();
		Sensors = new Array();
		Watermeters = new Array();
		Devices = new Array();
		RedrawPipes();
		$.ajax(
					{
						type: "POST",
						contentType: "application/json",
						url: "WebMethods.aspx/getPipeByAreaIDIncludeSubAreas",
						data: "{'areaid':'" + areaid + "'}",
						dataType: "json",
						success: function (msg) {


							var ret = $.parseJSON(msg.d);

							if (ret.msg == "ok") {
								for (var i = 0; i < ret.data.length; i++) {

									var startPosStr = ret.data[i].StartLocation.split(",");
									var endPosStr = ret.data[i].EndLocation.split(",");

									var StartPos = new BMap.Point(parseFloat(startPosStr[0]), parseFloat(startPosStr[1]));
									var EndPos = new BMap.Point(parseFloat(endPosStr[0]), parseFloat(endPosStr[1]));

									var pipe = new Pipe(StartPos, EndPos, ret.data[i].PipeID);
									pipe.PrePipe = ret.data[i].PrePipeID;


									Pipes.push(pipe);
									pipe.status = parseInt(ret.data[i].Status);
								}
							}
							RedrawPipes();
							LoadAllSensor();
							LoadAllWatermeters();
							LoadAllDevices();
						}
					});

	}


	var CurrentLocation = null;
	$(document).ready(function () {

		InitMap();
		if ($("#hideValue").attr("data-skin") != undefined && $("#hideValue").attr("data-skin") != null && $("#hideValue").attr("data-skin") != "") {
			skin = $("#hideValue").attr("data-skin");
		}
		$("#pageBody").jqxSplitter({ width: 1920, height: 1080, panels: [{ size: 300}], theme: skin });
		$("#sideBar").jqxNavigationBar({ width: '100%', theme: skin, expandMode: 'multiple', expandedIndexes: [0, 1, 2] });
		$("#sideBar").show();

		SensorImage = new Image();

		//            SensorImage.onload = function () {
		//                context.drawImage(imageObj, 69, 50);
		//            };
		SensorImage.src = 'Images/sensor22.png';

		WatermeterImage = new Image();

		WatermeterImage.src = 'Images/watermeter22.png';

		ValveImage = new Image();
		ValveImage.src = 'Images/valve1.png';

		FirePlugImage = new Image();
		FirePlugImage.src = 'Images/hydrant.png';

		WellImage = new Image();
		WellImage.src = 'Images/well2.png';
		MarkImage = new Image();
		MarkImage.src = 'Images/mark.png';

		Pipes = new Array();
		Sensors = new Array();
		Watermeters = new Array();
		Devices = new Array();
		//  $("#TopMenu").jqxMenu({ width: '650', height: '30px', theme: skin, autoOpen: false, autoCloseOnMouseLeave: false, showTopLevelArrows: true });

		$("#GroupMenu").jqxMenu({ width: '650', height: '30px', theme: skin, autoOpen: false, autoCloseOnMouseLeave: false, showTopLevelArrows: true });

		contextMenu = $("#ContextMenu").jqxMenu({ width: '200', height: 'auto', theme: skin, autoOpenPopup: false, mode: 'popup' });
		pipeContextMenu = $("#PipeEditContextMenu").jqxMenu({ width: '200', height: 'auto', theme: skin, autoOpenPopup: false, mode: 'popup' });
		sensorContextMenu = $("#SensorContextMenu").jqxMenu({ width: '200', height: 'auto', theme: skin, autoOpenPopup: false, mode: 'popup' });
		watermeterContextMenu = $("#WatermeterContextMenu").jqxMenu({ width: '200', height: 'auto', theme: skin, autoOpenPopup: false, mode: 'popup' });
		deviceContextMenu = $("#DeviceContextMenu").jqxMenu({ width: '200', height: 'auto', theme: skin, autoOpenPopup: false, mode: 'popup' });
		map.addEventListener("rightclick", function (e) {

			var left = $("#allmap").offset().left;
			var top = $("#allmap").offset().top;
			CurrentLocation = e.point;
			var pixel = map.pointToPixel(e.point);
			contextMenu.jqxMenu('open', parseInt(pixel.x) + 5 + left, parseInt(pixel.y) + 5 + top);

			CurrentPos = e.point.lng + "," + e.point.lat;
			return false;
		});


		function WatermeterHitTest(pixel, watermeter) {
			if (pixel.x > watermeter.x - 12 && pixel.x < watermeter.x + 12 && pixel.y > watermeter.y - 12 && pixel.y < watermeter.y + 12) {
				return true;
			}

			return false;
		}

		function DeviceHitTest(pixel, device) {
			var p = map.pointToPixel(device.Location);
			if (pixel.x > p.x - 12 && pixel.x < p.x + 12 && pixel.y > p.y - 12 && pixel.y < p.y + 12) {
				return true;
			}

			return false;
		}

		map.addEventListener("click", function (e) {

			CurrentLocation = e.point;
			var pixel = map.pointToPixel(e.point);
			var ret = 0;

			for (var k = 0; k < Devices.length; k++) {
				if (DeviceHitTest(pixel, Devices[k])) {
					ShowDeviceMenu(Devices[k], pixel);

					return;
				}

			}

			for (var j = 0; j < Watermeters.length; j++) {
				if (WatermeterHitTest(pixel, Watermeters[j])) {
					ShowWatermeterMenu(Watermeters[j], pixel);

					return;
				}
			}

			for (var i = 0; i < Pipes.length; i++) {

				ret = CusorInPipeHead(pixel, Pipes[i]);

				if (ret == 1 || ret == 2) {


					ShowSensorMenu(Pipes[i], pixel, ret, CursorInSensor(pixel));

					//  ShowSensorMenu(Pipes[i], pixel, ret, null);
					return;
				}

				else if (PipeHitTest(pixel.x, pixel.y, Pipes[i])) {


					ShowPipeContextMenu(Pipes[i], pixel);

					return;
				}
			}


			return false;
		});

		// PipeHitTest
		map.addEventListener("dblclick", function (e) {

			CurrentLocation = e.point;
			AddNextPipe();

			return false;
		});

		map.addEventListener("moving", function (e) {

			RedrawPipes();

			return false;
		});

		map.addEventListener("dragging", function (e) {

			RedrawPipes();

			return false;
		});
		map.addEventListener("zoomstart", function (e) {

			$("#OverlayCanvas").fadeOut(500);

			return false;
		});
		map.addEventListener("zoomend", function (e) {

			RedrawPipes();
			$("#OverlayCanvas").fadeIn(100);
			return false;
		});




		map.disableDoubleClickZoom();



		$("#RegionButton1").jqxDropDownButton({ width: '75%', theme: skin });
		$("#RegionTree1").jqxTree({ width: '75%', height: 220, theme: skin });
		$('#RegionWindow').jqxWindow({ height: 200, width: 270, resizable: false, isModal: true, modalOpacity: 0.3, theme: skin, autoOpen: false });

		$('#PipeWindow').jqxWindow({
			height: 220, width: 670,
			resizable: false, isModal: false, modalOpacity: 0.3, theme: skin, autoOpen: false
		});

		$('#SensorWindow').jqxWindow({
			height: 180, width: 690,
			resizable: false, isModal: false, modalOpacity: 0.3, theme: skin, autoOpen: false
		});
		$('#DeviceWindow').jqxWindow({
			height: 180, width: 520,
			resizable: false, isModal: false, modalOpacity: 0.3, theme: skin, autoOpen: false
		});

		$('#GaugeWindow').jqxWindow({
			height: 420, width: 380,
			resizable: false, isModal: false, modalOpacity: 0.3, theme: skin, autoOpen: false
		});

		$('#MaterialWindow').jqxWindow({
			height: 500, width: 670,
			resizable: false, isModal: false, modalOpacity: 0.3, theme: skin, autoOpen: false
		});

		$('#UserWindow').jqxWindow({
			height: 500, width: 800,
			resizable: false, isModal: false, modalOpacity: 0.3, theme: skin, autoOpen: false
		});

		//            $('#TooltipWindow').jqxWindow({
		//                height: 120, width: 200,
		//                resizable: false, isModal: false, modalOpacity: 0.3, theme: 'energyblue', autoOpen: false
		//            });

		$('#AreaWindow').jqxWindow({
			height: 500, width: 800,
			resizable: false, isModal: false, modalOpacity: 0.3, theme: skin, autoOpen: false
		});

		$('#GroupWindow').jqxWindow({
			height: 600, width: 800,
			resizable: false, isModal: false, modalOpacity: 0.3, theme: skin, autoOpen: false
		});

		$('#AddMaterialWindow').jqxWindow({
			height: 180, width: 400, zIndex: 10000,
			resizable: false, isModal: true, modalOpacity: 0.3, theme: skin, autoOpen: false
		});

		$('#AddUserWindow').jqxWindow({
			height: 220, width: 530, zIndex: 10000,
			resizable: false, isModal: true, modalOpacity: 0.3, theme: skin, autoOpen: false
		});
		$('#EditAreaWindow').jqxWindow({
			height: 220, width: 400, zIndex: 10000,
			resizable: false, isModal: true, modalOpacity: 0.3, theme: skin, autoOpen: false
		});

		$('#EditMaterialWindow').jqxWindow({
			height: 180, width: 400, zIndex: 10000,
			resizable: false, isModal: true, modalOpacity: 0.3, theme: skin, autoOpen: false
		});

		$('#WatermeterWindow').jqxWindow({
			height: 180, width: 670,
			resizable: false, isModal: false, modalOpacity: 0.3, theme: skin, autoOpen: false
		});

		$('#AdjustPosWindow').jqxWindow({
			height: 150, width: 280,
			resizable: false, isModal: false, modalOpacity: 0.3, theme: skin, autoOpen: false
		});

		$('#AddGroupWindow').jqxWindow({
			height: 160, width: 280,
			resizable: false, isModal: true, modalOpacity: 0.3, theme: skin, autoOpen: false
		});

		$('#ChangePwdWindow').jqxWindow({
			height: 180, width: 280,
			resizable: false, isModal: true, modalOpacity: 0.3, theme: skin, autoOpen: false
		});

		$("#txtRegionName").jqxInput({ width: 160, minLength: 1, theme: skin });
		$("#txtRegionPos").jqxInput({ width: 160, minLength: 1, theme: skin });
		$("#btnSaveRegion").jqxButton({ width: '100', theme: skin });
		$("#btnAddMaterial").jqxButton({ width: '100', theme: skin });
		$("#btnEditMaterial").jqxButton({ width: '100', theme: skin });
		$("#btnDelMaterial").jqxButton({ width: '100', theme: skin });
		$("#btnAddUser").jqxButton({ width: '100', theme: skin });
		$("#btnEditUser").jqxButton({ width: '100', theme: skin });
		$("#btnDelUser").jqxButton({ width: '100', theme: skin });
		$("#btnEditArea").jqxButton({ width: '100', theme: skin });
		$("#btnDelArea").jqxButton({ width: '100', theme: skin });
		$("#btnSelectRegion").jqxDropDownButton({ width: 160, theme: skin });
		$('#RegionTreeList').jqxTree({ height: '200px', width: '330px', theme: skin });
		$("#btnPipeArea").jqxDropDownButton({ width: 120, theme: skin });
		$('#trPipeArea').jqxTree({ height: '200px', width: '330px', theme: skin });
		$("#btnSensorArea").jqxDropDownButton({ width: 120, theme: skin });
		$("#btnDeviceArea").jqxDropDownButton({ width: 120, theme: skin });
		$('#trSensorArea').jqxTree({ height: '200px', width: '330px', theme: skin });
		$('#trDeviceArea').jqxTree({ height: '200px', width: '330px', theme: skin });

		$('#RegionTreeList').on('select', function (event) {
			var args = event.args;
			var item = $('#RegionTreeList').jqxTree('getItem', args.element);
			var dropDownContent = '<div style="position: relative; margin-left: 3px; margin-top: 2px;">' + item.label + '</div>';
			$("#btnSelectRegion").jqxDropDownButton('setContent', dropDownContent);
		});

		$('#trAreaSet').jqxTree({ theme: skin });
		$("input[type='text']").jqxInput({ theme: skin });
		$("input[type='password']").jqxInput({ theme: skin });
		$("#menuSysSetting").jqxMenu({ width: 'auto', theme: skin, mode: 'vertical' });
		$("#menuReportManage").jqxMenu({ width: 'auto', theme: skin, mode: 'vertical' });
		$("#menuUserManage").jqxMenu({ width: 'auto', theme: skin, mode: 'vertical' });
		$("#head").jqxPanel({ width: '100%', height: 90, theme: skin });
		$("#RegionButton1").jqxDropDownButton({ width: 160, theme: skin });
		$('#RegionTree1').jqxTree({ height: '200px', width: '330px', theme: skin });
		$("#btnUserArea1").jqxDropDownButton({ width: 160, theme: skin });
		$('#trUserArea1').jqxTree({ height: '200px', width: '330px', theme: skin });
		$("input[type='button']").jqxButton({ theme: skin });
		$("#btnWatermeterArea").jqxDropDownButton({ width: 120, theme: skin });
		$('#trWatermeterArea').jqxTree({ height: '200px', width: '330px', theme: skin });

		$('#RegionTree1').on('select', function (event) {
			var args = event.args;
			var item = $('#RegionTree1').jqxTree('getItem', args.element);
			var dropDownContent = '<div style="position: relative; margin-left: 3px; margin-top: 2px;">' + item.label + '</div>';
			$("#RegionButton1").jqxDropDownButton('setContent', dropDownContent);
			var LocationStr = GetAreaLocationByID(item.value);
			var point = LocationStr.split(",");
			map.centerAndZoom(new BMap.Point(point[0], point[1]
), 19);
			LoadAllPipes();

		});

		$('#trPipeArea').on('select', function (event) {
			var args = event.args;
			var item = $('#trPipeArea').jqxTree('getItem', args.element);
			var dropDownContent = '<div style="position: relative; margin-left: 3px; margin-top: 2px;">' + item.label + '</div>';
			$("#btnPipeArea").jqxDropDownButton('setContent', dropDownContent);

		});

		$('#trSensorArea').on('select', function (event) {
			var args = event.args;
			var item = $('#trSensorArea').jqxTree('getItem', args.element);
			var dropDownContent = '<div style="position: relative; margin-left: 3px; margin-top: 2px;">' + item.label + '</div>';
			$("#btnSensorArea").jqxDropDownButton('setContent', dropDownContent);

		});

		$('#trDeviceArea').on('select', function (event) {
			var args = event.args;
			var item = $('#trDeviceArea').jqxTree('getItem', args.element);
			var dropDownContent = '<div style="position: relative; margin-left: 3px; margin-top: 2px;">' + item.label + '</div>';
			$("#btnDeviceArea").jqxDropDownButton('setContent', dropDownContent);

		});
		$('#trUserArea1').on('select', function (event) {
			var args = event.args;
			var item = $('#trUserArea1').jqxTree('getItem', args.element);
			var dropDownContent = '<div style="position: relative; margin-left: 3px; margin-top: 2px;">' + item.label + '</div>';
			$("#btnUserArea1").jqxDropDownButton('setContent', dropDownContent);

		});

		$('#trWatermeterArea').on('select', function (event) {
			var args = event.args;
			var item = $('#trWatermeterArea').jqxTree('getItem', args.element);
			var dropDownContent = '<div style="position: relative; margin-left: 3px; margin-top: 2px;">' + item.label + '</div>';
			$("#btnWatermeterArea").jqxDropDownButton('setContent', dropDownContent);

		});


		$('#leftTabs').jqxTabs({  showCloseButtons: true ,theme:'light'});
		$('#leftTabs').jqxTabs('hideCloseButtonAt', 0); 
		$("#txtPipeName").jqxInput({ width: 120, minLength: 1, theme: skin });
		$("#txtPipeNo").jqxInput({ width: 120, minLength: 1, theme: skin });
		//$("#txtPipeNo").jqxNumberInput({ width: '120px', height: '20px', inputMode: 'simple', spinButtons: false, theme: skin });
		$("#txtPipeWidth").jqxNumberInput({ width: '120px', inputMode: 'simple', spinButtons: false, theme: skin });
		$("#txtPipeLength").jqxNumberInput({ width: '120px', inputMode: 'simple', spinButtons: false, theme: skin });
		$("#txtPipeDepth").jqxNumberInput({ width: '120px', inputMode: 'simple', spinButtons: false, theme: skin });
		$("#txtPipeRemark").jqxInput({ width: 120, minLength: 1, theme: skin });
		$("#txtSensorName").jqxInput({ width: 120, minLength: 1, theme: skin });
		$("#txtSensorNo").jqxInput({ width: 120, minLength: 1, theme: skin });
		$("#txtSensorRemark").jqxInput({ width: 120, minLength: 1, theme: skin });
		$("#txtSensorType").jqxInput({ width: 120, minLength: 1, theme: skin });
		$("#txtWatermeterName").jqxInput({ width: 120, minLength: 1, theme: skin });
		$("#txtWatermeterNo").jqxInput({ width: 120, minLength: 1, theme: skin });
		$("#txtWatermeterRemark").jqxInput({ width: 120, minLength: 1, theme: skin });
		$("#txtWatermeterType").jqxInput({ width: 120, minLength: 1, theme: skin });
		$("#txtNewPos").jqxInput({ width: 190, minLength: 1, theme: skin });
		var srcWaterLevel = [1, 2, 3, 4, 5, 6, 7, 8, 9];
		$("#cmbWaterLevel").jqxComboBox({ selectedIndex: -1, width: 120, dropDownWidth: '250', source: srcWaterLevel, theme: skin });

		var skinSource = ["glacier", "mobile", "android", "windowsphone", "ui-start", "ui-sunny", "ui-redmond", "ui-le-frog", "ui-smoothness", "arctic", "ui-overcast", "web", "bootstrap", "metro", "metrodark", "office", "fresh", "energyblue", "darkblue", "black", "shinyblack"];
		$("#cmbSkinList").jqxComboBox({ selectedIndex: -1, width: 120, dropDownWidth: '250', source: skinSource, theme: skin });
		$("#cmbSkinList").jqxComboBox('val', skin);

		$('#cmbSkinList').on('select', function (event) {
			var args = event.args;
			if (args) {
				// index represents the item's index.                       
				var index = args.index;
				var item = args.item;
				var value = item.value;
				window.location.href = "default.aspx?skin=" + item.value;
			}
		});

		ApplyPrivilete();
		BindRegion("RegionTree1", $("#hideValue").attr("data-defaultarea"));
		BindRegion("trUserArea1");
		BindRegion("RegionTreeList");
		BindRegion("trPipeArea");
		BindRegion("trSensorArea");
		BindRegion("trDeviceArea");
		BindRegion("trWatermeterArea");
		BindMaterial("cmbMaterial");
		BindUsers("cmbAdmins");
		BindUsers("cmbSensorAdmin");
		BindUsers("cmbWatermeterAdmin");
		LoadAllPipes();

		$("#allmap").mousemove(function (e) {
			var pos = getMousePos(e);
			//                var pixel = new BMap.Pixel(pos.x, pos.y);

			var left = $("#allmap").offset().left;
			var top = $("#allmap").offset().top;

			for (var i = 0; i < Pipes.length; i++) {

				if (Pipes[i].status == 0 && PipeHitTest(pos.x, pos.y, Pipes[i])) {

					$("#TooltipWindow").show();
					$("#TooltipWindow").css("left", pos.x + 15 + left + "px");
					$("#TooltipWindow").css("top", pos.y - 50 + top + "px");
					//  $('#TooltipWindow').jqxWindow({ position: { x: e.pageX + 15, y: e.pageY - 50} });
					RefreshTooltip(Pipes[i].id);
					return;
				}
				else {
					$("#TooltipWindow").hide();
				}
			}

		});
	});

</script>
