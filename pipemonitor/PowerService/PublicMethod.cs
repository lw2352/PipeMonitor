using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
//using System.Linq;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.Script.Serialization;
using Newtonsoft.Json;
using ThoughtWorks.QRCode.Codec;

/// <summary>
/////PublicMethod 的摘要说明
///// </summary>
/////
///// 
    class IPAdderssData
	    {
	        public string country;
	        public string country_id;
	        public string area;
	        public string area_id;
	        public string region;
	        public string region_id;
	        public string city;
	        public string city_id;
	        public string county;
	        public string county_id;
	        public string isp;
	        public string isp_id;
	        public string ip;
	    }
 class IPCheckResult
	    {
	        public int code;
	        public IPAdderssData data;
	    }
public class PublicMethod
{
   // public static OperationTxT oper = new OperationTxT(@"D:\\ShopServiceLog.txt");
    public static string ERRORCODE = "-9";//记录异常的代码

    //活动图片存放路径
    public static string g_strActivityRootFoldName = "D://WebAppResources//Aileguang//Photos//Activity//";
    //商品图片存放路径
    public static string g_strGoodsRootFoldName = "D://WebAppResources//Aileguang//Photos//Shop//Goods//";
    //二维码存放路径
    public static string g_strShopQRCodeRootFoldName = "D://WebAppResources//Aileguang//Photos//Shop//ShopQRCode//";
        //用户二维码存放路径
    public static string g_strPhoneUserQRCodeRootFoldName = "D://WebAppResources//Exhibit//Photos//PhoneUserQRCode//";
    //微信店铺详情前缀
    public static string g_strWeiXinShopDetailPrefix = "http://dx.aileguang.com/ShopWebService/weixin/shop.html?shop_id=";
    //手机用户二维码前缀
    public static string g_strPhoneUserDetailQRCodePrefix = "huihui://user?phoneuserid=";

    public PublicMethod()
    {
        //
        //TODO: 在此处添加构造函数逻辑
        //
    }
    /// <summary>
    /// 
    /// </summary>
    /// 参考如下的调用
    /// /// UnZip(@"E:\C#TEST\myzip\myzip\myzip\bin\Debug\mydat.zip", @"E:\C#TEST\myzip\myzip\myzip\bin\Debug");
    /// <param name="zipFile"></param>
    /// <param name="destFolder"></param>
  
    public static bool StringToFile(string base64String, string fileName)
    {
        //string path = Path.GetDirectoryName(Assembly.GetExecutingAssembly().GetName().CodeBase) + @"/beapp/" + fileName; 
        System.IO.FileStream fs = new System.IO.FileStream(fileName, System.IO.FileMode.Create);
        System.IO.BinaryWriter bw = new System.IO.BinaryWriter(fs);
        if (!string.IsNullOrEmpty(base64String) && File.Exists(fileName))
        {
            bw.Write(Convert.FromBase64String(base64String));
        }
        bw.Close();
        fs.Close();
        return true;
    }



