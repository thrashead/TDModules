using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Web;

namespace TDLibrary
{
    public class Uploader
    {
        public string FileName { get; set; }
        public string ThumbName { get; set; }
        public string ErrorMessage { get; set; }
        public bool Control { get; set; }
        public bool? HasFile { get; set; }

        public UploadErrors? UploadError { get; set; }

        public static long MaxFileSize
        {
            get
            {
                return 256000;
            }
        }

        public static long MaxPictureSize
        {
            get
            {
                return 256000;
            }
        }

        public static string UploadPath
        {
            get
            {
                return HttpContext.Current.Server.MapPath("~/Uploads/");
            }
        }

        public static Uploader UploadPicture(bool addGuid = true, string uploadPath = null, bool createThumb = true, long maxSize = 0)
        {
            Uploader uploader = new Uploader();
            uploader.Control = false;
            uploader.HasFile = null;
            uploader.UploadError = null;

            uploadPath = uploadPath == null ? UploadPath : uploadPath;
            maxSize = maxSize <= 0 ? MaxPictureSize : maxSize;

            try
            {
                if (HttpContext.Current.Request.Files.Count > 0)
                {
                    var file = HttpContext.Current.Request.Files[0];

                    if (file?.FileName.IsNull() != true)
                    {
                        if (file.ContentLength >= 0 && file.ContentLength < maxSize)
                        {
                            var fileName = Path.GetFileName(file.FileName);

                            if (addGuid)
                                fileName = fileName.Split('.')[0] + "_" + Guider.GetGuid(5) + "." + fileName.Split('.')[1];

                            var path = Path.Combine(uploadPath, fileName);
                            file.SaveAs(path);

                            if (createThumb)
                            {
                                var thumbName = fileName.Split('.')[0] + "_t." + fileName.Split('.')[1];
                                CreateThumb(320, 240, file.InputStream, Path.Combine(uploadPath, thumbName));
                                uploader.ThumbName = thumbName;
                            }

                            uploader.HasFile = true;
                            uploader.Control = true;
                            uploader.FileName = fileName;
                        }
                        else
                        {
                            uploader.UploadError = UploadErrors.Size;
                            uploader.HasFile = true;
                            uploader.Control = false;
                            uploader.ErrorMessage = "Resim boyutu " + (MaxPictureSize / 1024) + "kb'tan küçük olmalı.";
                        }
                    }
                    else
                    {
                        uploader.HasFile = false;
                        uploader.Control = false;
                        uploader.ErrorMessage = "Resim yüklenemedi veya yüklenecek dosya seçmediniz.";
                    }
                }
                else
                {
                    uploader.HasFile = false;
                    uploader.Control = false;
                    uploader.ErrorMessage = "Resim yüklenemedi veya yüklenecek dosya seçmediniz.";
                }
            }
            catch (Exception ex)
            {
                uploader.UploadError = UploadErrors.Other;
                uploader.Control = false;
                uploader.ErrorMessage = "Resim yüklenirken bir hata oluştu. (" + ex.Message + ")";
            }

            return uploader;
        }

        public static List<Uploader> UploadPictures(bool addGuid = true, string uploadPath = null, bool createThumb = true, long maxSize = 0)
        {
            List<Uploader> uploaderList = new List<Uploader>();
            Uploader uploader;

            uploadPath = uploadPath == null ? UploadPath : uploadPath;
            maxSize = maxSize <= 0 ? MaxPictureSize : maxSize;

            try
            {
                if (HttpContext.Current.Request.Files.Count > 0)
                {
                    for (int i = 0; i < HttpContext.Current.Request.Files.Count; i++)
                    {
                        uploader = new Uploader();
                        uploader.Control = false;

                        var file = HttpContext.Current.Request.Files[i];

                        if (file?.FileName.IsNull() != true)
                        {
                            if (file.ContentLength >= 0 && file.ContentLength < maxSize)
                            {
                                var fileName = Path.GetFileName(file.FileName);

                                if (addGuid)
                                    fileName = fileName.Split('.')[0] + "_" + Guider.GetGuid(5) + "." + fileName.Split('.')[1];

                                var path = Path.Combine(uploadPath, fileName);
                                file.SaveAs(path);

                                if (createThumb)
                                {
                                    var thumbName = fileName.Split('.')[0] + "_t." + fileName.Split('.')[1];
                                    CreateThumb(320, 240, file.InputStream, Path.Combine(uploadPath, thumbName));
                                    uploader.ThumbName = thumbName;
                                }

                                uploader.HasFile = true;
                                uploader.Control = true;
                                uploader.FileName = fileName;
                                uploaderList.Add(uploader);
                            }
                            else
                            {
                                uploader.UploadError = UploadErrors.Size;
                                uploader.HasFile = true;
                                uploader.Control = false;
                                uploader.ErrorMessage = "Resim boyutu " + (MaxPictureSize / 1024) + "kb'tan küçük olmalı.";
                                uploaderList.Add(uploader);
                            }
                        }
                        else
                        {
                            uploader.HasFile = false;
                            uploader.Control = false;
                            uploader.ErrorMessage = "Resim yüklenemedi veya yüklenecek dosya seçmediniz.";
                            uploaderList.Add(uploader);
                        }
                    }
                }
                else
                {
                    uploader = new Uploader();
                    uploader.HasFile = false;
                    uploader.Control = false;
                    uploader.ErrorMessage = "Resim yüklenemedi veya yüklenecek dosya seçmediniz.";
                    uploaderList.Add(uploader);
                }
            }
            catch (Exception ex)
            {
                uploader = new Uploader();
                uploader.UploadError = UploadErrors.Other;
                uploader.HasFile = null;
                uploader.Control = false;
                uploader.ErrorMessage = "Resim yüklenirken bir hata oluştu. (" + ex.Message + ")";
            }

            return uploaderList;
        }

