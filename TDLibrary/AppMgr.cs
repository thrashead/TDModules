using System.Web.Configuration;

namespace TDLibrary
{
    public class AppMgr
	{
		public static string MainPath
		{
			get
			{
				return WebConfigurationManager.AppSettings["MainPath"] != null ? WebConfigurationManager.AppSettings["MainPath"].ToString() : string.Empty;
			}
		}

		public static string ScriptPath
		{
			get
			{
				return WebConfigurationManager.AppSettings["ScriptPath"] != null ? MainPath + WebConfigurationManager.AppSettings["ScriptPath"].ToString() : string.Empty;
			}
		}

		public static string StylePath
		{
			get
			{
				return WebConfigurationManager.AppSettings["StylePath"] != null ? MainPath + WebConfigurationManager.AppSettings["StylePath"].ToString() : string.Empty;
			}
		}

		public static string ImagePath
		{
			get
			{
				return WebConfigurationManager.AppSettings["ImagePath"] != null ? MainPath + WebConfigurationManager.AppSettings["ImagePath"].ToString() : string.Empty;
			}
		}

		public static string FilePath
		{
			get
			{
				return WebConfigurationManager.AppSettings["FilePath"] != null ? MainPath + WebConfigurationManager.AppSettings["FilePath"].ToString() : string.Empty;
			}
		}

		public static string UploadPath
		{
			get
			{
				return WebConfigurationManager.AppSettings["UploadPath"] != null ? MainPath + WebConfigurationManager.AppSettings["UploadPath"].ToString() : string.Empty;
			}
        }

        public static string AjaxPath
        {
            get
            {
                return WebConfigurationManager.AppSettings["AjaxPath"] != null ? MainPath + WebConfigurationManager.AppSettings["AjaxPath"].ToString() : string.Empty;
            }
        }

        public static string SystemUser
		{
			get
			{
				return WebConfigurationManager.AppSettings["SystemUser"] != null ? WebConfigurationManager.AppSettings["SystemUser"].ToString() : string.Empty;
			}
        }

        public static string AdminPath
        {
            get
            {
                return WebConfigurationManager.AppSettings["AdminPath"] != null ? WebConfigurationManager.AppSettings["AdminPath"].ToString() : string.Empty;
            }
        }

        public static string AdminScriptPath
        {
            get
            {
                return WebConfigurationManager.AppSettings["AdminScriptPath"] != null ? MainPath + WebConfigurationManager.AppSettings["AdminScriptPath"].ToString() : string.Empty;
            }
        }

        public static string AdminStylePath
        {
            get
            {
                return WebConfigurationManager.AppSettings["AdminStylePath"] != null ? MainPath + WebConfigurationManager.AppSettings["AdminStylePath"].ToString() : string.Empty;
            }
        }

        public static string AdminImagePath
        {
            get
            {
                return WebConfigurationManager.AppSettings["AdminImagePath"] != null ? MainPath + WebConfigurationManager.AppSettings["AdminImagePath"].ToString() : string.Empty;
            }
        }

        public static string AdminProjectFilePath
        {
            get
            {
                return WebConfigurationManager.AppSettings["AdminProjectFilePath"] != null ? AdminPath + WebConfigurationManager.AppSettings["AdminProjectFilePath"].ToString() : string.Empty;
            }
        }

        public static string AdminUploadPath
        {
            get
            {
                return WebConfigurationManager.AppSettings["AdminUploadPath"] != null ? AdminPath + WebConfigurationManager.AppSettings["AdminUploadPath"].ToString() : string.Empty;
            }
        }

        public static string AdminAjaxPath
        {
            get
            {
                return WebConfigurationManager.AppSettings["AdminAjaxPath"] != null ? AdminPath + WebConfigurationManager.AppSettings["AdminAjaxPath"].ToString() : string.Empty;
            }
        }
    }
}