    /// <summary>  
    /// 创建二维码  
    /// </summary>  
    /// <param name="QRString">二维码字符串</param>    
    /// <param name="filePath">保存路径</param>  
    /// <param name="size">保存图片大小</param>  
    /// <param name="hasLogo">是否有logo(logo尺寸50x50，QRCodeScale>=5，QRCodeErrorCorrect为H级)</param>  
    /// <param name="logoFilePath">logo路径</param>  
    /// <returns></returns>  
    public static  bool CreateQRCode(string QRString, string filePath,
        int size, bool hasLogo, string logoFilePath)
    {
        string QRCodeEncodeMode = "Byte"; //二维码编码(Byte、AlphaNumeric、Numeric)
        string QRCodeErrorCorrect = "H";  //二维码纠错能力(L：7% M：15% Q：25% H：30%)
        int QRCodeScale = 6;              //二维码尺寸(Version为0时，1：26x26，每加1宽和高各加25  
        int QRCodeVersion = 0;           //二维码密集度0-40

        bool result = true;
        QRCodeEncoder qrCodeEncoder = new QRCodeEncoder();

        switch (QRCodeEncodeMode)
        {
            case "Byte":
                qrCodeEncoder.QRCodeEncodeMode = QRCodeEncoder.ENCODE_MODE.BYTE;
                break;
            case "AlphaNumeric":
                qrCodeEncoder.QRCodeEncodeMode = QRCodeEncoder.ENCODE_MODE.ALPHA_NUMERIC;
                break;
            case "Numeric":
                qrCodeEncoder.QRCodeEncodeMode = QRCodeEncoder.ENCODE_MODE.NUMERIC;
                break;
            default:
                qrCodeEncoder.QRCodeEncodeMode = QRCodeEncoder.ENCODE_MODE.BYTE;
                break;
        }
        qrCodeEncoder.QRCodeScale = QRCodeScale;
        qrCodeEncoder.QRCodeVersion = QRCodeVersion;



        switch (QRCodeErrorCorrect)
        {
            case "L":
                qrCodeEncoder.QRCodeErrorCorrect = QRCodeEncoder.ERROR_CORRECTION.L;
                break;
            case "M":
                qrCodeEncoder.QRCodeErrorCorrect = QRCodeEncoder.ERROR_CORRECTION.M;
                break;
            case "Q":
                qrCodeEncoder.QRCodeErrorCorrect = QRCodeEncoder.ERROR_CORRECTION.Q;
                break;
            case "H":
                qrCodeEncoder.QRCodeErrorCorrect = QRCodeEncoder.ERROR_CORRECTION.H;
                break;
            default:
                qrCodeEncoder.QRCodeErrorCorrect = QRCodeEncoder.ERROR_CORRECTION.H;
                break;
        }

        try
        {
            Image image = qrCodeEncoder.Encode(QRString, System.Text.Encoding.UTF8);
            System.IO.FileStream fs = new System.IO.FileStream(filePath, System.IO.FileMode.OpenOrCreate, System.IO.FileAccess.Write);
            image.Save(fs, System.Drawing.Imaging.ImageFormat.Jpeg);
            fs.Close();

            if (hasLogo)
            {
                Image copyImage = System.Drawing.Image.FromFile(logoFilePath);
                Graphics g = Graphics.FromImage(image);
                int x = image.Width / 2 - copyImage.Width / 2;
                int y = image.Height / 2 - copyImage.Height / 2;
                g.DrawImage(copyImage, new Rectangle(x, y, copyImage.Width, copyImage.Height), 0, 0, copyImage.Width, copyImage.Height, GraphicsUnit.Pixel);
                g.Dispose();


                Bitmap changedBitmap = new Bitmap(size, size);
                Graphics graphics = Graphics.FromImage(changedBitmap);
                graphics.Clear(System.Drawing.Color.Transparent);
                graphics.DrawImage(image, new Rectangle(0, 0, size, size),
                            new Rectangle(0, 0, image.Width, image.Height),
                            GraphicsUnit.Pixel);
                changedBitmap.Save(filePath);
                graphics.Dispose();
                image.Dispose();

                //image.Save(filePath);
                copyImage.Dispose();
            }
            image.Dispose();

        }
        catch (Exception ex)
        {
            result = false;
        }
        return result;
    } 

    /// <summary> 
    /// 调用此函数后使此两种图片合并，类似相册，有个 
    /// 背景图，中间贴自己的目标图片 
    /// </summary> 
    /// <param name="imgBack">粘贴的源图片 
    /// <param name="destImg">粘贴的目标图片 
    public static System.Drawing.Image CombinImage(System.Drawing.Image imgBack, string destImg)
    {
        Image img = Image.FromFile(destImg);        //照片图片   
        if (img.Height != 65 || img.Width != 65)
        {
            img = KiResizeImage(img, 65, 65, 0);
        }
        Graphics g = Graphics.FromImage(imgBack);

        g.DrawImage(imgBack, 0, 0, imgBack.Width, imgBack.Height);      //g.DrawImage(imgBack, 0, 0, 相框宽, 相框高);  

        //g.FillRectangle(System.Drawing.Brushes.White, imgBack.Width / 2 - img.Width / 2 - 1, imgBack.Width / 2 - img.Width / 2 - 1,1,1);//相片四周刷一层黑色边框 

        //g.DrawImage(img, 照片与相框的左边距, 照片与相框的上边距, 照片宽, 照片高); 

        g.DrawImage(img, imgBack.Width / 2 - img.Width / 2, imgBack.Width / 2 - img.Width / 2, img.Width, img.Height);
        GC.Collect();
        return imgBack;
    }