        public static List<Uploader> UploadGallery(string galleryName, bool addGuid = true, string uploadPath = null, bool createThumb = true, long maxSize = 0)
        {
            List<Uploader> uploaderList = new List<Uploader>();
            Uploader uploader;

            uploadPath = uploadPath == null ? UploadPath : uploadPath;
            maxSize = maxSize <= 0 ? MaxPictureSize : maxSize;

            try
            {
                if (HttpContext.Current.Request.Files.Count > 0)
                {
                    for (int i = 0; i < HttpContext.Current.Request.Files.Count; i++)
                    {
                        uploader = new Uploader();
                        uploader.Control = false;

                        var file = HttpContext.Current.Request.Files[i];

                        if (file?.FileName.IsNull() != true)
                        {
                            if (file.ContentLength >= 0 && file.ContentLength < maxSize)
                            {
                                var fileName = Path.GetFileName(file.FileName);

                                if (addGuid)
                                    fileName = fileName.Split('.')[0] + "_" + Guider.GetGuid(5) + "." + fileName.Split('.')[1];

                                var path = Path.Combine(uploadPath, galleryName + "/" + fileName);
                                file.SaveAs(path);

                                if (createThumb)
                                {
                                    var thumbName = fileName.Split('.')[0] + "_t." + fileName.Split('.')[1];
                                    CreateThumb(320, 240, file.InputStream, Path.Combine(uploadPath, galleryName + "/" + thumbName));
                                    uploader.ThumbName = thumbName;
                                }

                                uploader.HasFile = true;
                                uploader.Control = true;
                                uploader.FileName = fileName;
                                uploaderList.Add(uploader);
                            }
                            else
                            {
                                uploader.UploadError = UploadErrors.Size;
                                uploader.HasFile = true;
                                uploader.Control = false;
                                uploader.ErrorMessage = "Resim boyutu " + (MaxPictureSize / 1024) + "kb'tan küçük olmalı.";
                                uploaderList.Add(uploader);
                            }
                        }
                        else
                        {
                            uploader.HasFile = false;
                            uploader.Control = false;
                            uploader.ErrorMessage = "Resim yüklenemedi veya yüklenecek dosya seçmediniz.";
                            uploaderList.Add(uploader);
                        }
                    }
                }
                else
                {
                    uploader = new Uploader();
                    uploader.HasFile = false;
                    uploader.Control = false;
                    uploader.ErrorMessage = "Resim yüklenemedi veya yüklenecek dosya seçmediniz.";
                    uploaderList.Add(uploader);
                }
            }
            catch (Exception ex)
            {
                uploader = new Uploader();
                uploader.UploadError = UploadErrors.Other;
                uploader.HasFile = null;
                uploader.Control = false;
                uploader.ErrorMessage = "Resim yüklenirken bir hata oluştu. (" + ex.Message + ")";
            }

            return uploaderList;
        }

