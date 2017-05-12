function BindRegion(TreeID,defaultValue) {

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
                    $('#' + TreeID).jqxTree({ source: records });
                    if (defaultValue != undefined && defaultValue != null) {
                        SelectArea(TreeID, defaultValue);

                    }

                    if (TreeID == "trAreaSet") {


                        $('#trAreaSet .jqx-tree-item').dblclick(function () {

                            $("#trAreaSet").jqxTree('selectItem', this);
                            var item = $("#trAreaSet").jqxTree('getSelectedItem');

                            ShowEditAreaWindow();
                        });
                    }

                }
            });


        }




        function SaveRegion() {

           

            var RegionName = $("#txtRegionName").val().trim();

            if (RegionName == "") {
                alert("区域名不能为空！");
                return;
            }
            var ParentID = -1;
            var RegionItem = $("#RegionTreeList").jqxTree('getSelectedItem');
            if ( RegionItem!= null) {
               
               ParentID=RegionItem.value;
            }

          

            var location=$("#txtRegionPos").val();

            $.ajax(
            {
                type: "POST",
                contentType: "application/json",
                url: "WebMethods.aspx/addArea",
                data: "{'areaname':'" + RegionName + "','parentareaid':'" + ParentID + "','location':'" + location + "'}",
                dataType: "json",
                success: function (msg) {

                    var result = $.parseJSON(msg.d);
                
                    if (result.msg == "ok") {
                        BindRegion("RegionTree1");
                        BindRegion("RegionTreeList");
                    }

                }
            })

        }


       