    /// <summary> 
    /// Resize图片 
    /// </summary> 
    /// <param name="bmp">原始Bitmap 
    /// <param name="newW">新的宽度 
    /// <param name="newH">新的高度 
    /// <param name="Mode">保留着，暂时未用 
    /// <returns>处理以后的图片</returns> 
    public static Image KiResizeImage(Image bmp, int newW, int newH, int Mode)
    {
        try
        {
            Image b = new Bitmap(newW, newH);
            Graphics g = Graphics.FromImage(b);
            // 插值算法的质量 
            g.InterpolationMode = InterpolationMode.HighQualityBicubic;
            g.DrawImage(bmp, new Rectangle(0, 0, newW, newH), new Rectangle(0, 0, bmp.Width, bmp.Height), GraphicsUnit.Pixel);
            g.Dispose();
            return b;
        }
        catch
        {
            return null;
        }
    }

    public static void DeleteFile(string strFile)
    {
        try
        {
            if (File.Exists(strFile))
            {

                FileInfo fi = new FileInfo(strFile);

                if (fi.Attributes.ToString().IndexOf("ReadOnly") != -1)

                    fi.Attributes = FileAttributes.Normal;

                File.Delete(strFile);

            }
        }
        catch (Exception)
        {

            ;
        }

    }

    public static DataSet GetDataSetFromXML(string FileName)
    {
        DataSet ds = new DataSet();
        ds.ReadXml(FileName);//xml文件包含了完整路径
        return ds;
    }

    public static string getISPInfo(string IPAddress)
    {
                string strResult = "";
                WebClient client = new WebClient();
                string uri = @"http://ip.taobao.com/service/getIpInfo.php?ip=" + IPAddress;
                string jsonData = client.DownloadString(uri);
                IPCheckResult result = JsonConvert.DeserializeObject<IPCheckResult>(jsonData);
	            if (result.code != 0)
                {
                    return "";
                }
                strResult = result.data.isp_id;
        return strResult;
    } 

    //MD5加密，陈浩然  2014.2.12
    public static string GetMD5Hash(string encryptString)
    {
        byte[] result = Encoding.Default.GetBytes(encryptString);
        MD5 md5 = new MD5CryptoServiceProvider();
        byte[] output = md5.ComputeHash(result);
        string encryptResult = BitConverter.ToString(output).Replace("-", "");
        encryptResult = encryptResult.ToLower();
        return encryptResult;
    } 

    /// <summary>
    /// 图片转为Byte字节数组
    /// </summary>
    /// <param name="FilePath">路径</param>
    /// <returns>字节数组</returns>
    public static byte[] imageToByteArray(string FilePath)
    {
        using (MemoryStream ms = new MemoryStream())
        {

            using (Image imageIn = Image.FromFile(FilePath))
            {

                using (Bitmap bmp = new Bitmap(imageIn))
                {
                    bmp.Save(ms, imageIn.RawFormat);
                    bmp.Dispose();//my add in 2013.9.25 
                }

            }
            return ms.ToArray();
        }
    }