        public static Uploader UploadFile(bool addGuid = true, string uploadPath = null, long maxSize = 0)
        {
            Uploader uploader = new Uploader();
            uploader.Control = false;

            uploadPath = uploadPath == null ? UploadPath : uploadPath;
            maxSize = maxSize <= 0 ? MaxFileSize : maxSize;

            try
            {
                if (HttpContext.Current.Request.Files.Count > 0)
                {
                    var file = HttpContext.Current.Request.Files[0];

                    if (file?.FileName.IsNull() != true)
                    {
                        if (file.ContentLength >= 0 && file.ContentLength < maxSize)
                        {
                            var fileName = Path.GetFileName(file.FileName);

                            if (addGuid)
                                fileName = fileName.Split('.')[0] + "_" + Guider.GetGuid(5) + "." + fileName.Split('.')[1];

                            var path = Path.Combine(uploadPath, fileName);
                            file.SaveAs(path);

                            uploader.HasFile = true;
                            uploader.Control = true;
                            uploader.FileName = fileName;
                        }
                        else
                        {
                            uploader.UploadError = UploadErrors.Size;
                            uploader.HasFile = true;
                            uploader.Control = false;
                            uploader.ErrorMessage = "Dosya boyutu " + (MaxPictureSize / 1024) + "kb'tan küçük olmalı.";
                        }
                    }
                    else
                    {
                        uploader.HasFile = false;
                        uploader.Control = false;
                        uploader.ErrorMessage = "Dosya yüklenemedi veya yüklenecek dosya seçmediniz.";
                    }
                }
                else
                {
                    uploader.HasFile = false;
                    uploader.Control = false;
                    uploader.ErrorMessage = "Dosya yüklenemedi veya yüklenecek dosya seçmediniz.";
                }
            }
            catch (Exception ex)
            {
                uploader.UploadError = UploadErrors.Other;
                uploader.HasFile = null;
                uploader.Control = false;
                uploader.ErrorMessage = "Dosya yüklenirken bir hata oluştu. (" + ex.Message + ")";
            }

            return uploader;
        }

        public static List<Uploader> UploadFiles(bool addGuid = true, string uploadPath = null, long maxSize = 0)
        {
            List<Uploader> uploaderList = new List<Uploader>();
            Uploader uploader;

            uploadPath = uploadPath == null ? UploadPath : uploadPath;
            maxSize = maxSize <= 0 ? MaxFileSize : maxSize;

            try
            {
                if (HttpContext.Current.Request.Files.Count > 0)
                {
                    for (int i = 0; i < HttpContext.Current.Request.Files.Count; i++)
                    {
                        uploader = new Uploader();
                        uploader.Control = false;

                        var file = HttpContext.Current.Request.Files[i];

                        if (file?.FileName.IsNull() != true)
                        {
                            if (file.ContentLength >= 0 && file.ContentLength < maxSize)
                            {
                                var fileName = Path.GetFileName(file.FileName);

                                if (addGuid)
                                    fileName = fileName.Split('.')[0] + "_" + Guider.GetGuid(5) + "." + fileName.Split('.')[1];

                                var path = Path.Combine(uploadPath, fileName);
                                file.SaveAs(path);

                                uploader.HasFile = true;
                                uploader.Control = true;
                                uploader.FileName = fileName;
                                uploaderList.Add(uploader);
                            }
                            else
                            {
                                uploader.UploadError = UploadErrors.Size;
                                uploader.HasFile = true;
                                uploader.Control = false;
                                uploader.ErrorMessage = "Dosya boyutu " + (MaxPictureSize / 1024) + "kb'tan küçük olmalı.";
                                uploaderList.Add(uploader);
                            }
                        }
                        else
                        {
                            uploader.HasFile = false;
                            uploader.Control = false;
                            uploader.ErrorMessage = "Dosya yüklenemedi veya yüklenecek dosya seçmediniz.";
                            uploaderList.Add(uploader);
                        }
                    }
                }
                else
                {
                    uploader = new Uploader();
                    uploader.HasFile = false;
                    uploader.Control = false;
                    uploader.ErrorMessage = "Dosya yüklenemedi veya yüklenecek dosya seçmediniz.";
                    uploaderList.Add(uploader);
                }
            }
            catch (Exception ex)
            {
                uploader = new Uploader();
                uploader.UploadError = UploadErrors.Other;
                uploader.HasFile = null;
                uploader.Control = false;
                uploader.ErrorMessage = "Dosya yüklenirken bir hata oluştu. (" + ex.Message + ")";
            }

            return uploaderList;
        }

        private static void CreateThumb(int Width, int Height, Stream streamImg, string saveFilePath)
        {
            Bitmap sourceImage = new Bitmap(streamImg);
            using (Bitmap objBitmap = new Bitmap(Width, Height))
            {
                objBitmap.SetResolution(sourceImage.HorizontalResolution, sourceImage.VerticalResolution);
                using (Graphics objGraphics = Graphics.FromImage(objBitmap))
                {
                    objGraphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;

                    objGraphics.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
                    objGraphics.DrawImage(sourceImage, 0, 0, Width, Height);

                    objBitmap.Save(saveFilePath);
                }
            }
        }

        public enum UploadErrors
        {
            Size,
            Other
        }
    }
}