    //判断是否为中文
    private static  bool IsChineseChar(string input)
    {
        int code = 0;
        int chfrom = Convert.ToInt32("4e00", 16); //范围（0x4e00～0x9fff）转换成int（chfrom～chend）
        int chend = Convert.ToInt32("9fff", 16);

        code = Char.ConvertToUtf32(input, 0); //获得字符串input中指定索引index处字符unicode编码
        if ((code >= chfrom && code <= chend) ||
                   (code >= Convert.ToInt32("2000", 16) && code <= Convert.ToInt32("206f", 16)) ||//常用标点符号
                   (code >= Convert.ToInt32("4e00", 16) && code <= Convert.ToInt32("4e00", 16)) ||
                   (code >= Convert.ToInt32("3400", 16) && code <= Convert.ToInt32("4db5", 16)) ||
                   (code >= Convert.ToInt32("9fa6", 16) && code <= Convert.ToInt32("9fbb", 16)) ||
                   (code >= Convert.ToInt32("f900", 16) && code <= Convert.ToInt32("fa2d", 16)) ||
                   (code >= Convert.ToInt32("fa30", 16) && code <= Convert.ToInt32("fa6a", 16)) ||
                   (code >= Convert.ToInt32("fa70", 16) && code <= Convert.ToInt32("fad9", 16)) ||
                   (code >= Convert.ToInt32("20000", 16) && code <= Convert.ToInt32("2a6d6", 16)) ||
                   (code >= Convert.ToInt32("2f800", 16) && code <= Convert.ToInt32("2fa1d", 16)) ||
                   (code >= Convert.ToInt32("ff00", 16) && code <= Convert.ToInt32("ffef", 16)) ||
                   (code >= Convert.ToInt32("2e80", 16) && code <= Convert.ToInt32("2eff", 16)) ||
                   (code >= Convert.ToInt32("3000", 16) && code <= Convert.ToInt32("303f", 16)) ||
                   (code >= Convert.ToInt32("31c0", 16) && code <= Convert.ToInt32("31ef", 16)
                    ))


        {
            return true; //当code在中文范围内返回true
        }
        else
        {
            return false; //当code不在中文范围内返回false
        }
        return false;
    }

    //得到将中文转换为Unicode编码后的字符串
    public static string GetUnicodeString(string strValue)
    {
        string result = "";
        for (int i = 0; i < strValue.Length; i++)
        {
            string str = strValue[i].ToString();
            if (IsChineseChar(str))
            {
                byte[] bytes = ASCIIEncoding.Unicode.GetBytes(str);
                result += "\\u";
                for (int j = bytes.Length - 1; j >= 0; j--)
                {
                    string strChar = Convert.ToString(bytes[j], 16).PadLeft(2, '0');
                    result += strChar;
                }

            }
            else
            {
                result += str;
            }
        }
        return result;
    }


    public static string TableToJson(DataTable dt)
    {

        return  Newtonsoft.Json.JsonConvert.SerializeObject(dt);

    }



    ///     
    /// dataTable转换成Json格式    
    ///     
    ///     
    ///     
    public static  string DataTableToJson(DataTable dt)
    {
        StringBuilder jsonBuilder = new StringBuilder();
        jsonBuilder.Append("{\"");
        jsonBuilder.Append("data");
        jsonBuilder.Append("\":[");
        for (int i = 0; i < dt.Rows.Count; i++)
        {
            jsonBuilder.Append("{");
            for (int j = 0; j < dt.Columns.Count; j++)
            {
                jsonBuilder.Append("\"");
                jsonBuilder.Append(dt.Columns[j].ColumnName);
                jsonBuilder.Append("\":\"");
                jsonBuilder.Append(GetUnicodeString(dt.Rows[i][j].ToString()));
                jsonBuilder.Append("\",");
            }
            jsonBuilder.Remove(jsonBuilder.Length - 1, 1);
            jsonBuilder.Append("},");
        }
        jsonBuilder.Remove(jsonBuilder.Length - 1, 1);
        jsonBuilder.Append("],");
        jsonBuilder.Append("\"");
        jsonBuilder.Append("ret");
        jsonBuilder.Append("\":");
        jsonBuilder.Append("\"0" + "\",");
        jsonBuilder.Append("\"msg\":\"ok" + "\"");

        jsonBuilder.Append("}");
        return jsonBuilder.ToString();
    }

    public static string DataTableJsonForWeiXin(DataTable dt)
    {
        StringBuilder jsonBuilder = new StringBuilder();
        jsonBuilder.Append("{\"");
        jsonBuilder.Append("data");
        jsonBuilder.Append("\":[");
        for (int i = 0; i < dt.Rows.Count; i++)
        {
            jsonBuilder.Append("{");
            for (int j = 0; j < dt.Columns.Count; j++)
            {
                jsonBuilder.Append("\"");
                jsonBuilder.Append(dt.Columns[j].ColumnName);
                jsonBuilder.Append("\":\"");
                jsonBuilder.Append(dt.Rows[i][j].ToString());
                jsonBuilder.Append("\",");
            }
            jsonBuilder.Remove(jsonBuilder.Length - 1, 1);
            jsonBuilder.Append("},");
        }
        jsonBuilder.Remove(jsonBuilder.Length - 1, 1);
        jsonBuilder.Append("],");
        jsonBuilder.Append("\"");
        jsonBuilder.Append("ret");
        jsonBuilder.Append("\":");
        jsonBuilder.Append("\"0"  + "\",");
        jsonBuilder.Append("\"msg\":\"ok" + "\"");

        jsonBuilder.Append("}");
        return jsonBuilder.ToString();
    }

    public static string DataTableJson(DataTable dt)
    {
        StringBuilder jsonBuilder = new StringBuilder();
        jsonBuilder.Append("{\"");
        jsonBuilder.Append("data");
        jsonBuilder.Append("\":[");
        for (int i = 0; i < dt.Rows.Count; i++)
        {
            jsonBuilder.Append("{");
            for (int j = 0; j < dt.Columns.Count; j++)
            {
                jsonBuilder.Append("\"");
                jsonBuilder.Append(dt.Columns[j].ColumnName);
                jsonBuilder.Append("\":\"");
                jsonBuilder.Append(dt.Rows[i][j].ToString());
                jsonBuilder.Append("\",");
            }
            jsonBuilder.Remove(jsonBuilder.Length - 1, 1);
            jsonBuilder.Append("},");
        }
        jsonBuilder.Remove(jsonBuilder.Length - 1, 1);
        jsonBuilder.Append("],");
        jsonBuilder.Append("\"");
        jsonBuilder.Append("ret");
        jsonBuilder.Append("\":");
        jsonBuilder.Append("\"0" + "\",");
        jsonBuilder.Append("\"msg\":\"ok" + "\"");

        jsonBuilder.Append("}");
        return jsonBuilder.ToString();
    }

    /// <summary>
    /// 汉字转换为Unicode编码
    /// </summary>
    /// <param name="str">要编码的汉字字符串</param>
    /// <returns>Unicode编码的的字符串</returns>
    public static string ToUnicode(string str)
    {
        //return HttpUtility.UrlEncodeUnicode(str);
        UnicodeEncoding unicode = new UnicodeEncoding();
        Byte[] encodedBytes = unicode.GetBytes(str);
        //string str = System.Text.Encoding.Default.GetString(encodedBytes);
        return Encoding.Default.GetString(encodedBytes);
        // byte[] bts = Encoding.Unicode.GetBytes(str);
        // string r = "";
        //  for (int i = 0; i < bts.Length; i += 2) r += "\\u" + bts[i + 1].ToString("x").PadLeft(2, '0') + bts[i].ToString("x").PadLeft(2, '0');
        // return r;
    }



    public static string ToUnicodetest2(string str)
{
    string outStr = "";
    if (!string.IsNullOrEmpty(str))
    {
        for (int i = 0; i < str.Length; i++)
        {
            outStr += "/u" + ((int)str[i]).ToString("x");
        }
    }
    return outStr;
}
    public static string Utf8ToUtf16(string utf8String)
    {
        // Get UTF8 bytes by reading each byte with ANSI encoding
        byte[] utf8Bytes = Encoding.Default.GetBytes(utf8String);

        // Convert UTF8 bytes to UTF16 bytes
        byte[] utf16Bytes = Encoding.Convert(Encoding.UTF8, Encoding.Unicode, utf8Bytes);

        // Return UTF16 bytes as UTF16 string
        return Encoding.Unicode.GetString(utf16Bytes);
    }
//public static string ToUnicodetest(string str)
//    {
//        string temp1 = "";
//        string temp2 = "";
//        for (int i = 1; i < str.Length; i++)
//        {
//            temp2=Conversion.
//        }

//        for (int i = 0; i < bts.Length; i += 2) r += "\\u" + bts[i + 1].ToString("x").PadLeft(2, '0') + bts[i].ToString("x").PadLeft(2, '0');
//        return r;
//    }

    public static  string test(string data)
    {
        string str = data;

        System.Text.UnicodeEncoding encodingUNICODE = new System.Text.UnicodeEncoding();

        str = encodingUNICODE.GetString(new UnicodeEncoding().GetBytes(str));
        return str;
    }

    public static string DataTableJsonForWeiXintest(DataTable dt)
    {
        StringBuilder jsonBuilder = new StringBuilder();
        jsonBuilder.Append("{\"");
        jsonBuilder.Append("data");
        jsonBuilder.Append("\":[");
        for (int i = 0; i < dt.Rows.Count; i++)
        {
            jsonBuilder.Append("{");
            for (int j = 0; j < dt.Columns.Count; j++)
            {
                jsonBuilder.Append("\"");
                jsonBuilder.Append(dt.Columns[j].ColumnName);
                jsonBuilder.Append("\":\"");
                string strRow = dt.Rows[i][j].ToString();

                jsonBuilder.Append(Utf8ToUtf16(dt.Rows[i][j].ToString()));
                jsonBuilder.Append("\",");
            }
            jsonBuilder.Remove(jsonBuilder.Length - 1, 1);
            jsonBuilder.Append("},");
        }
        jsonBuilder.Remove(jsonBuilder.Length - 1, 1);
        jsonBuilder.Append("],");
        jsonBuilder.Append("\"");
        jsonBuilder.Append("ret");
        jsonBuilder.Append("\":");
        jsonBuilder.Append("\"0" + "\",");
        jsonBuilder.Append("\"msg\":\"ok" + "\"");

        jsonBuilder.Append("}");
        return jsonBuilder.ToString();
    }

    public static string DataTableJsonOnlyOneColumn(string ColumnName,string ColumnValue)
    {
        StringBuilder jsonBuilder = new StringBuilder();
        jsonBuilder.Append("{\"");
        jsonBuilder.Append("data");
        jsonBuilder.Append("\":[");

        jsonBuilder.Append("{");
        jsonBuilder.Append("\"");
        jsonBuilder.Append(ColumnName);
        jsonBuilder.Append("\":\"");
        jsonBuilder.Append(ColumnValue);
        jsonBuilder.Append("\",");

        jsonBuilder.Remove(jsonBuilder.Length - 1, 1);
        jsonBuilder.Append("},");
        jsonBuilder.Remove(jsonBuilder.Length - 1, 1);
        jsonBuilder.Append("],");
        jsonBuilder.Append("\"");
        jsonBuilder.Append("ret");
        jsonBuilder.Append("\":");
        jsonBuilder.Append("\"0" + "\",");
        jsonBuilder.Append("\"msg\":\"ok" + "\"");

        jsonBuilder.Append("}");
        return jsonBuilder.ToString();
    }

    public static string DataTableJsonOnlyOneColumnForWeiXin(string ColumnValue)
    {
        StringBuilder jsonBuilder = new StringBuilder();
        jsonBuilder.Append("{\"");
        jsonBuilder.Append("data");
        jsonBuilder.Append("\":[");

            jsonBuilder.Append("{");

                jsonBuilder.Append("\"");
                jsonBuilder.Append("CityName");
                jsonBuilder.Append("\":\"");
                jsonBuilder.Append(ColumnValue);
                jsonBuilder.Append("\",");

            jsonBuilder.Remove(jsonBuilder.Length - 1, 1);
            jsonBuilder.Append("},");
        jsonBuilder.Remove(jsonBuilder.Length - 1, 1);
        jsonBuilder.Append("],");
        jsonBuilder.Append("\"");
        jsonBuilder.Append("ret");
        jsonBuilder.Append("\":");
        jsonBuilder.Append("\"0" + "\",");
        jsonBuilder.Append("\"msg\":\"ok" + "\"");

        jsonBuilder.Append("}");
        return jsonBuilder.ToString();
    }

    public static string DataTableJsonForIOS(DataTable dt)
    {
        StringBuilder jsonBuilder = new StringBuilder();
        jsonBuilder.Append("{\"");
        jsonBuilder.Append("data");
        jsonBuilder.Append("\":[");
        for (int i = 0; i < dt.Rows.Count; i++)
        {
            jsonBuilder.Append("{");
            for (int j = 0; j < dt.Columns.Count; j++)
            {
                jsonBuilder.Append("\"");
                jsonBuilder.Append(dt.Columns[j].ColumnName);
                jsonBuilder.Append("\":\"");
                jsonBuilder.Append(dt.Rows[i][j].ToString());
                jsonBuilder.Append("\",");
            }
            jsonBuilder.Remove(jsonBuilder.Length - 1, 1);
            jsonBuilder.Append("},");
        }
        jsonBuilder.Remove(jsonBuilder.Length - 1, 1);
        jsonBuilder.Append("]");
        jsonBuilder.Append("}");
        return jsonBuilder.ToString();
    }

    public static string getResultJson(int ret, string msg)
    {
        StringBuilder jsonBuilder = new StringBuilder();
        jsonBuilder.Append("{\"");
        jsonBuilder.Append("ret");
        jsonBuilder.Append("\":");
        jsonBuilder.Append("\"" + ret.ToString() + "\",");
        jsonBuilder.Append("\"msg\":\"" + msg + "\"");
        jsonBuilder.Append("}");
        return jsonBuilder.ToString();
    }

    public static string OperationResultJson(string ret,string msg)
    {
        StringBuilder jsonBuilder = new StringBuilder();
        jsonBuilder.Append("{\"");
        jsonBuilder.Append("ret");
        jsonBuilder.Append("\":");
        jsonBuilder.Append("\""+ret+"\",");
        jsonBuilder.Append("\"msg\":\""+msg+"\"");
        jsonBuilder.Append("}");
        return jsonBuilder.ToString();
    }

    public static string DataSetToNewJson(DataSet ds)
    {
        return JsonConvert.SerializeObject(ds, Formatting.Indented);
    }

    ///     
    /// dataTable转换成Json格式    
    ///     
    ///     
    ///     
    public static string DataSetJson(DataSet ds)
    {
       
        StringBuilder jsonBuilder = new StringBuilder();
        jsonBuilder.Append("{\"");
        jsonBuilder.Append("Table");
        jsonBuilder.Append("\":[");
        for (int inttablecount = 0; inttablecount < ds.Tables.Count; inttablecount++)
        {
            DataTable dt = ds.Tables[inttablecount];
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                jsonBuilder.Append("{");
                for (int j = 0; j < dt.Columns.Count; j++)
                {
                    jsonBuilder.Append("\"");
                    jsonBuilder.Append(dt.Columns[j].ColumnName);
                    jsonBuilder.Append("\":\"");
                    jsonBuilder.Append(dt.Rows[i][j].ToString());
                    jsonBuilder.Append("\",");
                }
                jsonBuilder.Remove(jsonBuilder.Length - 1, 1);
                jsonBuilder.Append("},");
            }
        }
 

        jsonBuilder.Remove(jsonBuilder.Length - 1, 1);
        jsonBuilder.Append("]");
        jsonBuilder.Append("}");
        return jsonBuilder.ToString();
    }



    private static byte[] KeysCode = { 0x12, 0x34, 0x56, 0x78, 0x90, 0xAB, 0xCD, 0xEF };
    /// <summary>
    /// encryptKey必须是8位字符串,加密
    /// </summary>
    /// <param name="encryptString"></param>
    /// <param name="encryptKey"></param>
    /// <returns></returns>
    public static string EncryptDES(string encryptString, string encryptKey)
    {

        encryptKey = "87654321";
        try
        {
            byte[] rgbKey = Encoding.UTF8.GetBytes(encryptKey.Substring(0, 8));
            byte[] rgbIV = KeysCode;
            byte[] inputByteArray = Encoding.UTF8.GetBytes(encryptString);
            DESCryptoServiceProvider dCSP = new DESCryptoServiceProvider();
            MemoryStream mStream = new MemoryStream();
            CryptoStream cStream = new CryptoStream(mStream, dCSP.CreateEncryptor(rgbKey, rgbIV), CryptoStreamMode.Write);
            cStream.Write(inputByteArray, 0, inputByteArray.Length);
            cStream.FlushFinalBlock();
            return Convert.ToBase64String(mStream.ToArray());
        }
        catch
        {
            return encryptString;
        }
    }

    ///<summary>
    /// 生成缩略图,也就是生成一张小图片
    ///@author 何求
    /// </summary>
    /// <param name="originalImagePath">源图路径（物理路径）</param>
    /// <param name="thumbnailPath">缩略图路径（物理路径）</param>
    /// <param name="width">缩略图宽度</param>
    /// <param name="height">缩略图高度</param>
    /// <param name="mode">生成缩略图的方式</param>   
    public static void MakeThumbnail(string originalImagePath, string thumbnailPath, int width, int height, string mode, out string outthumbnailPath)
    {
        //oper.opertxt(System.DateTime.Now + "MakeThumbnail1111");
        System.Drawing.Image originalImage = System.Drawing.Image.FromFile(originalImagePath);

        int towidth = width;
        int toheight = height;

        int x = 0;
        int y = 0;
        int ow = originalImage.Width;
        int oh = originalImage.Height;
        if (originalImage.Width <= towidth && originalImage.Height <= toheight)
        {
            //File.Copy(originalImagePath, thumbnailPath, true);
            outthumbnailPath = thumbnailPath;
            //oper.opertxt(System.DateTime.Now + "MakeThumbnail2222");
            return;
        }
        else
        {
            switch (mode)
            {
                case "HW"://指定高宽缩放（可能变形）               
                    break;
                case "W"://指定宽，高按比例                   
                    toheight = originalImage.Height * width / originalImage.Width;
                    break;
                case "H"://指定高，宽按比例
                    towidth = originalImage.Width * height / originalImage.Height;
                    break;
                case "zyHW"://根据比例自动选择按高或宽进行缩写
                    if (originalImage.Height / height >= originalImage.Width / width)//按高缩写
                    {
                        towidth = originalImage.Width * height / originalImage.Height;
                        //oper.opertxt(System.DateTime.Now + "MakeThumbnail3");
                    }
                    else
                    {
                        toheight = originalImage.Height * width / originalImage.Width;//按宽缩写
                        //oper.opertxt(System.DateTime.Now + "MakeThumbnail4");
                    }
                    break;
                case "Cut"://指定高宽裁减（不变形）

                    if ((double)originalImage.Width / (double)originalImage.Height > (double)towidth / (double)toheight)
                    {
                        oh = originalImage.Height;
                        ow = originalImage.Height * towidth / toheight;
                        y = 0;
                        x = (originalImage.Width - ow) / 2;
                    }
                    else
                    {
                        ow = originalImage.Width;
                        oh = originalImage.Width * height / towidth;
                        x = 0;
                        y = (originalImage.Height - oh) / 2;
                    }
                    break;
                default:
                    break;
            }
        }

       // oper.opertxt(System.DateTime.Now + "MakeThumbnail5");
        //新建一个bmp图片
        System.Drawing.Image bitmap = new System.Drawing.Bitmap(towidth, toheight);

        //新建一个画板
        Graphics g = System.Drawing.Graphics.FromImage(bitmap);

        //设置高质量插值法
        g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.High;

        //设置高质量,低速度呈现平滑程度
        g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;

        //清空画布并以透明背景色填充
        g.Clear(Color.Transparent);

        //在指定位置并且按指定大小绘制原图片的指定部分
        g.DrawImage(originalImage, new Rectangle(0, 0, towidth, toheight),
         new Rectangle(x, y, ow, oh),
         GraphicsUnit.Pixel);
      //  oper.opertxt(System.DateTime.Now + "MakeThumbnail6");
        try
        {
            //以jpg格式保存缩略图
            bitmap.Save(thumbnailPath, System.Drawing.Imaging.ImageFormat.Jpeg);
           // oper.opertxt(System.DateTime.Now + "MakeThumbnail7");
            outthumbnailPath = thumbnailPath;
        }
        catch (System.Exception e)
        {
            //oper.opertxt(System.DateTime.Now + "MakeThumbnail8");
            throw e;
        }
        finally
        {
            originalImage.Dispose();
            bitmap.Dispose();
            g.Dispose();
            //oper.opertxt(System.DateTime.Now + "MakeThumbnail9");
        }
    }
}
