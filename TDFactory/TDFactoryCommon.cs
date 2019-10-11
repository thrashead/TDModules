using Common;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using TDFactory.Helper;
using InType = TDFactory.Helper.ExMethods.InType;

namespace TDFactory
{
    public partial class TDFactory : Form
    {
        #region Common

        void CreateRepository()
        {
            foreach (string Table in selectedTables)
            {
                List<string> identityColumns = Helper.Helper.ReturnIdentityColumn(connectionInfo, Table);

                string id = identityColumns.Count > 0 ? identityColumns.FirstOrDefault() : "id";

                identityColumns = identityColumns.IdentityCheck(lstSeciliKolonlar);

                SqlConnection con = new SqlConnection(Helper.Helper.CreateConnectionText(connectionInfo));
                List<ForeignKeyChecker> fkcList = ForeignKeyCheck(con, Table);
                fkcList = fkcList.Where(a => a.PrimaryTableName == Table).ToList();

                List<ForeignKeyChecker> fkcListForeign = ForeignKeyCheck(con);
                fkcListForeign = fkcListForeign.Where(a => a.ForeignTableName == Table).ToList();

                //Class
                using (FileStream fs = new FileStream(PathAddress + "\\" + projectFolder + "\\" + repositoryName + "\\Models\\" + Table + "\\" + Table + ".cs", FileMode.Create))
                {
                    using (StreamWriter yaz = new StreamWriter(fs, Encoding.Unicode))
                    {
                        List<ColumnInfo> columnNames = TableColumns(Table);
                        List<ColumnInfo> urlColumns = TableColumns(Table, ColumnType.UrlColumns);
                        List<ColumnInfo> guidColumns = TableColumns(Table, ColumnType.GuidColumns);
                        List<ColumnInfo> codeColumns = TableColumns(Table, ColumnType.CodeColumns);
                        List<ColumnInfo> fileColumns = TableColumns(Table, ColumnType.FileColumns);
                        List<ColumnInfo> imageColumns = TableColumns(Table, ColumnType.ImageColumns);
                        List<ColumnInfo> searchColumns = TableColumns(Table, ColumnType.SearchColumns);
                        bool deleted = TableColumns(Table, ColumnType.DeletedColumns).Count > 0 ? true : false;

                        bool allowHtml = false;

                        foreach (ColumnInfo column in columnNames)
                        {
                            if (column.Type != null)
                            {
                                if (column.Type.Name == "String")
                                {
                                    if (column.CharLength == -1 && !column.ColumnName.In(FileColumns, InType.ToUrlLower) && !column.ColumnName.In(ImageColumns, InType.ToUrlLower))
                                    {
                                        allowHtml = true;

                                        break;
                                    }
                                }
                            }
                        }

                        yaz.WriteLine("using System;");
                        yaz.WriteLine("using System.Collections.Generic;");

                        if ((chkRepositoryInternal.Checked && !chkAngular.Checked) || !chkRepositoryInternal.Checked)
                        {
                            yaz.WriteLine("using System.ComponentModel.DataAnnotations;");
                        }

                        if (fkcListForeign.Count > 0 || allowHtml)
                        {
                            yaz.WriteLine("using System.Web.Mvc;");
                        }

                        yaz.WriteLine("using System.Linq;");
                        yaz.WriteLine(repositoryType);
                        yaz.WriteLine("using TDLibrary;");

                        if (fkcList.Count > 0)
                        {
                            foreach (ForeignKeyChecker fkc in fkcList.GroupBy(a => a.ForeignTableName).Select(a => a.First()).ToList())
                            {
                                string ForeignTableName = fkc.ForeignTableName;
                                yaz.WriteLine("using " + repositoryName + "." + ForeignTableName + "Model;");

                            }
                        }

                        yaz.WriteLine("");

                        yaz.WriteLine("namespace " + repositoryName + "." + Table + "Model");
                        yaz.WriteLine("{");

                        yaz.WriteLine("\tpublic class " + Table + " : I" + Table);
                        yaz.WriteLine("\t{");

                        yaz.WriteLine("\t\treadonly " + cmbVeritabani.Text + "Entities entity = new " + cmbVeritabani.Text + "Entities();");
                        yaz.WriteLine("");

                        yaz.WriteLine("\t\t#region Model");
                        yaz.WriteLine("");

                        if (fkcList.Count > 0 || fkcListForeign.Count > 0)
                        {
                            yaz.WriteLine("\t\tpublic " + Table + "()");
                            yaz.WriteLine("\t\t{");

                            if (fkcList.Count > 0)
                            {
                                foreach (ForeignKeyChecker fkc in fkcList.GroupBy(a => a.ForeignTableName).Select(a => a.First()).ToList())
                                {
                                    string ForeignTableName = fkc.ForeignTableName;
                                    yaz.WriteLine("\t\t\t" + ForeignTableName + "List = new List<I" + ForeignTableName + ">();");
                                }
                            }

                            if (fkcListForeign.Count > 0)
                            {
                                foreach (ForeignKeyChecker fkc in fkcListForeign.GroupBy(a => a.PrimaryTableName).Select(a => a.First()).ToList())
                                {
                                    string PrimaryTableName = fkc.PrimaryTableName;
                                    yaz.WriteLine("\t\t\t" + PrimaryTableName + "List = new List<SelectListItem>();");
                                }
                            }

                            yaz.WriteLine("\t\t}");
                            yaz.WriteLine("");
                        }

                        int counter = columnNames.Count;
                        int i = 1;

                        foreach (ColumnInfo column in columnNames)
                        {
                            if (column.Type != null)
                            {
                                if (column.Type.Name == "String")
                                {
                                    if (column.CharLength == -1 && !column.ColumnName.In(FileColumns, InType.ToUrlLower) && !column.ColumnName.In(ImageColumns, InType.ToUrlLower))
                                    {
                                        yaz.WriteLine("\t\t[AllowHtml]");

                                        if (!chkAngular.Checked)
                                        {
                                            yaz.WriteLine("\t\t[DataType(DataType.MultilineText)]");
                                        }
                                    }
                                }

                                if ((chkRepositoryInternal.Checked && !chkAngular.Checked) || !chkRepositoryInternal.Checked)
                                {
                                    if (!column.ColumnName.In(DeletedColumns, InType.ToUrlLower) && !column.ColumnName.In(UrlColumns, InType.ToUrlLower) && !column.ColumnName.In(GuidColumns, InType.ToUrlLower))
                                    {
                                        if (!column.IsIdentity)
                                        {
                                            if (column.Type.Name != "Boolean")
                                            {
                                                if (!column.IsNullable)
                                                {
                                                    if (column.Type.Name.In(new string[] { "Int16", "Int32", "Int64" }))
                                                    {
                                                        yaz.WriteLine("\t\t[Required(ErrorMessage = \"" + column.ColumnName + " alanı boş olamaz ve " + column.ColumnName + " alanına en az 0 değeri girmelisiniz.\")]");
                                                        yaz.WriteLine("\t\t[Range(0, int.MaxValue, ErrorMessage = \"" + column.ColumnName + " alanı boş olamaz ve " + column.ColumnName + " alanına en az 0 değeri girmelisiniz.\")]");
                                                    }
                                                    else if (column.Type.Name == "String")
                                                    {
                                                        if (column.Type.Name == "String" && column.CharLength == -1)
                                                        {
                                                            yaz.WriteLine("\t\t[Required(ErrorMessage = \"" + column.ColumnName + " alanı boş olamaz.\")]");
                                                        }
                                                        else
                                                        {
                                                            yaz.WriteLine("\t\t[Required(ErrorMessage = \"" + column.ColumnName + " alanı boş olamaz ve en fazla " + column.CharLength + " karakter olmalıdır.\")]");
                                                            yaz.WriteLine("\t\t[StringLength(" + column.CharLength + ")]");
                                                        }
                                                    }
                                                }
                                            }
                                        }
                                    }
                                }

                                if (column.IsNullable)
                                {
                                    switch (column.Type.Name)
                                    {
                                        case "Int16": yaz.WriteLine("\t\tpublic int? " + column.ColumnName + " { get; set; }"); break;
                                        case "Int32": yaz.WriteLine("\t\tpublic int? " + column.ColumnName + " { get; set; }"); break;
                                        case "Int64": yaz.WriteLine("\t\tpublic Int64? " + column.ColumnName + " { get; set; }"); break;
                                        case "Decimal": yaz.WriteLine("\t\tpublic decimal? " + column.ColumnName + " { get; set; }"); break;
                                        case "Double": yaz.WriteLine("\t\tpublic double? " + column.ColumnName + " { get; set; }"); break;
                                        case "Char": yaz.WriteLine("\t\tpublic char " + column.ColumnName + " { get; set; }"); break;
                                        case "Chars": yaz.WriteLine("\t\tpublic char[] " + column.ColumnName + " { get; set; }"); break;
                                        case "String": yaz.WriteLine("\t\tpublic string " + column.ColumnName + " { get; set; }"); break;
                                        case "Byte": yaz.WriteLine("\t\tpublic byte " + column.ColumnName + " { get; set; }"); break;
                                        case "Bytes": yaz.WriteLine("\t\tpublic byte[] " + column.ColumnName + " { get; set; }"); break;
                                        case "Boolean": yaz.WriteLine("\t\tpublic bool? " + column.ColumnName + " { get; set; }"); break;
                                        case "DateTime": yaz.WriteLine("\t\tpublic DateTime? " + column.ColumnName + " { get; set; }"); break;
                                        case "DateTimeOffset": yaz.WriteLine("\t\tpublic DateTimeOffset? " + column.ColumnName + " { get; set; }"); break;
                                        case "TimeSpan": yaz.WriteLine("\t\tpublic TimeSpan? " + column.ColumnName + " { get; set; }"); break;
                                        case "Single": yaz.WriteLine("\t\tpublic Single? " + column.ColumnName + " { get; set; }"); break;
                                        case "Object": yaz.WriteLine("\t\tpublic object " + column.ColumnName + " { get; set; }"); break;
                                        case "Guid": yaz.WriteLine("\t\tpublic Guid? " + column.ColumnName + " { get; set; }"); break;
                                        default: yaz.WriteLine("\t\tpublic string " + column.ColumnName + " { get; set; }"); break;
                                    }
                                }
                                else
                                {
                                    switch (column.Type.Name)
                                    {
                                        case "Int16": yaz.WriteLine("\t\tpublic int " + column.ColumnName + " { get; set; }"); break;
                                        case "Int32": yaz.WriteLine("\t\tpublic int " + column.ColumnName + " { get; set; }"); break;
                                        case "Int64": yaz.WriteLine("\t\tpublic Int64 " + column.ColumnName + " { get; set; }"); break;
                                        case "Decimal": yaz.WriteLine("\t\tpublic decimal " + column.ColumnName + " { get; set; }"); break;
                                        case "Double": yaz.WriteLine("\t\tpublic double " + column.ColumnName + " { get; set; }"); break;
                                        case "Char": yaz.WriteLine("\t\tpublic char " + column.ColumnName + " { get; set; }"); break;
                                        case "Chars": yaz.WriteLine("\t\tpublic char[] " + column.ColumnName + " { get; set; }"); break;
                                        case "String": yaz.WriteLine("\t\tpublic string " + column.ColumnName + " { get; set; }"); break;
                                        case "Byte": yaz.WriteLine("\t\tpublic byte " + column.ColumnName + " { get; set; }"); break;
                                        case "Bytes": yaz.WriteLine("\t\tpublic byte[] " + column.ColumnName + " { get; set; }"); break;
                                        case "Boolean": yaz.WriteLine("\t\tpublic bool " + column.ColumnName + " { get; set; }"); break;
                                        case "DateTime": yaz.WriteLine("\t\tpublic DateTime " + column.ColumnName + " { get; set; }"); break;
                                        case "DateTimeOffset": yaz.WriteLine("\t\tpublic DateTimeOffset " + column.ColumnName + " { get; set; }"); break;
                                        case "TimeSpan": yaz.WriteLine("\t\tpublic TimeSpan " + column.ColumnName + " { get; set; }"); break;
                                        case "Single": yaz.WriteLine("\t\tpublic Single " + column.ColumnName + " { get; set; }"); break;
                                        case "Object": yaz.WriteLine("\t\tpublic object " + column.ColumnName + " { get; set; }"); break;
                                        case "Guid": yaz.WriteLine("\t\tpublic Guid " + column.ColumnName + " { get; set; }"); break;
                                        default: yaz.WriteLine("\t\tpublic string " + column.ColumnName + " { get; set; }"); break;
                                    }
                                }
                            }
                            else
                            {
                                yaz.WriteLine("\t\t//" + column.ColumnName + " isimli kolonun veri tipi bu programda tanumlı değil.");
                            }

                            i++;
                        }

                        yaz.WriteLine("");
                        yaz.WriteLine("\t\tpublic string Mesaj { get; set; }");

                        if (fileColumns.Count > 0 || imageColumns.Count > 0)
                        {
                            yaz.WriteLine("");

                            foreach (ColumnInfo item in fileColumns)
                            {
                                yaz.WriteLine("\t\tpublic string Old" + item.ColumnName + " { get; set; }");
                            }

                            foreach (ColumnInfo item in imageColumns)
                            {
                                yaz.WriteLine("\t\tpublic string Old" + item.ColumnName + " { get; set; }");
                            }
                        }

                        if (fileColumns.Count > 0 || imageColumns.Count > 0)
                        {
                            yaz.WriteLine("");

                            foreach (ColumnInfo item in fileColumns)
                            {
                                yaz.WriteLine("\t\tpublic bool " + item.ColumnName + "HasFile { get; set; }");
                            }

                            foreach (ColumnInfo item in imageColumns)
                            {
                                yaz.WriteLine("\t\tpublic bool " + item.ColumnName + "HasFile { get; set; }");
                            }
                        }

                        if (fkcList.Count > 0)
                        {
                            yaz.WriteLine("");

                            foreach (ForeignKeyChecker fkc in fkcList.GroupBy(a => a.ForeignTableName).Select(a => a.First()).ToList())
                            {
                                string ForeignTableName = fkc.ForeignTableName;
                                yaz.WriteLine("\t\tpublic List<I" + ForeignTableName + "> " + ForeignTableName + "List { get; set; }");
                            }
                        }

                        if (fkcListForeign.Count > 0)
                        {
                            yaz.WriteLine("");

                            foreach (ForeignKeyChecker fkc in fkcListForeign.GroupBy(a => a.PrimaryTableName).Select(a => a.First()).ToList())
                            {
                                string PrimaryTableName = fkc.PrimaryTableName;
                                yaz.WriteLine("\t\tpublic List<SelectListItem> " + PrimaryTableName + "List { get; set; }");
                            }

                            yaz.WriteLine("");

                            foreach (ForeignKeyChecker fkc in fkcListForeign.GroupBy(a => a.PrimaryTableName).Select(a => a.First()).ToList())
                            {
                                string PrimaryTableName = fkc.PrimaryTableName;
                                yaz.WriteLine("\t\tpublic string " + PrimaryTableName + "Adi { get; set; }");
                            }
                        }

                        yaz.WriteLine("");
                        yaz.WriteLine("\t\t#endregion");
                        yaz.WriteLine("");
                        yaz.WriteLine("\t\t#region Methods");
                        yaz.WriteLine("");

                        string searchText = GetColumnText(tableColumnInfos.Where(a => a.TableName == Table).ToList());
                        string linked = fkcListForeign.Count > 0 ? "Linked" : "";

                        // List
                        yaz.WriteLine("\t\tpublic List<" + Table + "> List(int? id = null, int? top = null, bool relation = true)");
                        yaz.WriteLine("\t\t{");
                        yaz.WriteLine("\t\t\tList<" + Table + "> table;");
                        yaz.WriteLine("");
                        yaz.WriteLine("\t\t\tList<usp_" + Table + linked + "Select_Result> tableTemp;");
                        yaz.WriteLine("\t\t\tList<usp_" + Table + "SelectTop_Result> tableTopTemp;");
                        yaz.WriteLine("");
                        yaz.WriteLine("\t\t\tif (top == null)");
                        yaz.WriteLine("\t\t\t{");
                        yaz.WriteLine("\t\t\t\ttableTemp = entity.usp_" + Table + linked + "Select(id).ToList();");
                        yaz.WriteLine("\t\t\t\ttable = tableTemp.ChangeModelList<" + Table + ", usp_" + Table + linked + "Select_Result>();");
                        yaz.WriteLine("\t\t\t}");
                        yaz.WriteLine("\t\t\telse");
                        yaz.WriteLine("\t\t\t{");
                        yaz.WriteLine("\t\t\t\ttableTopTemp = entity.usp_" + Table + "SelectTop(id, top).ToList();");
                        yaz.WriteLine("\t\t\t\ttable = tableTopTemp.ChangeModelList<" + Table + ", usp_" + Table + "SelectTop_Result>();");
                        yaz.WriteLine("\t\t\t}");
                        yaz.WriteLine("");

                        if (fkcListForeign.Count > 0 || fkcList.Count > 0)
                        {
                            yaz.WriteLine("\t\t\tif (relation)");
                            yaz.WriteLine("\t\t\t{");
                            yaz.WriteLine("\t\t\t\tforeach(" + Table + " item in table)");
                            yaz.WriteLine("\t\t\t\t{");
                        }

                        int j = 1;
                        if (fkcListForeign.Count > 0)
                        {
                            foreach (ForeignKeyChecker fkc in fkcListForeign.GroupBy(a => a.PrimaryTableName).Select(a => a.First()).ToList())
                            {
                                string PrimaryTableName = fkc.PrimaryTableName;
                                string columnText = GetColumnText(tableColumnInfos.Where(a => a.TableName == PrimaryTableName).ToList(), false);

                                yaz.WriteLine("\t\t\t\t\tList<usp_" + PrimaryTableName + "Select_Result> table" + PrimaryTableName + " = entity.usp_" + PrimaryTableName + "Select(null).ToList();");
                                yaz.WriteLine("\t\t\t\t\titem." + PrimaryTableName + "List = table" + PrimaryTableName + ".ToSelectList<usp_" + PrimaryTableName + "Select_Result, SelectListItem>(\"" + fkc.PrimaryColumnName + "\", \"" + columnText + "\", item." + fkc.ForeignColumnName + ");");

                                if (j < fkcListForeign.Count)
                                    yaz.WriteLine("");

                                j++;
                            }

                            if (fkcList.Count > 0)
                            {
                                yaz.WriteLine("");
                            }
                        }


                        if (fkcList.Count > 0)
                        {
                            foreach (ForeignKeyChecker fkc in fkcList.GroupBy(a => a.PrimaryTableName).Select(a => a.First()).ToList())
                            {
                                j = 1;
                                foreach (ForeignKeyChecker fkc2 in fkcList.GroupBy(a => a.ForeignTableName).Select(a => a.First()).ToList())
                                {
                                    string PrimaryTableName = fkc.PrimaryTableName;
                                    string ForeignTableName = fkc2.ForeignTableName;

                                    yaz.WriteLine("\t\t\t\t\tList<usp_" + ForeignTableName + "_" + PrimaryTableName + "ByLinkedIDSelect_Result> " + ForeignTableName.ToUrl(true) + "ModelList = entity.usp_" + ForeignTableName + "_" + PrimaryTableName + "ByLinkedIDSelect(item." + fkc.PrimaryColumnName + ").ToList();"); ;
                                    yaz.WriteLine("\t\t\t\t\titem." + ForeignTableName + "List.AddRange(" + ForeignTableName.ToUrl(true) + "ModelList.ChangeModelList<" + ForeignTableName + ", usp_" + ForeignTableName + "_" + PrimaryTableName + "ByLinkedIDSelect_Result>());");

                                    if (j < fkcList.Count)
                                        yaz.WriteLine("");

                                    j++;
                                }
                            }
                        }

                        if (fkcListForeign.Count > 0 || fkcList.Count > 0)
                        {
                            yaz.WriteLine("\t\t\t\t}");
                            yaz.WriteLine("\t\t\t}");
                            yaz.WriteLine("");
                        }

                        yaz.WriteLine("\t\t\treturn table;");
                        yaz.WriteLine("\t\t}");
                        yaz.WriteLine("");

                        // ListAll
                        yaz.WriteLine("\t\tpublic List<" + Table + "> ListAll(int? id = null, bool relation = true)");
                        yaz.WriteLine("\t\t{");
                        yaz.WriteLine("\t\t\tList<" + Table + "> table;");
                        yaz.WriteLine("");
                        yaz.WriteLine("\t\t\tList<usp_" + Table + "SelectAll_Result> tableTemp;");
                        yaz.WriteLine("");
                        yaz.WriteLine("\t\t\ttableTemp = entity.usp_" + Table + "SelectAll(id).ToList();");
                        yaz.WriteLine("\t\t\ttable = tableTemp.ChangeModelList<" + Table + ", usp_" + Table + "SelectAll_Result>();");
                        yaz.WriteLine("");

                        if (fkcListForeign.Count > 0 || fkcList.Count > 0)
                        {
                            yaz.WriteLine("\t\t\tif (relation)");
                            yaz.WriteLine("\t\t\t{");
                            yaz.WriteLine("\t\t\t\tforeach(" + Table + " item in table)");
                            yaz.WriteLine("\t\t\t\t{");
                        }

                        j = 1;
                        if (fkcListForeign.Count > 0)
                        {
                            foreach (ForeignKeyChecker fkc in fkcListForeign.GroupBy(a => a.PrimaryTableName).Select(a => a.First()).ToList())
                            {
                                string PrimaryTableName = fkc.PrimaryTableName;
                                string columnText = GetColumnText(tableColumnInfos.Where(a => a.TableName == PrimaryTableName).ToList(), false);

                                yaz.WriteLine("\t\t\t\t\tList<usp_" + PrimaryTableName + "Select_Result> table" + PrimaryTableName + " = entity.usp_" + PrimaryTableName + "Select(null).ToList();");
                                yaz.WriteLine("\t\t\t\t\titem." + PrimaryTableName + "List = table" + PrimaryTableName + ".ToSelectList<usp_" + PrimaryTableName + "Select_Result, SelectListItem>(\"" + fkc.PrimaryColumnName + "\", \"" + columnText + "\", item." + fkc.ForeignColumnName + ");");

                                if (j < fkcListForeign.Count)
                                    yaz.WriteLine("");

                                j++;
                            }

                            if (fkcList.Count > 0)
                            {
                                yaz.WriteLine("");
                            }
                        }

                        if (fkcList.Count > 0)
                        {
                            foreach (ForeignKeyChecker fkc in fkcList.GroupBy(a => a.PrimaryTableName).Select(a => a.First()).ToList())
                            {
                                j = 1;
                                foreach (ForeignKeyChecker fkc2 in fkcList.GroupBy(a => a.ForeignTableName).Select(a => a.First()).ToList())
                                {
                                    string PrimaryTableName = fkc.PrimaryTableName;
                                    string ForeignTableName = fkc2.ForeignTableName;

                                    yaz.WriteLine("\t\t\t\t\tList<usp_" + ForeignTableName + "_" + PrimaryTableName + "ByLinkedIDSelect_Result> " + ForeignTableName.ToUrl(true) + "ModelList = entity.usp_" + ForeignTableName + "_" + PrimaryTableName + "ByLinkedIDSelect(item." + fkc.PrimaryColumnName + ").ToList();"); ;
                                    yaz.WriteLine("\t\t\t\t\titem." + ForeignTableName + "List.AddRange(" + ForeignTableName.ToUrl(true) + "ModelList.ChangeModelList<" + ForeignTableName + ", usp_" + ForeignTableName + "_" + PrimaryTableName + "ByLinkedIDSelect_Result>());");

                                    if (j < fkcList.Count)
                                        yaz.WriteLine("");

                                    j++;
                                }
                            }
                        }

                        if (fkcListForeign.Count > 0 || fkcList.Count > 0)
                        {
                            yaz.WriteLine("\t\t\t\t}");
                            yaz.WriteLine("\t\t\t}");
                            yaz.WriteLine("");
                        }

                        yaz.WriteLine("\t\t\treturn table;");
                        yaz.WriteLine("\t\t}");
                        yaz.WriteLine("");

                        // Select
                        yaz.WriteLine("\t\tpublic I" + Table + " Select(int? id, bool relation = true)");
                        yaz.WriteLine("\t\t{");
                        yaz.WriteLine("\t\t\tusp_" + Table + "SelectTop_Result tableTemp = entity.usp_" + Table + "SelectTop(id, 1).FirstOrDefault();");
                        yaz.WriteLine("\t\t\t" + Table + " table = tableTemp.ChangeModel<" + Table + ">();");
                        yaz.WriteLine("");

                        if (fkcListForeign.Count > 0 || fkcList.Count > 0)
                        {
                            yaz.WriteLine("\t\t\tif (relation)");
                            yaz.WriteLine("\t\t\t{");
                        }

                        j = 1;
                        if (fkcListForeign.Count > 0)
                        {
                            foreach (ForeignKeyChecker fkc in fkcListForeign.GroupBy(a => a.PrimaryTableName).Select(a => a.First()).ToList())
                            {
                                string PrimaryTableName = fkc.PrimaryTableName;
                                string columnText = GetColumnText(tableColumnInfos.Where(a => a.TableName == PrimaryTableName).ToList(), false);

                                yaz.WriteLine("\t\t\t\tList<usp_" + PrimaryTableName + "Select_Result> table" + PrimaryTableName + " = entity.usp_" + PrimaryTableName + "Select(null).ToList();");
                                yaz.WriteLine("\t\t\t\ttable." + PrimaryTableName + "List = table" + PrimaryTableName + ".ToSelectList<usp_" + PrimaryTableName + "Select_Result, SelectListItem>(\"" + fkc.PrimaryColumnName + "\", \"" + columnText + "\", table." + fkc.ForeignColumnName + ");");

                                if (j < fkcListForeign.Count)
                                    yaz.WriteLine("");

                                j++;
                            }

                            if (fkcList.Count > 0)
                            {
                                yaz.WriteLine("");
                            }
                        }

                        if (fkcList.Count > 0)
                        {
                            foreach (ForeignKeyChecker fkc in fkcList.GroupBy(a => a.PrimaryTableName).Select(a => a.First()).ToList())
                            {
                                j = 1;
                                foreach (ForeignKeyChecker fkc2 in fkcList.GroupBy(a => a.ForeignTableName).Select(a => a.First()).ToList())
                                {
                                    string PrimaryTableName = fkc.PrimaryTableName;
                                    string ForeignTableName = fkc2.ForeignTableName;

                                    yaz.WriteLine("\t\t\t\tList<usp_" + ForeignTableName + "_" + PrimaryTableName + "ByLinkedIDSelect_Result> " + ForeignTableName.ToUrl(true) + "ModelList = entity.usp_" + ForeignTableName + "_" + PrimaryTableName + "ByLinkedIDSelect(id).ToList();"); ;
                                    yaz.WriteLine("\t\t\t\ttable." + ForeignTableName + "List.AddRange(" + ForeignTableName.ToUrl(true) + "ModelList.ChangeModelList<" + ForeignTableName + ", usp_" + ForeignTableName + "_" + PrimaryTableName + "ByLinkedIDSelect_Result>());");

                                    if (j < fkcList.Count)
                                        yaz.WriteLine("");

                                    j++;
                                }
                            }
                        }

                        if (fkcListForeign.Count > 0 || fkcList.Count > 0)
                        {
                            yaz.WriteLine("\t\t\t}");
                            yaz.WriteLine("");
                        }

                        yaz.WriteLine("\t\t\treturn table;");
                        yaz.WriteLine("\t\t}");
                        yaz.WriteLine("");

                        foreach (ColumnInfo item in urlColumns)
                        {
                            // SelectByUrl
                            yaz.WriteLine("\t\tpublic I" + Table + " SelectBy" + item.ColumnName + "(string url, bool relation = true)");
                            yaz.WriteLine("\t\t{");
                            yaz.WriteLine("\t\t\tusp_" + Table + "SelectBy" + item.ColumnName + "_Result tableTemp = entity.usp_" + Table + "SelectBy" + item.ColumnName + "(url).FirstOrDefault();");
                            yaz.WriteLine("\t\t\t" + Table + " table = tableTemp.ChangeModel<" + Table + ">();");
                            yaz.WriteLine("");

                            if (fkcListForeign.Count > 0 || fkcList.Count > 0)
                            {
                                yaz.WriteLine("\t\t\tif (relation)");
                                yaz.WriteLine("\t\t\t{");
                            }

                            j = 1;
                            if (fkcListForeign.Count > 0)
                            {
                                foreach (ForeignKeyChecker fkc in fkcListForeign.GroupBy(a => a.PrimaryTableName).Select(a => a.First()).ToList())
                                {
                                    string PrimaryTableName = fkc.PrimaryTableName;
                                    string columnText = GetColumnText(tableColumnInfos.Where(a => a.TableName == PrimaryTableName).ToList(), false);

                                    yaz.WriteLine("\t\t\t\tList<usp_" + PrimaryTableName + "Select_Result> table" + PrimaryTableName + " = entity.usp_" + PrimaryTableName + "Select(null).ToList();");
                                    yaz.WriteLine("\t\t\t\ttable." + PrimaryTableName + "List = table" + PrimaryTableName + ".ToSelectList<usp_" + PrimaryTableName + "Select_Result, SelectListItem>(\"" + fkc.PrimaryColumnName + "\", \"" + columnText + "\", table." + fkc.ForeignColumnName + ");");

                                    if (j < fkcListForeign.Count)
                                        yaz.WriteLine("");

                                    j++;
                                }

                                if (fkcList.Count > 0)
                                {
                                    yaz.WriteLine("");
                                }
                            }

                            if (fkcList.Count > 0)
                            {
                                foreach (ForeignKeyChecker fkc in fkcList.GroupBy(a => a.PrimaryTableName).Select(a => a.First()).ToList())
                                {
                                    j = 1;
                                    foreach (ForeignKeyChecker fkc2 in fkcList.GroupBy(a => a.ForeignTableName).Select(a => a.First()).ToList())
                                    {
                                        string PrimaryTableName = fkc.PrimaryTableName;
                                        string ForeignTableName = fkc2.ForeignTableName;

                                        yaz.WriteLine("\t\t\t\tList<usp_" + ForeignTableName + "_" + PrimaryTableName + "ByLinkedIDSelect_Result> " + ForeignTableName.ToUrl(true) + "ModelList = entity.usp_" + ForeignTableName + "_" + PrimaryTableName + "ByLinkedIDSelect(table." + fkc.PrimaryColumnName + ").ToList();"); ;
                                        yaz.WriteLine("\t\t\t\ttable." + ForeignTableName + "List.AddRange(" + ForeignTableName.ToUrl(true) + "ModelList.ChangeModelList<" + ForeignTableName + ", usp_" + ForeignTableName + "_" + PrimaryTableName + "ByLinkedIDSelect_Result>());");

                                        if (j < fkcList.Count)
                                            yaz.WriteLine("");

                                        j++;
                                    }
                                }
                            }

                            if (fkcListForeign.Count > 0 || fkcList.Count > 0)
                            {
                                yaz.WriteLine("\t\t\t}");
                                yaz.WriteLine("");
                            }

                            yaz.WriteLine("\t\t\treturn table;");
                            yaz.WriteLine("\t\t}");
                            yaz.WriteLine("");
                        }

                        foreach (ColumnInfo item in guidColumns)
                        {
                            // SelectByGuid
                            yaz.WriteLine("\t\tpublic I" + Table + " SelectBy" + item.ColumnName + "(string guid, bool relation = true)");
                            yaz.WriteLine("\t\t{");
                            yaz.WriteLine("\t\t\tusp_" + Table + "SelectBy" + item.ColumnName + "_Result tableTemp = entity.usp_" + Table + "SelectBy" + item.ColumnName + "(guid).FirstOrDefault();");
                            yaz.WriteLine("\t\t\t" + Table + " table = tableTemp.ChangeModel<" + Table + ">();");
                            yaz.WriteLine("");

                            if (fkcListForeign.Count > 0 || fkcList.Count > 0)
                            {
                                yaz.WriteLine("\t\t\tif (relation)");
                                yaz.WriteLine("\t\t\t{");
                            }

                            j = 1;
                            if (fkcListForeign.Count > 0)
                            {
                                foreach (ForeignKeyChecker fkc in fkcListForeign.GroupBy(a => a.PrimaryTableName).Select(a => a.First()).ToList())
                                {
                                    string PrimaryTableName = fkc.PrimaryTableName;
                                    string columnText = GetColumnText(tableColumnInfos.Where(a => a.TableName == PrimaryTableName).ToList(), false);

                                    yaz.WriteLine("\t\t\t\tList<usp_" + PrimaryTableName + "Select_Result> table" + PrimaryTableName + " = entity.usp_" + PrimaryTableName + "Select(null).ToList();");
                                    yaz.WriteLine("\t\t\t\ttable." + PrimaryTableName + "List = table" + PrimaryTableName + ".ToSelectList<usp_" + PrimaryTableName + "Select_Result, SelectListItem>(\"" + fkc.PrimaryColumnName + "\", \"" + columnText + "\", table." + fkc.ForeignColumnName + ");");

                                    if (j < fkcListForeign.Count)
                                        yaz.WriteLine("");

                                    j++;
                                }

                                if (fkcList.Count > 0)
                                {
                                    yaz.WriteLine("");
                                }
                            }

                            if (fkcList.Count > 0)
                            {
                                foreach (ForeignKeyChecker fkc in fkcList.GroupBy(a => a.PrimaryTableName).Select(a => a.First()).ToList())
                                {
                                    j = 1;
                                    foreach (ForeignKeyChecker fkc2 in fkcList.GroupBy(a => a.ForeignTableName).Select(a => a.First()).ToList())
                                    {
                                        string PrimaryTableName = fkc.PrimaryTableName;
                                        string ForeignTableName = fkc2.ForeignTableName;

                                        yaz.WriteLine("\t\t\t\tList<usp_" + ForeignTableName + "_" + PrimaryTableName + "ByLinkedIDSelect_Result> " + ForeignTableName.ToUrl(true) + "ModelList = entity.usp_" + ForeignTableName + "_" + PrimaryTableName + "ByLinkedIDSelect(table." + fkc.PrimaryColumnName + ").ToList();"); ;
                                        yaz.WriteLine("\t\t\t\ttable." + ForeignTableName + "List.AddRange(" + ForeignTableName.ToUrl(true) + "ModelList.ChangeModelList<" + ForeignTableName + ", usp_" + ForeignTableName + "_" + PrimaryTableName + "ByLinkedIDSelect_Result>());");

                                        if (j < fkcList.Count)
                                            yaz.WriteLine("");

                                        j++;
                                    }
                                }
                            }

                            if (fkcListForeign.Count > 0 || fkcList.Count > 0)
                            {
                                yaz.WriteLine("\t\t\t}");
                                yaz.WriteLine("");
                            }

                            yaz.WriteLine("\t\t\treturn table;");
                            yaz.WriteLine("\t\t}");
                            yaz.WriteLine("");
                        }

                        foreach (ColumnInfo item in codeColumns)
                        {
                            // SelectByCode
                            yaz.WriteLine("\t\tpublic List<" + Table + "> SelectBy" + item.ColumnName + "(string code, bool relation = true)");
                            yaz.WriteLine("\t\t{");
                            yaz.WriteLine("\t\t\tList<usp_" + Table + "SelectBy" + item.ColumnName + "_Result> tableTemp = entity.usp_" + Table + "SelectBy" + item.ColumnName + "(code).ToList();");
                            yaz.WriteLine("\t\t\tList<" + Table + "> table = tableTemp.ChangeModelList<" + Table + ", usp_" + Table + "SelectBy" + item.ColumnName + "_Result>();");
                            yaz.WriteLine("");

                            if (fkcListForeign.Count > 0 || fkcList.Count > 0)
                            {
                                yaz.WriteLine("\t\t\tif (relation)");
                                yaz.WriteLine("\t\t\t{");
                                yaz.WriteLine("\t\t\t\tforeach(" + Table + " item in table)");
                                yaz.WriteLine("\t\t\t\t{");
                            }

                            j = 1;
                            if (fkcListForeign.Count > 0)
                            {
                                foreach (ForeignKeyChecker fkc in fkcListForeign.GroupBy(a => a.PrimaryTableName).Select(a => a.First()).ToList())
                                {
                                    string PrimaryTableName = fkc.PrimaryTableName;
                                    string columnText = GetColumnText(tableColumnInfos.Where(a => a.TableName == PrimaryTableName).ToList(), false);

                                    yaz.WriteLine("\t\t\t\t\tList<usp_" + PrimaryTableName + "Select_Result> table" + PrimaryTableName + " = entity.usp_" + PrimaryTableName + "Select(null).ToList();");
                                    yaz.WriteLine("\t\t\t\t\titem." + PrimaryTableName + "List = table" + PrimaryTableName + ".ToSelectList<usp_" + PrimaryTableName + "Select_Result, SelectListItem>(\"" + fkc.PrimaryColumnName + "\", \"" + columnText + "\", item." + fkc.ForeignColumnName + ");");

                                    if (j < fkcListForeign.Count)
                                        yaz.WriteLine("");

                                    j++;
                                }

                                if (fkcList.Count > 0)
                                {
                                    yaz.WriteLine("");
                                }
                            }

                            if (fkcList.Count > 0)
                            {
                                foreach (ForeignKeyChecker fkc in fkcList.GroupBy(a => a.PrimaryTableName).Select(a => a.First()).ToList())
                                {
                                    j = 1;
                                    foreach (ForeignKeyChecker fkc2 in fkcList.GroupBy(a => a.ForeignTableName).Select(a => a.First()).ToList())
                                    {
                                        string PrimaryTableName = fkc.PrimaryTableName;
                                        string ForeignTableName = fkc2.ForeignTableName;

                                        yaz.WriteLine("\t\t\t\t\tList<usp_" + ForeignTableName + "_" + PrimaryTableName + "ByLinkedIDSelect_Result> " + ForeignTableName.ToUrl(true) + "ModelList = entity.usp_" + ForeignTableName + "_" + PrimaryTableName + "ByLinkedIDSelect(item." + fkc.PrimaryColumnName + ").ToList();"); ;
                                        yaz.WriteLine("\t\t\t\t\titem." + ForeignTableName + "List.AddRange(" + ForeignTableName.ToUrl(true) + "ModelList.ChangeModelList<" + ForeignTableName + ", usp_" + ForeignTableName + "_" + PrimaryTableName + "ByLinkedIDSelect_Result>());");

                                        if (j < fkcList.Count)
                                            yaz.WriteLine("");

                                        j++;
                                    }
                                }
                            }

                            if (fkcListForeign.Count > 0 || fkcList.Count > 0)
                            {
                                yaz.WriteLine("\t\t\t\t}");
                                yaz.WriteLine("\t\t\t}");
                                yaz.WriteLine("");
                            }

                            yaz.WriteLine("\t\t\treturn table;");
                            yaz.WriteLine("\t\t}");
                            yaz.WriteLine("");
                        }

                        foreach (ColumnInfo item in searchColumns)
                        {
                            // SelectBySearch
                            yaz.WriteLine("\t\tpublic List<" + Table + "> SelectBy" + item.ColumnName + "(string searchText, bool relation = true)");
                            yaz.WriteLine("\t\t{");
                            yaz.WriteLine("\t\t\tList<usp_" + Table + "SelectBy" + item.ColumnName + "_Result> tableTemp = entity.usp_" + Table + "SelectBy" + item.ColumnName + "(searchText).ToList();");
                            yaz.WriteLine("\t\t\tList<" + Table + "> table = tableTemp.ChangeModelList<" + Table + ", usp_" + Table + "SelectBy" + item.ColumnName + "_Result>();");
                            yaz.WriteLine("");

                            if (fkcListForeign.Count > 0 || fkcList.Count > 0)
                            {
                                yaz.WriteLine("\t\t\tif (relation)");
                                yaz.WriteLine("\t\t\t{");
                                yaz.WriteLine("\t\t\t\tforeach(" + Table + " item in table)");
                                yaz.WriteLine("\t\t\t\t{");
                            }

                            j = 1;
                            if (fkcListForeign.Count > 0)
                            {
                                foreach (ForeignKeyChecker fkc in fkcListForeign.GroupBy(a => a.PrimaryTableName).Select(a => a.First()).ToList())
                                {
                                    string PrimaryTableName = fkc.PrimaryTableName;
                                    string columnText = GetColumnText(tableColumnInfos.Where(a => a.TableName == PrimaryTableName).ToList(), false);

                                    yaz.WriteLine("\t\t\t\t\tList<usp_" + PrimaryTableName + "Select_Result> table" + PrimaryTableName + " = entity.usp_" + PrimaryTableName + "Select(null).ToList();");
                                    yaz.WriteLine("\t\t\t\t\titem." + PrimaryTableName + "List = table" + PrimaryTableName + ".ToSelectList<usp_" + PrimaryTableName + "Select_Result, SelectListItem>(\"" + fkc.PrimaryColumnName + "\", \"" + columnText + "\", item." + fkc.ForeignColumnName + ");");

                                    if (j < fkcListForeign.Count)
                                        yaz.WriteLine("");

                                    j++;
                                }

                                if (fkcList.Count > 0)
                                {
                                    yaz.WriteLine("");
                                }
                            }

                            if (fkcList.Count > 0)
                            {
                                foreach (ForeignKeyChecker fkc in fkcList.GroupBy(a => a.PrimaryTableName).Select(a => a.First()).ToList())
                                {
                                    j = 1;
                                    foreach (ForeignKeyChecker fkc2 in fkcList.GroupBy(a => a.ForeignTableName).Select(a => a.First()).ToList())
                                    {
                                        string PrimaryTableName = fkc.PrimaryTableName;
                                        string ForeignTableName = fkc2.ForeignTableName;

                                        yaz.WriteLine("\t\t\t\t\tList<usp_" + ForeignTableName + "_" + PrimaryTableName + "ByLinkedIDSelect_Result> " + ForeignTableName.ToUrl(true) + "ModelList = entity.usp_" + ForeignTableName + "_" + PrimaryTableName + "ByLinkedIDSelect(item." + fkc.PrimaryColumnName + ").ToList();"); ;
                                        yaz.WriteLine("\t\t\t\t\titem." + ForeignTableName + "List.AddRange(" + ForeignTableName.ToUrl(true) + "ModelList.ChangeModelList<" + ForeignTableName + ", usp_" + ForeignTableName + "_" + PrimaryTableName + "ByLinkedIDSelect_Result>());");

                                        if (j < fkcList.Count)
                                            yaz.WriteLine("");

                                        j++;
                                    }
                                }
                            }

                            if (fkcListForeign.Count > 0 || fkcList.Count > 0)
                            {
                                yaz.WriteLine("\t\t\t\t}");
                                yaz.WriteLine("\t\t\t}");
                                yaz.WriteLine("");
                            }

                            yaz.WriteLine("\t\t\treturn table;");
                            yaz.WriteLine("\t\t}");
                            yaz.WriteLine("");
                        }

                        // Insert
                        string linkID = ", bool? none = null";
                        string[] links = new string[fkcListForeign.Count];

                        int l = 0;
                        if (fkcListForeign.Count > 0)
                        {
                            linkID = "";

                            foreach (ForeignKeyChecker fkc in fkcListForeign.GroupBy(a => a.PrimaryTableName).Select(a => a.First()).ToList())
                            {
                                linkID += ", int? " + fkc.ForeignColumnName.FirstCharToLowerCase() + " = null";
                                links[l] = fkc.ForeignColumnName.FirstCharToLowerCase();

                                l++;
                            }
                        }

                        yaz.WriteLine("\t\tpublic I" + Table + " Insert(I" + Table + " table = null" + linkID + ")");
                        yaz.WriteLine("\t\t{");
                        yaz.WriteLine("\t\t\tif (table == null)");
                        yaz.WriteLine("\t\t\t\ttable = new " + Table + "();");
                        yaz.WriteLine("");

                        l = 0;
                        if (fkcListForeign.Count > 0)
                        {
                            foreach (ForeignKeyChecker fkc in fkcListForeign.GroupBy(a => a.PrimaryTableName).Select(a => a.First()).ToList())
                            {
                                string PrimaryTableName = fkc.PrimaryTableName;
                                string columnText = GetColumnText(tableColumnInfos.Where(a => a.TableName == PrimaryTableName).ToList(), false);

                                yaz.WriteLine("\t\t\tList<usp_" + PrimaryTableName + "Select_Result> table" + PrimaryTableName + " = entity.usp_" + PrimaryTableName + "Select(null).ToList();");
                                yaz.WriteLine("\t\t\ttable." + PrimaryTableName + "List = table" + PrimaryTableName + ".ToSelectList<usp_" + PrimaryTableName + "Select_Result, SelectListItem>(\"" + fkc.PrimaryColumnName + "\",  \"" + columnText + "\", " + links[l] + ");");
                                yaz.WriteLine("");

                                l++;
                            }
                        }

                        yaz.WriteLine("\t\t\treturn table;");
                        yaz.WriteLine("\t\t}");
                        yaz.WriteLine("");

                        // Insert
                        yaz.WriteLine("\t\tpublic bool Insert(I" + Table + " table)");
                        yaz.WriteLine("\t\t{");

                        if (urlColumns.Count > 0)
                        {
                            foreach (ColumnInfo item in urlColumns)
                            {
                                yaz.WriteLine("\t\t\ttable." + item.ColumnName + " = table." + searchText + ".ToUrl();");
                            }

                            yaz.WriteLine("");
                        }

                        if (guidColumns.Count > 0)
                        {
                            foreach (ColumnInfo item in guidColumns)
                            {
                                if (item.CharLength > 0)
                                    yaz.WriteLine("\t\t\ttable." + item.ColumnName + " = Guider.GetGuid(" + item.CharLength + ");");
                            }

                            yaz.WriteLine("");
                        }

                        string insertSql = "var result = entity.usp_" + Table + "Insert(";
                        foreach (ColumnInfo column in tableColumnInfos.Where(a => a.TableName == Table).ToList())
                        {
                            if (!column.IsIdentity)
                            {
                                if (!column.ColumnName.In(DeletedColumns, InType.ToUrlLower))
                                    insertSql += "table." + column.ColumnName + ", ";
                            }
                        }
                        insertSql = insertSql.TrimEnd(' ').TrimEnd(',');
                        insertSql += ").FirstOrDefault();";

                        yaz.WriteLine("\t\t\t" + insertSql);

                        yaz.WriteLine("");
                        yaz.WriteLine("\t\t\tif(result != null)");
                        yaz.WriteLine("\t\t\t\treturn true;");
                        yaz.WriteLine("\t\t\telse");
                        yaz.WriteLine("\t\t\t\treturn false;");
                        yaz.WriteLine("\t\t}");
                        yaz.WriteLine("");

                        if (identityColumns.Count > 0)
                        {
                            //Update
                            yaz.WriteLine("\t\tpublic I" + Table + " Update(int? id = null, I" + Table + " table = null)");
                            yaz.WriteLine("\t\t{");
                            yaz.WriteLine("\t\t\tif (table == null)");
                            yaz.WriteLine("\t\t\t{");
                            yaz.WriteLine("\t\t\t\ttable = Select(id);");
                            yaz.WriteLine("\t\t\t}");

                            if (fkcListForeign.Count > 0 || fkcList.Count > 0)
                            {
                                yaz.WriteLine("\t\t\telse");
                                yaz.WriteLine("\t\t\t{");
                            }

                            if (fkcListForeign.Count > 0)
                            {
                                l = 1;

                                foreach (ForeignKeyChecker fkc in fkcListForeign.GroupBy(a => a.PrimaryTableName).Select(a => a.First()).ToList())
                                {
                                    if (l > 1)
                                    {
                                        yaz.WriteLine("");
                                    }

                                    string PrimaryTableName = fkc.PrimaryTableName;
                                    string columnText = GetColumnText(tableColumnInfos.Where(a => a.TableName == PrimaryTableName).ToList(), false);

                                    yaz.WriteLine("\t\t\t\tList<usp_" + PrimaryTableName + "Select_Result> table" + PrimaryTableName + " = entity.usp_" + PrimaryTableName + "Select(null).ToList();");
                                    yaz.WriteLine("\t\t\t\ttable." + PrimaryTableName + "List = table" + PrimaryTableName + ".ToSelectList<usp_" + PrimaryTableName + "Select_Result, SelectListItem>(\"" + fkc.PrimaryColumnName + "\", \"" + columnText + "\", table." + fkc.ForeignColumnName + ");");

                                    l++;

                                    if (fkcList.Count > 0)
                                    {
                                        yaz.WriteLine("");
                                    }
                                }
                            }

                            if (fkcList.Count > 0)
                            {
                                l = 1;
                                foreach (ForeignKeyChecker fkc in fkcList.GroupBy(a => a.PrimaryTableName).Select(a => a.First()).ToList())
                                {
                                    foreach (ForeignKeyChecker fkc2 in fkcList.GroupBy(a => a.ForeignTableName).Select(a => a.First()).ToList())
                                    {
                                        string PrimaryTableName = fkc.PrimaryTableName;
                                        string ForeignTableName = fkc2.ForeignTableName;

                                        yaz.WriteLine("\t\t\t\tList<usp_" + ForeignTableName + "_" + PrimaryTableName + "ByLinkedIDSelect_Result> " + ForeignTableName.ToUrl(true) + "ModelList = entity.usp_" + ForeignTableName + "_" + PrimaryTableName + "ByLinkedIDSelect(table." + id + ").ToList();"); ;
                                        yaz.WriteLine("\t\t\t\ttable." + ForeignTableName + "List.AddRange(" + ForeignTableName.ToUrl(true) + "ModelList.ChangeModelList<" + ForeignTableName + ", usp_" + ForeignTableName + "_" + PrimaryTableName + "ByLinkedIDSelect_Result>());");

                                        if (l != fkcList.Count)
                                            yaz.WriteLine("");
                                    }

                                    l++;
                                }
                            }

                            if (fkcListForeign.Count > 0 || fkcList.Count > 0)
                            {
                                yaz.WriteLine("\t\t\t}");
                            }

                            yaz.WriteLine("");

                            yaz.WriteLine("\t\t\treturn table;");
                            yaz.WriteLine("\t\t}");
                            yaz.WriteLine("");


                            //Update
                            yaz.WriteLine("\t\tpublic bool Update(I" + Table + " table)");
                            yaz.WriteLine("\t\t{");

                            string columntype = tableColumnInfos.Where(a => a.ColumnName == id && a.TableName == Table).FirstOrDefault().Type.Name.ToString();

                            if (urlColumns.Count > 0)
                            {
                                foreach (ColumnInfo item in urlColumns)
                                {
                                    yaz.WriteLine("\t\t\ttable." + item.ColumnName + " = table." + searchText + ".ToUrl();");
                                }

                                yaz.WriteLine("");
                            }

                            string updateSql = "var result = entity.usp_" + Table + "Update(";
                            foreach (ColumnInfo column in tableColumnInfos.Where(a => a.TableName == Table).ToList())
                            {
                                if (!column.ColumnName.In(DeletedColumns, InType.ToUrlLower) && !column.ColumnName.In(GuidColumns, InType.ToUrlLower))
                                    updateSql += "table." + column.ColumnName + ", ";
                            }
                            updateSql = updateSql.TrimEnd(' ').TrimEnd(',');
                            updateSql += ").FirstOrDefault();";

                            yaz.WriteLine("\t\t\t" + updateSql);
                            yaz.WriteLine("");

                            yaz.WriteLine("\t\t\tif(result != null)");
                            yaz.WriteLine("\t\t\t\treturn true;");
                            yaz.WriteLine("\t\t\telse");
                            yaz.WriteLine("\t\t\t\treturn false;");
                            yaz.WriteLine("\t\t}");

                            //Copy
                            yaz.WriteLine("");
                            yaz.WriteLine("\t\tpublic bool Copy(" + columntype.ReturnCSharpType() + " id)");
                            yaz.WriteLine("\t\t{");
                            yaz.WriteLine("\t\t\ttry");
                            yaz.WriteLine("\t\t\t{");
                            yaz.WriteLine("\t\t\t\tvar result = entity.usp_" + Table + "Copy(id).FirstOrDefault();");
                            yaz.WriteLine("");
                            yaz.WriteLine("\t\t\t\treturn result == null ? false : true;");
                            yaz.WriteLine("\t\t\t}");
                            yaz.WriteLine("\t\t\tcatch");
                            yaz.WriteLine("\t\t\t{");
                            yaz.WriteLine("\t\t\t\treturn false;");
                            yaz.WriteLine("\t\t\t}");
                            yaz.WriteLine("\t\t}");

                            //Delete
                            yaz.WriteLine("");
                            yaz.WriteLine("\t\tpublic bool Delete(" + columntype.ReturnCSharpType() + "? id = null)");
                            yaz.WriteLine("\t\t{");
                            yaz.WriteLine("\t\t\ttry");
                            yaz.WriteLine("\t\t\t{");
                            yaz.WriteLine("\t\t\t\tentity.usp_" + Table + "Delete(id);");
                            yaz.WriteLine("");
                            yaz.WriteLine("\t\t\t\treturn true;");
                            yaz.WriteLine("\t\t\t}");
                            yaz.WriteLine("\t\t\tcatch");
                            yaz.WriteLine("\t\t\t{");
                            yaz.WriteLine("\t\t\t\treturn false;");
                            yaz.WriteLine("\t\t\t}");
                            yaz.WriteLine("\t\t}");

                            if (deleted)
                            {
                                //Remove
                                yaz.WriteLine("");
                                yaz.WriteLine("\t\tpublic bool Remove(" + columntype.ReturnCSharpType() + "? id = null)");
                                yaz.WriteLine("\t\t{");
                                yaz.WriteLine("\t\t\ttry");
                                yaz.WriteLine("\t\t\t{");
                                yaz.WriteLine("\t\t\t\tentity.usp_" + Table + "Remove(id);");
                                yaz.WriteLine("");
                                yaz.WriteLine("\t\t\t\treturn true;");
                                yaz.WriteLine("\t\t\t}");
                                yaz.WriteLine("\t\t\tcatch");
                                yaz.WriteLine("\t\t\t{");
                                yaz.WriteLine("\t\t\t\treturn false;");
                                yaz.WriteLine("\t\t\t}");
                                yaz.WriteLine("\t\t}");
                            }
                        }

                        yaz.WriteLine("");
                        yaz.WriteLine("\t\t#endregion");

                        yaz.WriteLine("");
                        yaz.WriteLine("\t\t#region User Defined");
                        yaz.WriteLine("");
                        yaz.WriteLine("");
                        yaz.WriteLine("");
                        yaz.WriteLine("\t\t#endregion");

                        yaz.WriteLine("\t}");
                        yaz.WriteLine("}");
                        yaz.Close();
                    }
                }

                //Interface
                using (FileStream fs = new FileStream(PathAddress + "\\" + projectFolder + "\\" + repositoryName + "\\Models\\" + Table + "\\I" + Table + ".cs", FileMode.Create))
                {
                    using (StreamWriter yaz = new StreamWriter(fs, Encoding.Unicode))
                    {
                        List<ColumnInfo> columnNames = TableColumns(Table);
                        List<ColumnInfo> urlColumns = TableColumns(Table, ColumnType.UrlColumns);
                        List<ColumnInfo> guidColumns = TableColumns(Table, ColumnType.GuidColumns);
                        List<ColumnInfo> codeColumns = TableColumns(Table, ColumnType.CodeColumns);
                        List<ColumnInfo> fileColumns = TableColumns(Table, ColumnType.FileColumns);
                        List<ColumnInfo> imageColumns = TableColumns(Table, ColumnType.ImageColumns);
                        List<ColumnInfo> searchColumns = TableColumns(Table, ColumnType.SearchColumns);
                        bool deleted = TableColumns(Table, ColumnType.DeletedColumns).Count > 0 ? true : false;

                        yaz.WriteLine("using System;");
                        yaz.WriteLine("using System.Collections.Generic;");
                        if (fkcListForeign.Count > 0)
                        {
                            yaz.WriteLine("using System.Web.Mvc;");
                        }

                        if (fkcList.Count > 0)
                        {
                            foreach (ForeignKeyChecker fkc in fkcList.GroupBy(a => a.ForeignTableName).Select(a => a.First()).ToList())
                            {
                                string ForeignTableName = fkc.ForeignTableName;
                                yaz.WriteLine("using " + repositoryName + "." + ForeignTableName + "Model;");

                            }
                        }

                        yaz.WriteLine("");

                        yaz.WriteLine("namespace " + repositoryName + "." + Table + "Model");
                        yaz.WriteLine("{");

                        yaz.WriteLine("\tpublic interface I" + Table + "");
                        yaz.WriteLine("\t{");

                        yaz.WriteLine("\t\t#region Model");
                        yaz.WriteLine("");

                        int counter = columnNames.Count;
                        int i = 1;

                        foreach (ColumnInfo column in columnNames)
                        {
                            if (column.Type != null)
                            {
                                if (column.IsNullable)
                                {
                                    switch (column.Type.Name)
                                    {
                                        case "Int16": yaz.WriteLine("\t\tint? " + column.ColumnName + " { get; set; }"); break;
                                        case "Int32": yaz.WriteLine("\t\tint? " + column.ColumnName + " { get; set; }"); break;
                                        case "Int64": yaz.WriteLine("\t\tInt64? " + column.ColumnName + " { get; set; }"); break;
                                        case "Decimal": yaz.WriteLine("\t\tdecimal? " + column.ColumnName + " { get; set; }"); break;
                                        case "Double": yaz.WriteLine("\t\tdouble? " + column.ColumnName + " { get; set; }"); break;
                                        case "Char": yaz.WriteLine("\t\tchar " + column.ColumnName + " { get; set; }"); break;
                                        case "Chars": yaz.WriteLine("\t\tchar[] " + column.ColumnName + " { get; set; }"); break;
                                        case "String": yaz.WriteLine("\t\tstring " + column.ColumnName + " { get; set; }"); break;
                                        case "Byte": yaz.WriteLine("\t\tbyte " + column.ColumnName + " { get; set; }"); break;
                                        case "Bytes": yaz.WriteLine("\t\tbyte[] " + column.ColumnName + " { get; set; }"); break;
                                        case "Boolean": yaz.WriteLine("\t\tbool? " + column.ColumnName + " { get; set; }"); break;
                                        case "DateTime": yaz.WriteLine("\t\tDateTime? " + column.ColumnName + " { get; set; }"); break;
                                        case "DateTimeOffset": yaz.WriteLine("\t\tDateTimeOffset? " + column.ColumnName + " { get; set; }"); break;
                                        case "TimeSpan": yaz.WriteLine("\t\tTimeSpan? " + column.ColumnName + " { get; set; }"); break;
                                        case "Single": yaz.WriteLine("\t\tSingle? " + column.ColumnName + " { get; set; }"); break;
                                        case "Object": yaz.WriteLine("\t\tobject " + column.ColumnName + " { get; set; }"); break;
                                        case "Guid": yaz.WriteLine("\t\tGuid? " + column.ColumnName + " { get; set; }"); break;
                                        default: yaz.WriteLine("\t\tstring " + column.ColumnName + " { get; set; }"); break;
                                    }
                                }
                                else
                                {
                                    switch (column.Type.Name)
                                    {
                                        case "Int16": yaz.WriteLine("\t\tint " + column.ColumnName + " { get; set; }"); break;
                                        case "Int32": yaz.WriteLine("\t\tint " + column.ColumnName + " { get; set; }"); break;
                                        case "Int64": yaz.WriteLine("\t\tInt64 " + column.ColumnName + " { get; set; }"); break;
                                        case "Decimal": yaz.WriteLine("\t\tdecimal " + column.ColumnName + " { get; set; }"); break;
                                        case "Double": yaz.WriteLine("\t\tdouble " + column.ColumnName + " { get; set; }"); break;
                                        case "Char": yaz.WriteLine("\t\tchar " + column.ColumnName + " { get; set; }"); break;
                                        case "Chars": yaz.WriteLine("\t\tchar[] " + column.ColumnName + " { get; set; }"); break;
                                        case "String": yaz.WriteLine("\t\tstring " + column.ColumnName + " { get; set; }"); break;
                                        case "Byte": yaz.WriteLine("\t\tbyte " + column.ColumnName + " { get; set; }"); break;
                                        case "Bytes": yaz.WriteLine("\t\tbyte[] " + column.ColumnName + " { get; set; }"); break;
                                        case "Boolean": yaz.WriteLine("\t\tbool " + column.ColumnName + " { get; set; }"); break;
                                        case "DateTime": yaz.WriteLine("\t\tDateTime " + column.ColumnName + " { get; set; }"); break;
                                        case "DateTimeOffset": yaz.WriteLine("\t\tDateTimeOffset " + column.ColumnName + " { get; set; }"); break;
                                        case "TimeSpan": yaz.WriteLine("\t\tTimeSpan " + column.ColumnName + " { get; set; }"); break;
                                        case "Single": yaz.WriteLine("\t\tSingle " + column.ColumnName + " { get; set; }"); break;
                                        case "Object": yaz.WriteLine("\t\tobject " + column.ColumnName + " { get; set; }"); break;
                                        case "Guid": yaz.WriteLine("\t\tGuid " + column.ColumnName + " { get; set; }"); break;
                                        default: yaz.WriteLine("\t\tstring " + column.ColumnName + " { get; set; }"); break;
                                    }
                                }
                            }
                            else
                            {
                                yaz.WriteLine("\t\t//" + column.ColumnName + " isimli kolonun veri tipi bu programda tanumlı değil.");
                            }

                            i++;
                        }

                        yaz.WriteLine("");
                        yaz.WriteLine("\t\tstring Mesaj { get; set; }");

                        if (fileColumns.Count > 0 || imageColumns.Count > 0)
                        {
                            yaz.WriteLine("");

                            foreach (ColumnInfo item in fileColumns)
                            {
                                yaz.WriteLine("\t\tstring Old" + item.ColumnName + " { get; set; }");
                            }

                            foreach (ColumnInfo item in imageColumns)
                            {
                                yaz.WriteLine("\t\tstring Old" + item.ColumnName + " { get; set; }");
                            }
                        }


                        if (fileColumns.Count > 0 || imageColumns.Count > 0)
                        {
                            yaz.WriteLine("");

                            foreach (ColumnInfo item in fileColumns)
                            {
                                yaz.WriteLine("\t\tbool " + item.ColumnName + "HasFile { get; set; }");
                            }

                            foreach (ColumnInfo item in imageColumns)
                            {
                                yaz.WriteLine("\t\tbool " + item.ColumnName + "HasFile { get; set; }");
                            }
                        }

                        if (fkcList.Count > 0)
                        {
                            yaz.WriteLine("");

                            foreach (ForeignKeyChecker fkc in fkcList.GroupBy(a => a.ForeignTableName).Select(a => a.First()).ToList())
                            {
                                string ForeignTableName = fkc.ForeignTableName;
                                yaz.WriteLine("\t\tList<I" + ForeignTableName + "> " + ForeignTableName + "List { get; set; }");
                            }
                        }

                        if (fkcListForeign.Count > 0)
                        {
                            yaz.WriteLine("");

                            foreach (ForeignKeyChecker fkc in fkcListForeign.GroupBy(a => a.PrimaryTableName).Select(a => a.First()).ToList())
                            {
                                string PrimaryTableName = fkc.PrimaryTableName;
                                yaz.WriteLine("\t\tList<SelectListItem> " + PrimaryTableName + "List { get; set; }");
                            }

                            yaz.WriteLine("");

                            foreach (ForeignKeyChecker fkc in fkcListForeign.GroupBy(a => a.PrimaryTableName).Select(a => a.First()).ToList())
                            {
                                string PrimaryTableName = fkc.PrimaryTableName;
                                yaz.WriteLine("\t\tstring " + PrimaryTableName + "Adi { get; set; }");
                            }
                        }

                        yaz.WriteLine("");
                        yaz.WriteLine("\t\t#endregion");
                        yaz.WriteLine("");
                        yaz.WriteLine("\t\t#region Methods");
                        yaz.WriteLine("");

                        string searchText = GetColumnText(tableColumnInfos.Where(a => a.TableName == Table).ToList());

                        // List
                        yaz.WriteLine("\t\tList<" + Table + "> List(int? id, int? top, bool relation);");

                        // ListAll
                        yaz.WriteLine("\t\tList<" + Table + "> ListAll(int? id, bool relation);");

                        // Select
                        yaz.WriteLine("\t\tI" + Table + " Select(int? id, bool relation);");

                        foreach (ColumnInfo item in urlColumns)
                        {
                            // SelectByUrl
                            yaz.WriteLine("\t\tI" + Table + " SelectBy" + item.ColumnName + "(string url, bool relation);");
                        }

                        foreach (ColumnInfo item in guidColumns)
                        {
                            // SelectByGuid
                            yaz.WriteLine("\t\tI" + Table + " SelectBy" + item.ColumnName + "(string guid, bool relation);");
                        }

                        foreach (ColumnInfo item in codeColumns)
                        {
                            // SelectByCode
                            yaz.WriteLine("\t\tList<" + Table + "> SelectBy" + item.ColumnName + "(string code, bool relation);");
                        }

                        foreach (ColumnInfo item in searchColumns)
                        {
                            // SelectBySearch
                            yaz.WriteLine("\t\tList<" + Table + "> SelectBy" + item.ColumnName + "(string searchText, bool relation);");
                        }

                        // Insert
                        string linkID = ", bool? none";
                        string[] links = new string[fkcListForeign.Count];

                        int l = 0;
                        if (fkcListForeign.Count > 0)
                        {
                            linkID = "";

                            foreach (ForeignKeyChecker fkc in fkcListForeign.GroupBy(a => a.PrimaryTableName).Select(a => a.First()).ToList())
                            {
                                linkID += ", int? " + fkc.ForeignColumnName.FirstCharToLowerCase();
                                links[l] = fkc.ForeignColumnName.FirstCharToLowerCase();

                                l++;
                            }
                        }

                        yaz.WriteLine("\t\tI" + Table + " Insert(I" + Table + " table" + linkID + ");");

                        yaz.WriteLine("\t\tbool Insert(I" + Table + " table);");

                        if (identityColumns.Count > 0)
                        {
                            string columntype = tableColumnInfos.Where(a => a.ColumnName == id && a.TableName == Table).FirstOrDefault().Type.Name.ToString();

                            //Update
                            yaz.WriteLine("\t\tI" + Table + " Update(int? id, I" + Table + " table);");
                            yaz.WriteLine("\t\tbool Update(I" + Table + " table);");

                            //Copy
                            yaz.WriteLine("\t\tbool Copy(" + columntype.ReturnCSharpType() + " id);");

                            //Delete
                            yaz.WriteLine("\t\tbool Delete(" + columntype.ReturnCSharpType() + "? id);");

                            if (deleted)
                            {
                                //Remove
                                yaz.WriteLine("\t\tbool Remove(" + columntype.ReturnCSharpType() + "? id);");
                            }
                        }

                        yaz.WriteLine("");
                        yaz.WriteLine("\t\t#endregion");

                        yaz.WriteLine("\t}");
                        yaz.WriteLine("}");
                        yaz.Close();
                    }
                }
            }
        }

        void CreateWebConfig()
        {
            string wcKullanici = String.IsNullOrEmpty(txtWCKullanici.Text) ? "user" : txtWCKullanici.Text;
            string wcSifre = String.IsNullOrEmpty(txtWCSifre.Text) ? "123456" : txtWCSifre.Text;

            using (FileStream fs = new FileStream(PathAddress + "\\" + projectFolder + "\\Web.config.txt", FileMode.Create))
            {
                using (StreamWriter yaz = new StreamWriter(fs, Encoding.Unicode))
                {
                    yaz.WriteLine("<add key=\"SystemUser\" value=\"admin\" />");
                    yaz.WriteLine("<add key=\"MainPath\" value=\"http://localhost/" + projectName + "\" />");
                    yaz.WriteLine("<add key=\"ScriptPath\" value=\"/Content/js\" />");
                    yaz.WriteLine("<add key=\"StylePath\" value=\"/Content/css\" />");
                    yaz.WriteLine("<add key=\"ImagePath\" value=\"/Content/img\" />");
                    yaz.WriteLine("<add key=\"AjaxPath\" value=\"/Ajax\" />");
                    yaz.WriteLine("<add key=\"AdminPath\" value=\"http://localhost/" + projectName + "/Admin\" />");
                    yaz.WriteLine("<add key=\"AdminScriptPath\" value=\"/Content/admin/js\" />");
                    yaz.WriteLine("<add key=\"AdminStylePath\" value=\"/Content/admin/css\" />");
                    yaz.WriteLine("<add key=\"AdminImagePath\" value=\"/Content/admin/img\" />");
                    yaz.WriteLine("<add key=\"AdminAjaxPath\" value=\"/Ajax/Ajax\" />");
                    yaz.WriteLine("<add key=\"UploadPath\" value=\"/Uploads\" />");
                    yaz.WriteLine("<add key=\"MaxFileSize\" value=\"1024000\" />");
                    yaz.WriteLine("<add key=\"MaxPictureSize\" value=\"1024000\" />");
                    yaz.WriteLine("");
                    yaz.WriteLine("<system.webServer>");
                    yaz.WriteLine("\t<validation validateIntegratedModeConfiguration=\"false\"/>");
                    yaz.WriteLine("\t<modules runAllManagedModulesForAllRequests=\"true\"/>");
                    yaz.WriteLine("</system.webServer>");

                    if (chkMVCWcfServis.Checked || chkMVCHepsi.Checked)
                    {
                        yaz.WriteLine("");
                        yaz.WriteLine("<system.serviceModel>");
                        yaz.WriteLine("\t<behaviors>");
                        yaz.WriteLine("\t\t<serviceBehaviors>");
                        yaz.WriteLine("\t\t\t<behavior name=\"\">");
                        yaz.WriteLine("\t\t\t\t<serviceMetadata httpGetEnabled=\"true\" />");
                        yaz.WriteLine("\t\t\t\t<serviceDebug includeExceptionDetailInFaults=\"false\" />");
                        yaz.WriteLine("\t\t\t</behavior>");
                        yaz.WriteLine("\t\t</serviceBehaviors>");
                        yaz.WriteLine("\t</behaviors>");
                        yaz.WriteLine("\t<serviceHostingEnvironment multipleSiteBindingsEnabled=\"true\" minFreeMemoryPercentageToActivateService=\"0\" />");
                        yaz.WriteLine("\t<services>");

                        foreach (string table in selectedTables)
                        {
                            yaz.WriteLine("\t\t<service name=\"" + projectName + ".Service." + table + "Service\">");
                            yaz.WriteLine("\t\t\t<endpoint kind=\"webHttpEndpoint\" contract=\"" + projectName + ".Service.I" + table + "Service\" />");
                            yaz.WriteLine("\t\t</service>");
                        }

                        yaz.WriteLine("\t</services>");
                        yaz.WriteLine("</system.serviceModel>");
                    }

                    yaz.Close();
                }
            }
        }

        void CreateWcfService()
        {
            if (chkAngular.Checked)
                CreateAngularDirectories();
            else
                CreateMVCDirectories();

            foreach (string Table in selectedTables)
            {
                List<string> identityColumns = Helper.Helper.ReturnIdentityColumn(connectionInfo, Table);

                string id = identityColumns.Count > 0 ? identityColumns.FirstOrDefault() : "id";

                List<ColumnInfo> columnNames = TableColumns(Table);
                List<ColumnInfo> urlColumns = TableColumns(Table, ColumnType.UrlColumns);
                List<ColumnInfo> guidColumns = TableColumns(Table, ColumnType.GuidColumns);
                List<ColumnInfo> codeColumns = TableColumns(Table, ColumnType.CodeColumns);
                List<ColumnInfo> searchColumns = TableColumns(Table, ColumnType.SearchColumns);
                bool deleted = TableColumns(Table, ColumnType.DeletedColumns).Count > 0 ? true : false;
                columnNames = columnNames.Where(a => !a.ColumnName.In(DeletedColumns, InType.ToUrlLower)).ToList();

                using (FileStream fs = new FileStream(PathAddress + "\\" + projectFolder + "\\Service\\I" + Table + "Service.cs", FileMode.Create))
                {
                    using (StreamWriter yaz = new StreamWriter(fs, Encoding.Unicode))
                    {
                        yaz.WriteLine("using System;");
                        yaz.WriteLine("using System.Collections.Generic;");
                        yaz.WriteLine("using System.Runtime.Serialization;");
                        yaz.WriteLine("using System.ServiceModel;");
                        yaz.WriteLine("using System.ServiceModel.Web;");
                        yaz.WriteLine("");
                        yaz.WriteLine("namespace " + projectName + ".Service");
                        yaz.WriteLine("{");
                        yaz.WriteLine("\t[ServiceContract]");
                        yaz.WriteLine("\tpublic interface I" + Table + "Service");
                        yaz.WriteLine("\t{");

                        // Select
                        yaz.WriteLine("\t\t[OperationContract]");
                        yaz.WriteLine("\t\t[WebGet(UriTemplate = \"/Select/?top={top}\", ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Wrapped)]");
                        yaz.WriteLine("\t\tList<" + Table + "Data> Select(string top);");
                        yaz.WriteLine("");

                        // SelectAll
                        yaz.WriteLine("\t\t[OperationContract]");
                        yaz.WriteLine("\t\t[WebGet(UriTemplate = \"/SelectAll/?id={id}\", ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Wrapped)]");
                        yaz.WriteLine("\t\tList<" + Table + "Data> SelectAll(string id);");
                        yaz.WriteLine("");

                        // SelectByID
                        yaz.WriteLine("\t\t[OperationContract]");
                        yaz.WriteLine("\t\t[WebGet(UriTemplate = \"/SelectByID/?id={id}\", ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Wrapped)]");
                        yaz.WriteLine("\t\t" + Table + "Data SelectByID(string id);");
                        yaz.WriteLine("");

                        foreach (ColumnInfo item in urlColumns)
                        {
                            // SelectByUrl
                            yaz.WriteLine("\t\t[OperationContract]");
                            yaz.WriteLine("\t\t[WebGet(UriTemplate = \"/SelectBy" + item.ColumnName + "/?url={url}\", ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Wrapped)]");
                            yaz.WriteLine("\t\t" + Table + "Data SelectBy" + item.ColumnName + "(string url);");
                            yaz.WriteLine("");
                        }

                        foreach (ColumnInfo item in guidColumns)
                        {
                            // SelectByGuid
                            yaz.WriteLine("\t\t[OperationContract]");
                            yaz.WriteLine("\t\t[WebGet(UriTemplate = \"/SelectBy" + item.ColumnName + "/?guid={guid}\", ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Wrapped)]");
                            yaz.WriteLine("\t\t" + Table + "Data SelectBy" + item.ColumnName + "(string guid);");
                            yaz.WriteLine("");
                        }

                        foreach (ColumnInfo item in codeColumns)
                        {
                            // SelectByCode
                            yaz.WriteLine("\t\t[OperationContract]");
                            yaz.WriteLine("\t\t[WebGet(UriTemplate = \"/SelectBy" + item.ColumnName + "/?code={code}\", ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Wrapped)]");
                            yaz.WriteLine("\t\tList<" + Table + "Data> SelectBy" + item.ColumnName + "(string code);");
                            yaz.WriteLine("");
                        }

                        foreach (ColumnInfo item in searchColumns)
                        {
                            // SelectBySearch
                            yaz.WriteLine("\t\t[OperationContract]");
                            yaz.WriteLine("\t\t[WebGet(UriTemplate = \"/SelectBy" + item.ColumnName + "/?searchText={searchText}\", ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Wrapped)]");
                            yaz.WriteLine("\t\tList<" + Table + "Data> SelectBy" + item.ColumnName + "(string searchText);");
                            yaz.WriteLine("");
                        }

                        // Insert
                        yaz.WriteLine("\t\t[OperationContract]");
                        yaz.WriteLine("\t\t[WebInvoke(Method = \"POST\", UriTemplate = \"/Insert/\", ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Wrapped)]");
                        yaz.WriteLine("\t\tbool Insert(" + Table + "Data table);");

                        if (identityColumns.Count > 0)
                        {
                            // Update
                            yaz.WriteLine("");
                            yaz.WriteLine("\t\t[OperationContract]");
                            yaz.WriteLine("\t\t[WebInvoke(Method = \"POST\", UriTemplate = \"/Update/\", ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Wrapped)]");
                            yaz.WriteLine("\t\tbool Update(" + Table + "Data table);");
                            yaz.WriteLine("");

                            // Copy
                            yaz.WriteLine("\t\t[OperationContract]");
                            yaz.WriteLine("\t\t[WebGet(UriTemplate = \"/Copy/?id={id}\", ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Wrapped)]");
                            yaz.WriteLine("\t\tbool Copy(string id);");
                            yaz.WriteLine("");

                            // Delete
                            yaz.WriteLine("\t\t[OperationContract]");
                            yaz.WriteLine("\t\t[WebGet(UriTemplate = \"/Delete/?id={id}\", ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Wrapped)]");
                            yaz.WriteLine("\t\tbool Delete(string id);");

                            if (deleted)
                            {
                                // Remove
                                yaz.WriteLine("");
                                yaz.WriteLine("\t\t[OperationContract]");
                                yaz.WriteLine("\t\t[WebGet(UriTemplate = \"/Remove/?id={id}\", ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Wrapped)]");
                                yaz.WriteLine("\t\tbool Remove(string id);");
                            }
                        }

                        yaz.WriteLine("\t}");
                        yaz.WriteLine("");
                        yaz.WriteLine("\t[DataContract]");
                        yaz.WriteLine("\tpublic class " + Table + "Data");
                        yaz.WriteLine("\t{");

                        foreach (ColumnInfo column in columnNames)
                        {
                            if (column.Type != null)
                            {
                                yaz.WriteLine("\t\t[DataMember]");

                                if (column.IsNullable)
                                {
                                    switch (column.Type.Name)
                                    {
                                        case "Int16": yaz.WriteLine("\t\tpublic int? " + column.ColumnName + " { get; set; }"); break;
                                        case "Int32": yaz.WriteLine("\t\tpublic int? " + column.ColumnName + " { get; set; }"); break;
                                        case "Int64": yaz.WriteLine("\t\tpublic Int64? " + column.ColumnName + " { get; set; }"); break;
                                        case "Decimal": yaz.WriteLine("\t\tpublic decimal? " + column.ColumnName + " { get; set; }"); break;
                                        case "Double": yaz.WriteLine("\t\tpublic double? " + column.ColumnName + " { get; set; }"); break;
                                        case "Char": yaz.WriteLine("\t\tpublic char " + column.ColumnName + " { get; set; }"); break;
                                        case "Chars": yaz.WriteLine("\t\tpublic char[] " + column.ColumnName + " { get; set; }"); break;
                                        case "String": yaz.WriteLine("\t\tpublic string " + column.ColumnName + " { get; set; }"); break;
                                        case "Byte": yaz.WriteLine("\t\tpublic byte " + column.ColumnName + " { get; set; }"); break;
                                        case "Bytes": yaz.WriteLine("\t\tpublic byte[] " + column.ColumnName + " { get; set; }"); break;
                                        case "Boolean": yaz.WriteLine("\t\tpublic bool? " + column.ColumnName + " { get; set; }"); break;
                                        case "DateTime": yaz.WriteLine("\t\tpublic DateTime? " + column.ColumnName + " { get; set; }"); break;
                                        case "DateTimeOffset": yaz.WriteLine("\t\tpublic DateTimeOffset? " + column.ColumnName + " { get; set; }"); break;
                                        case "TimeSpan": yaz.WriteLine("\t\tpublic TimeSpan? " + column.ColumnName + " { get; set; }"); break;
                                        case "Single": yaz.WriteLine("\t\tpublic Single? " + column.ColumnName + " { get; set; }"); break;
                                        case "Object": yaz.WriteLine("\t\tpublic object " + column.ColumnName + " { get; set; }"); break;
                                        case "Guid": yaz.WriteLine("\t\tpublic Guid? " + column.ColumnName + " { get; set; }"); break;
                                        default: yaz.WriteLine("\t\tpublic string " + column.ColumnName + " { get; set; }"); break;
                                    }
                                }
                                else
                                {
                                    switch (column.Type.Name)
                                    {
                                        case "Int16": yaz.WriteLine("\t\tpublic int " + column.ColumnName + " { get; set; }"); break;
                                        case "Int32": yaz.WriteLine("\t\tpublic int " + column.ColumnName + " { get; set; }"); break;
                                        case "Int64": yaz.WriteLine("\t\tpublic Int64 " + column.ColumnName + " { get; set; }"); break;
                                        case "Decimal": yaz.WriteLine("\t\tpublic decimal " + column.ColumnName + " { get; set; }"); break;
                                        case "Double": yaz.WriteLine("\t\tpublic double " + column.ColumnName + " { get; set; }"); break;
                                        case "Char": yaz.WriteLine("\t\tpublic char " + column.ColumnName + " { get; set; }"); break;
                                        case "Chars": yaz.WriteLine("\t\tpublic char[] " + column.ColumnName + " { get; set; }"); break;
                                        case "String": yaz.WriteLine("\t\tpublic string " + column.ColumnName + " { get; set; }"); break;
                                        case "Byte": yaz.WriteLine("\t\tpublic byte " + column.ColumnName + " { get; set; }"); break;
                                        case "Bytes": yaz.WriteLine("\t\tpublic byte[] " + column.ColumnName + " { get; set; }"); break;
                                        case "Boolean": yaz.WriteLine("\t\tpublic bool " + column.ColumnName + " { get; set; }"); break;
                                        case "DateTime": yaz.WriteLine("\t\tpublic DateTime " + column.ColumnName + " { get; set; }"); break;
                                        case "DateTimeOffset": yaz.WriteLine("\t\tpublic DateTimeOffset " + column.ColumnName + " { get; set; }"); break;
                                        case "TimeSpan": yaz.WriteLine("\t\tpublic TimeSpan " + column.ColumnName + " { get; set; }"); break;
                                        case "Single": yaz.WriteLine("\t\tpublic Single " + column.ColumnName + " { get; set; }"); break;
                                        case "Object": yaz.WriteLine("\t\tpublic object " + column.ColumnName + " { get; set; }"); break;
                                        case "Guid": yaz.WriteLine("\t\tpublic Guid " + column.ColumnName + " { get; set; }"); break;
                                        default: yaz.WriteLine("\t\tpublic string " + column.ColumnName + " { get; set; }"); break;
                                    }
                                }
                            }
                            else
                            {
                                yaz.WriteLine("\t\t//" + column.ColumnName + " isimli kolonun veri tipi bu programda tanumlı değil.");
                            }
                        }

                        yaz.WriteLine("\t}");
                        yaz.WriteLine("}");

                        yaz.Close();
                    }
                }

                using (FileStream fs = new FileStream(PathAddress + "\\" + projectFolder + "\\Service\\" + Table + "Service.svc", FileMode.Create))
                {
                    using (StreamWriter yaz = new StreamWriter(fs, Encoding.Unicode))
                    {
                        yaz.WriteLine("<%@ ServiceHost Language=\"C#\" Debug=\"true\" Service=\"" + projectName + ".Service." + Table + "Service\" CodeBehind=\"" + Table + "Service.svc.cs\" %>");

                        yaz.Close();
                    }
                }

                using (FileStream fs = new FileStream(PathAddress + "\\" + projectFolder + "\\Service\\" + Table + "Service.svc.cs", FileMode.Create))
                {
                    using (StreamWriter yaz = new StreamWriter(fs, Encoding.Unicode))
                    {
                        string columns = "";
                        string idcolumns = "";

                        foreach (ColumnInfo column in columnNames)
                        {
                            if (column.Type != null)
                            {
                                if (!column.IsIdentity)
                                    columns += "table." + column.ColumnName + ", ";
                                else
                                    idcolumns = "table." + column.ColumnName + ", ";
                            }
                        }

                        columns = columns.TrimEnd(' ').TrimEnd(',');

                        yaz.WriteLine("using System;");
                        yaz.WriteLine("using System.Linq;");
                        yaz.WriteLine("using System.Collections.Generic;");
                        yaz.WriteLine("using TDLibrary;");
                        yaz.WriteLine("using " + repositoryName + "." + Table + "Model;");
                        yaz.WriteLine("");
                        yaz.WriteLine("namespace " + projectName + ".Service");
                        yaz.WriteLine("{");
                        yaz.WriteLine("\tpublic class " + Table + "Service : I" + Table + "Service");
                        yaz.WriteLine("\t{");

                        yaz.WriteLine("\t\t" + Table + " model = new " + Table + "();");
                        yaz.WriteLine("");

                        //Select
                        yaz.WriteLine("\t\tpublic List<" + Table + "Data> Select(string top)");
                        yaz.WriteLine("\t\t{");
                        yaz.WriteLine("\t\t\tint _top;");
                        yaz.WriteLine("\t\t\tbool con = int.TryParse(top, out _top);");
                        yaz.WriteLine("");
                        yaz.WriteLine("\t\t\tif (con)");
                        yaz.WriteLine("\t\t\t\treturn model.List(null, _top).ChangeModelList<" + Table + "Data, " + Table + ">();");
                        yaz.WriteLine("");
                        yaz.WriteLine("\t\t\treturn model.List(null).ChangeModelList<" + Table + "Data, " + Table + ">();");
                        yaz.WriteLine("\t\t}");
                        yaz.WriteLine("");

                        //SelectAll
                        yaz.WriteLine("\t\tpublic List<" + Table + "Data> SelectAll(string id)");
                        yaz.WriteLine("\t\t{");
                        yaz.WriteLine("\t\t\tint _id;");
                        yaz.WriteLine("\t\t\tbool con = int.TryParse(id, out _id);");
                        yaz.WriteLine("");
                        yaz.WriteLine("\t\t\tif (con)");
                        yaz.WriteLine("\t\t\t\treturn model.ListAll(_id).ChangeModelList<" + Table + "Data, " + Table + ">();");
                        yaz.WriteLine("");
                        yaz.WriteLine("\t\t\treturn model.ListAll(null).ChangeModelList<" + Table + "Data, " + Table + ">();");
                        yaz.WriteLine("\t\t}");
                        yaz.WriteLine("");

                        //SelectByID
                        yaz.WriteLine("\t\tpublic " + Table + "Data SelectByID(string id)");
                        yaz.WriteLine("\t\t{");
                        yaz.WriteLine("\t\t\tint _id;");
                        yaz.WriteLine("\t\t\tbool con = int.TryParse(id, out _id);");
                        yaz.WriteLine("");
                        yaz.WriteLine("\t\t\tif (con)");
                        yaz.WriteLine("\t\t\t\treturn model.Select(_id).ChangeModel<" + Table + "Data>();");
                        yaz.WriteLine("");
                        yaz.WriteLine("\t\t\treturn model.List(null).FirstOrDefault().ChangeModel<" + Table + "Data>();");
                        yaz.WriteLine("\t\t}");
                        yaz.WriteLine("");

                        foreach (ColumnInfo item in urlColumns)
                        {
                            //SelectByUrl
                            yaz.WriteLine("\t\tpublic " + Table + "Data SelectBy" + item.ColumnName + "(string url)");
                            yaz.WriteLine("\t\t{");
                            yaz.WriteLine("\t\t\treturn model.SelectBy" + item.ColumnName + "(url).ChangeModel<" + Table + "Data>();");
                            yaz.WriteLine("\t\t}");
                            yaz.WriteLine("");
                        }

                        foreach (ColumnInfo item in guidColumns)
                        {
                            //SelectByGuid
                            yaz.WriteLine("\t\tpublic " + Table + "Data SelectBy" + item.ColumnName + "(string guid)");
                            yaz.WriteLine("\t\t{");
                            yaz.WriteLine("\t\t\treturn model.SelectBy" + item.ColumnName + "(guid).ChangeModel<" + Table + "Data>();");
                            yaz.WriteLine("\t\t}");
                            yaz.WriteLine("");
                        }

                        foreach (ColumnInfo item in codeColumns)
                        {
                            //SelectByCode
                            yaz.WriteLine("\t\tpublic List<" + Table + "Data> SelectBy" + item.ColumnName + "(string code)");
                            yaz.WriteLine("\t\t{");
                            yaz.WriteLine("\t\t\treturn model.SelectBy" + item.ColumnName + "(code).ChangeModelList<" + Table + "Data, " + Table + ">();");
                            yaz.WriteLine("\t\t}");
                            yaz.WriteLine("");
                        }

                        foreach (ColumnInfo item in searchColumns)
                        {
                            //SelectBySearch
                            yaz.WriteLine("\t\tpublic List<" + Table + "Data> SelectBy" + item.ColumnName + "(string searchText)");
                            yaz.WriteLine("\t\t{");
                            yaz.WriteLine("\t\t\treturn model.SelectBy" + item.ColumnName + "(searchText).ChangeModelList<" + Table + "Data, " + Table + ">();");
                            yaz.WriteLine("\t\t}");
                            yaz.WriteLine("");
                        }

                        //Insert
                        yaz.WriteLine("\t\tpublic bool Insert(" + Table + "Data table)");
                        yaz.WriteLine("\t\t{");
                        yaz.WriteLine("\t\t\tif (table != null)");
                        yaz.WriteLine("\t\t\t\treturn model.Insert(table.ChangeModel<" + Table + ">());");
                        yaz.WriteLine("");
                        yaz.WriteLine("\t\t\treturn false;");
                        yaz.WriteLine("\t\t}");
                        yaz.WriteLine("");

                        if (identityColumns.Count > 0)
                        {
                            //Update
                            yaz.WriteLine("\t\tpublic bool Update(" + Table + "Data table)");
                            yaz.WriteLine("\t\t{");
                            yaz.WriteLine("\t\t\tif (table != null)");
                            yaz.WriteLine("\t\t\t\treturn model.Update(table.ChangeModel<" + Table + ">());");
                            yaz.WriteLine("");
                            yaz.WriteLine("\t\t\treturn false;");
                            yaz.WriteLine("\t\t}");
                            yaz.WriteLine("");

                            //Copy
                            yaz.WriteLine("\t\tpublic bool Copy(string id)");
                            yaz.WriteLine("\t\t{");
                            yaz.WriteLine("\t\t\tint _id;");
                            yaz.WriteLine("\t\t\tbool con = int.TryParse(id, out _id);");
                            yaz.WriteLine("");
                            yaz.WriteLine("\t\t\tif (con)");
                            yaz.WriteLine("\t\t\t\treturn model.Copy(_id);");
                            yaz.WriteLine("");
                            yaz.WriteLine("\t\t\treturn false;");
                            yaz.WriteLine("\t\t}");
                            yaz.WriteLine("");

                            //Delete
                            yaz.WriteLine("\t\tpublic bool Delete(string id)");
                            yaz.WriteLine("\t\t{");
                            yaz.WriteLine("\t\t\tint _id;");
                            yaz.WriteLine("\t\t\tbool con = int.TryParse(id, out _id);");
                            yaz.WriteLine("");
                            yaz.WriteLine("\t\t\tif (con)");
                            yaz.WriteLine("\t\t\t\treturn model.Delete(_id);");
                            yaz.WriteLine("");
                            yaz.WriteLine("\t\t\treturn false;");
                            yaz.WriteLine("\t\t}");

                            if (deleted)
                            {
                                //Remove
                                yaz.WriteLine("");
                                yaz.WriteLine("\t\tpublic bool Remove(string id)");
                                yaz.WriteLine("\t\t{");
                                yaz.WriteLine("\t\t\tint _id;");
                                yaz.WriteLine("\t\t\tbool con = int.TryParse(id, out _id);");
                                yaz.WriteLine("");
                                yaz.WriteLine("\t\t\tif (con)");
                                yaz.WriteLine("\t\t\t\treturn model.Remove(_id);");
                                yaz.WriteLine("");
                                yaz.WriteLine("\t\t\treturn false;");
                                yaz.WriteLine("\t\t}");
                            }
                        }

                        yaz.WriteLine("\t}");
                        yaz.WriteLine("}");

                        yaz.Close();
                    }
                }
            }
        }

        void CreateStoredProcedure()
        {
            string schema = DefaultSchema(new SqlConnection(Helper.Helper.CreateConnectionText(connectionInfo)));

            using (FileStream fs = new FileStream(PathAddress + "\\" + projectFolder + "\\StoredProcedures\\_StoredProcedures.sql", FileMode.Create))
            {
                using (StreamWriter yaz = new StreamWriter(fs, Encoding.Unicode))
                {
                    yaz.WriteLine("USE [" + DBName + "]");
                    yaz.WriteLine("GO");
                    yaz.WriteLine("");

                    foreach (string Table in selectedTables)
                    {
                        List<string> identityColumns = Helper.Helper.ReturnIdentityColumn(connectionInfo, Table);
                        string idColumn = identityColumns.FirstOrDefault();

                        SqlConnection con = new SqlConnection(Helper.Helper.CreateConnectionText(connectionInfo));
                        List<ForeignKeyChecker> fkcList = ForeignKeyCheck(con, Table);
                        fkcList = fkcList.Where(a => a.PrimaryTableName == Table).ToList();

                        List<ForeignKeyChecker> fkcListForeign = ForeignKeyCheck(con);
                        fkcListForeign = fkcListForeign.Where(a => a.ForeignTableName == Table).ToList();

                        string[] diziML = new string[] { "nvarchar", "varchar", "binary", "char", "nchar", "varbinary" };

                        int i = 0;

                        List<ColumnInfo> columnNames = TableColumns(Table);
                        List<ColumnInfo> guidColumns = TableColumns(Table, ColumnType.GuidColumns);
                        List<ColumnInfo> urlColumns = TableColumns(Table, ColumnType.UrlColumns);
                        List<ColumnInfo> codeColumns = TableColumns(Table, ColumnType.CodeColumns);
                        List<ColumnInfo> searchColumns = TableColumns(Table, ColumnType.SearchColumns);
                        string deleted = columnNames.Where(a => a.ColumnName.In(DeletedColumns, InType.ToUrlLower)).ToList().Count > 0 ? " and [Deleted] = 0" : "";

                        string idType = null;

                        string searchText = GetColumnText(tableColumnInfos.Where(a => a.TableName == Table).ToList());

                        if (searchText.Contains(".ToString()"))
                            searchText = "";

                        try
                        {
                            idType = columnNames.Where(a => a.ColumnName == idColumn).FirstOrDefault().DataType;
                        }
                        catch
                        {
                        }

                        //Select//
                        yaz.WriteLine("/* Select */");
                        yaz.WriteLine("IF OBJECT_ID('" + schema + ".[usp_" + Table + "Select]') IS NOT NULL");
                        yaz.WriteLine("BEGIN");
                        yaz.WriteLine("\tDROP PROC " + schema + ".[usp_" + Table + "Select]");
                        yaz.WriteLine("END");
                        yaz.WriteLine("GO");
                        yaz.WriteLine("CREATE PROC " + schema + ".[usp_" + Table + "Select]");

                        if (idType != null)
                        {
                            yaz.WriteLine("\t@" + idColumn + " " + idType);
                        }

                        yaz.WriteLine("AS");
                        yaz.WriteLine("\tSET NOCOUNT ON");
                        yaz.WriteLine("\tSET XACT_ABORT ON");
                        yaz.WriteLine("");
                        yaz.WriteLine("\tBEGIN TRAN");
                        yaz.WriteLine("");

                        string sqlText = "\tSELECT ";

                        foreach (ColumnInfo column in columnNames)
                        {
                            if (!column.ColumnName.In(DeletedColumns, InType.ToUrlLower))
                                sqlText += "[" + column.ColumnName + "],";
                        }

                        sqlText = sqlText.Remove(sqlText.Length - 1);
                        sqlText = sqlText.Replace(",", ", ");

                        yaz.WriteLine(sqlText);
                        yaz.WriteLine("\tFROM " + schema + ".[" + Table + "]");

                        if (idType != null)
                        {
                            yaz.WriteLine("\tWHERE ([" + idColumn + "] = @" + idColumn + " OR @" + idColumn + " IS NULL)" + deleted);
                        }

                        yaz.WriteLine("");
                        yaz.WriteLine("\tCOMMIT");
                        yaz.WriteLine("GO");
                        yaz.WriteLine("");
                        //Select//

                        //SelectTop//
                        yaz.WriteLine("/* SelectTop */");
                        yaz.WriteLine("IF OBJECT_ID('" + schema + ".[usp_" + Table + "SelectTop]') IS NOT NULL");
                        yaz.WriteLine("BEGIN");
                        yaz.WriteLine("\tDROP PROC " + schema + ".[usp_" + Table + "SelectTop]");
                        yaz.WriteLine("END");
                        yaz.WriteLine("GO");
                        yaz.WriteLine("CREATE PROC " + schema + ".[usp_" + Table + "SelectTop]");

                        if (idType != null)
                        {
                            yaz.WriteLine("\t@" + idColumn + " " + idType + ",");
                        }
                        yaz.WriteLine("\t@Top int");

                        yaz.WriteLine("AS");
                        yaz.WriteLine("\tSET NOCOUNT ON");
                        yaz.WriteLine("\tSET XACT_ABORT ON");
                        yaz.WriteLine("");
                        yaz.WriteLine("\tBEGIN TRAN");
                        yaz.WriteLine("");

                        sqlText = "\tSELECT Top (@Top) ";

                        foreach (ColumnInfo column in columnNames)
                        {
                            if (!column.ColumnName.In(DeletedColumns, InType.ToUrlLower))
                                sqlText += "[" + column.ColumnName + "],";
                        }

                        sqlText = sqlText.Remove(sqlText.Length - 1);
                        sqlText = sqlText.Replace(",", ", ");

                        yaz.WriteLine(sqlText);
                        yaz.WriteLine("\tFROM " + schema + ".[" + Table + "]");

                        if (idType != null)
                        {
                            yaz.WriteLine("\tWHERE ([" + idColumn + "] = @" + idColumn + " OR @" + idColumn + " IS NULL)" + deleted);
                        }

                        yaz.WriteLine("");
                        yaz.WriteLine("\tCOMMIT");
                        yaz.WriteLine("GO");
                        yaz.WriteLine("");
                        //SelectTop//

                        foreach (ColumnInfo item in urlColumns)
                        {
                            //SelectByUrl//
                            yaz.WriteLine("/* SelectBy" + item.ColumnName + " */");
                            yaz.WriteLine("IF OBJECT_ID('" + schema + ".[usp_" + Table + "SelectBy" + item.ColumnName + "]') IS NOT NULL");
                            yaz.WriteLine("BEGIN");
                            yaz.WriteLine("\tDROP PROC " + schema + ".[usp_" + Table + "SelectBy" + item.ColumnName + "]");
                            yaz.WriteLine("END");
                            yaz.WriteLine("GO");
                            yaz.WriteLine("CREATE PROC " + schema + ".[usp_" + Table + "SelectBy" + item.ColumnName + "]");
                            yaz.WriteLine("\t@" + item.ColumnName + " " + item.DataType + "(" + item.MaxLength + ")");
                            yaz.WriteLine("AS");
                            yaz.WriteLine("\tSET NOCOUNT ON");
                            yaz.WriteLine("\tSET XACT_ABORT ON");
                            yaz.WriteLine("");
                            yaz.WriteLine("\tBEGIN TRAN");
                            yaz.WriteLine("");

                            sqlText = "\tSELECT Top (1) ";

                            foreach (ColumnInfo column in columnNames)
                            {
                                if (!column.ColumnName.In(DeletedColumns, InType.ToUrlLower))
                                    sqlText += "[" + column.ColumnName + "],";
                            }

                            sqlText = sqlText.Remove(sqlText.Length - 1);
                            sqlText = sqlText.Replace(",", ", ");

                            yaz.WriteLine(sqlText);
                            yaz.WriteLine("\tFROM " + schema + ".[" + Table + "]");
                            yaz.WriteLine("\tWHERE ([" + item.ColumnName + "] = @" + item.ColumnName + " OR @" + item.ColumnName + " IS NULL)" + deleted);
                            yaz.WriteLine("");
                            yaz.WriteLine("\tCOMMIT");
                            yaz.WriteLine("GO");
                            yaz.WriteLine("");
                            //SelectByUrl//
                        }

                        foreach (ColumnInfo item in guidColumns)
                        {
                            //SelectByGuid//
                            yaz.WriteLine("/* SelectBy" + item.ColumnName + " */");
                            yaz.WriteLine("IF OBJECT_ID('" + schema + ".[usp_" + Table + "SelectBy" + item.ColumnName + "]') IS NOT NULL");
                            yaz.WriteLine("BEGIN");
                            yaz.WriteLine("\tDROP PROC " + schema + ".[usp_" + Table + "SelectBy" + item.ColumnName + "]");
                            yaz.WriteLine("END");
                            yaz.WriteLine("GO");
                            yaz.WriteLine("CREATE PROC " + schema + ".[usp_" + Table + "SelectBy" + item.ColumnName + "]");
                            yaz.WriteLine("\t@" + item.ColumnName + " " + item.DataType + "(" + item.MaxLength + ")");
                            yaz.WriteLine("AS");
                            yaz.WriteLine("\tSET NOCOUNT ON");
                            yaz.WriteLine("\tSET XACT_ABORT ON");
                            yaz.WriteLine("");
                            yaz.WriteLine("\tBEGIN TRAN");
                            yaz.WriteLine("");

                            sqlText = "\tSELECT Top (1) ";

                            foreach (ColumnInfo column in columnNames)
                            {
                                if (!column.ColumnName.In(DeletedColumns, InType.ToUrlLower))
                                    sqlText += "[" + column.ColumnName + "],";
                            }

                            sqlText = sqlText.Remove(sqlText.Length - 1);
                            sqlText = sqlText.Replace(",", ", ");

                            yaz.WriteLine(sqlText);
                            yaz.WriteLine("\tFROM " + schema + ".[" + Table + "]");
                            yaz.WriteLine("\tWHERE ([" + item.ColumnName + "] = @" + item.ColumnName + " OR @" + item.ColumnName + " IS NULL)" + deleted);
                            yaz.WriteLine("");
                            yaz.WriteLine("\tCOMMIT");
                            yaz.WriteLine("GO");
                            yaz.WriteLine("");
                            //SelectByGuid//
                        }

                        foreach (ColumnInfo item in codeColumns)
                        {
                            //SelectByCode//
                            yaz.WriteLine("/* SelectBy" + item.ColumnName + " */");
                            yaz.WriteLine("IF OBJECT_ID('" + schema + ".[usp_" + Table + "SelectBy" + item.ColumnName + "]') IS NOT NULL");
                            yaz.WriteLine("BEGIN");
                            yaz.WriteLine("\tDROP PROC " + schema + ".[usp_" + Table + "SelectBy" + item.ColumnName + "]");
                            yaz.WriteLine("END");
                            yaz.WriteLine("GO");
                            yaz.WriteLine("CREATE PROC " + schema + ".[usp_" + Table + "SelectBy" + item.ColumnName + "]");
                            yaz.WriteLine("\t@" + item.ColumnName + " " + item.DataType + "(" + item.MaxLength + ")");
                            yaz.WriteLine("AS");
                            yaz.WriteLine("\tSET NOCOUNT ON");
                            yaz.WriteLine("\tSET XACT_ABORT ON");
                            yaz.WriteLine("");
                            yaz.WriteLine("\tBEGIN TRAN");
                            yaz.WriteLine("");

                            sqlText = "\tSELECT ";

                            foreach (ColumnInfo column in columnNames)
                            {
                                if (!column.ColumnName.In(DeletedColumns, InType.ToUrlLower))
                                    sqlText += "[" + column.ColumnName + "],";
                            }

                            sqlText = sqlText.Remove(sqlText.Length - 1);
                            sqlText = sqlText.Replace(",", ", ");

                            yaz.WriteLine(sqlText);
                            yaz.WriteLine("\tFROM " + schema + ".[" + Table + "]");
                            yaz.WriteLine("\tWHERE ([" + item.ColumnName + "] = @" + item.ColumnName + " OR @" + item.ColumnName + " IS NULL)" + deleted);
                            yaz.WriteLine("");
                            yaz.WriteLine("\tCOMMIT");
                            yaz.WriteLine("GO");
                            yaz.WriteLine("");
                            //SelectByCode//
                        }

                        foreach (ColumnInfo item in searchColumns)
                        {
                            //SelectBySearch//
                            yaz.WriteLine("/* SelectBy" + item.ColumnName + " */");
                            yaz.WriteLine("IF OBJECT_ID('" + schema + ".[usp_" + Table + "SelectBy" + item.ColumnName + "]') IS NOT NULL");
                            yaz.WriteLine("BEGIN");
                            yaz.WriteLine("\tDROP PROC " + schema + ".[usp_" + Table + "SelectBy" + item.ColumnName + "]");
                            yaz.WriteLine("END");
                            yaz.WriteLine("GO");
                            yaz.WriteLine("CREATE PROC " + schema + ".[usp_" + Table + "SelectBy" + item.ColumnName + "]");

                            if (item.Type.Name == "String")
                            {
                                yaz.WriteLine("\t@" + item.ColumnName + " " + item.DataType + "(" + item.MaxLength + ")");
                            }
                            else
                            {
                                yaz.WriteLine("\t@" + item.ColumnName + " " + item.DataType);
                            }

                            yaz.WriteLine("AS");
                            yaz.WriteLine("\tSET NOCOUNT ON");
                            yaz.WriteLine("\tSET XACT_ABORT ON");
                            yaz.WriteLine("");
                            yaz.WriteLine("\tBEGIN TRAN");
                            yaz.WriteLine("");

                            sqlText = "\tSELECT ";

                            foreach (ColumnInfo column in columnNames)
                            {
                                if (!column.ColumnName.In(DeletedColumns, InType.ToUrlLower))
                                    sqlText += "[" + column.ColumnName + "],";
                            }

                            sqlText = sqlText.Remove(sqlText.Length - 1);
                            sqlText = sqlText.Replace(",", ", ");

                            yaz.WriteLine(sqlText);
                            yaz.WriteLine("\tFROM " + schema + ".[" + Table + "]");

                            string whereClause;

                            if (item.Type.Name == "String")
                            {
                                whereClause = "\tWHERE ([" + item.ColumnName + "] LIKE '%' + @" + item.ColumnName + " + '%' OR @" + item.ColumnName + " IS NULL)" + deleted;
                            }
                            else
                            {
                                whereClause = "\tWHERE ([" + item.ColumnName + "] = @" + item.ColumnName + " OR @" + item.ColumnName + " IS NULL)" + deleted;
                            }

                            yaz.WriteLine(whereClause);
                            yaz.WriteLine("");
                            yaz.WriteLine("\tCOMMIT");
                            yaz.WriteLine("GO");
                            yaz.WriteLine("");
                            //SelectBySearch//
                        }

                        //LinkedSelect//
                        if (fkcListForeign.Count > 0)
                        {
                            yaz.WriteLine("/* LinkedSelect */");
                            yaz.WriteLine("IF OBJECT_ID('" + schema + ".[usp_" + Table + "LinkedSelect]') IS NOT NULL");
                            yaz.WriteLine("BEGIN");
                            yaz.WriteLine("\tDROP PROC " + schema + ".[usp_" + Table + "LinkedSelect]");
                            yaz.WriteLine("END");
                            yaz.WriteLine("GO");
                            yaz.WriteLine("CREATE PROC " + schema + ".[usp_" + Table + "LinkedSelect]");

                            if (idType != null)
                            {
                                yaz.WriteLine("\t@" + idColumn + " " + idType);
                            }

                            yaz.WriteLine("AS");
                            yaz.WriteLine("\tSET NOCOUNT ON");
                            yaz.WriteLine("\tSET XACT_ABORT ON");
                            yaz.WriteLine("");
                            yaz.WriteLine("\tBEGIN TRAN");
                            yaz.WriteLine("");

                            sqlText = "\tSELECT ";

                            foreach (ColumnInfo column in columnNames)
                            {
                                if (!column.ColumnName.In(DeletedColumns, InType.ToUrlLower))
                                    sqlText += "[" + column.ColumnName + "],";
                            }

                            yaz.WriteLine(sqlText);

                            i = 0;
                            int fkcCount = fkcListForeign.GroupBy(a => a.PrimaryTableName).Select(a => a.First()).ToList().Count;
                            foreach (ForeignKeyChecker fkc in fkcListForeign.GroupBy(a => a.PrimaryTableName).Select(a => a.First()).ToList())
                            {
                                sqlText = "";

                                string PrimaryTableName = fkc.PrimaryTableName;
                                string columnText = GetColumnText(tableColumnInfos.Where(a => a.TableName == PrimaryTableName).ToList()).Replace(".ToString()", "");

                                sqlText += "\t\t(SELECT " + aliases[i % 10] + "." + columnText + " FROM " + PrimaryTableName + " " + aliases[i % 10] + " WHERE " + aliases[i % 10] + "." + fkc.PrimaryColumnName + " = " + fkc.ForeignColumnName + ") as " + PrimaryTableName + "Adi,";

                                if (fkcCount == i + 1)
                                {
                                    sqlText = sqlText.Remove(sqlText.Length - 1);
                                    sqlText = sqlText.Replace(",", ", ");
                                }

                                yaz.WriteLine(sqlText);

                                i++;
                            }

                            yaz.WriteLine("\tFROM " + schema + ".[" + Table + "]");

                            if (idType != null)
                            {
                                yaz.WriteLine("\tWHERE ([" + idColumn + "] = @" + idColumn + " OR @" + idColumn + " IS NULL)" + deleted);
                            }

                            yaz.WriteLine("");
                            yaz.WriteLine("\tCOMMIT");
                            yaz.WriteLine("GO");
                            yaz.WriteLine("");
                        }
                        //LinkedSelect//

                        //ByLinkedIDSelect//
                        if (fkcList.Count > 0)
                        {
                            foreach (ForeignKeyChecker fkc in fkcList.GroupBy(a => a.PrimaryTableName).Select(a => a.First()).ToList())
                            {
                                foreach (ForeignKeyChecker fkc2 in fkcList.GroupBy(a => a.ForeignTableName).Select(a => a.First()).ToList())
                                {
                                    string PrimaryTableName = fkc.PrimaryTableName;
                                    string ForeignTableName = fkc2.ForeignTableName;
                                    string columnText = GetColumnText(tableColumnInfos.Where(a => a.TableName == Table).ToList()).Replace(".ToString()", "");

                                    List<ColumnInfo> fColumnNames = Helper.Helper.GetColumnsInfo(connectionInfo, ForeignTableName).ToList();
                                    string fDeleted = fColumnNames.Where(a => a.ColumnName.In(DeletedColumns, InType.ToUrlLower)).ToList().Count > 0 ? " and [Deleted] = 0" : "";

                                    yaz.WriteLine("/* ByLinkedIDSelect */");
                                    yaz.WriteLine("IF OBJECT_ID('" + schema + ".[usp_" + ForeignTableName + "_" + PrimaryTableName + "ByLinkedIDSelect]') IS NOT NULL");
                                    yaz.WriteLine("BEGIN");
                                    yaz.WriteLine("\tDROP PROC " + schema + ".[usp_" + ForeignTableName + "_" + PrimaryTableName + "ByLinkedIDSelect]");
                                    yaz.WriteLine("END");
                                    yaz.WriteLine("GO");
                                    yaz.WriteLine("CREATE PROC " + schema + ".[usp_" + ForeignTableName + "_" + PrimaryTableName + "ByLinkedIDSelect]");

                                    string fidType = null;
                                    try
                                    {
                                        fidType = fColumnNames.Where(a => a.ColumnName == fkc2.ForeignColumnName).FirstOrDefault().DataType;
                                    }
                                    catch
                                    {
                                    }

                                    if (fidType != null)
                                    {
                                        yaz.WriteLine("\t@" + fkc2.ForeignColumnName + " " + fidType);
                                    }

                                    yaz.WriteLine("AS");
                                    yaz.WriteLine("\tSET NOCOUNT ON");
                                    yaz.WriteLine("\tSET XACT_ABORT ON");
                                    yaz.WriteLine("");
                                    yaz.WriteLine("\tBEGIN TRAN");
                                    yaz.WriteLine("");

                                    sqlText = "\tSELECT ";

                                    foreach (ColumnInfo column in fColumnNames)
                                    {
                                        if (!column.ColumnName.In(DeletedColumns, InType.ToUrlLower))
                                            sqlText += "[" + column.ColumnName + "],";
                                    }

                                    yaz.WriteLine(sqlText);

                                    sqlText = "";

                                    sqlText += "\t(SELECT A." + columnText + " FROM " + Table + " A WHERE A." + fkc.PrimaryColumnName + " = " + fkc2.ForeignColumnName + ") as " + Table + "Adi";

                                    yaz.WriteLine(sqlText);

                                    yaz.WriteLine("\tFROM " + schema + ".[" + ForeignTableName + "]");

                                    yaz.WriteLine("\tWHERE ([" + fkc2.ForeignColumnName + "] = @" + fkc2.ForeignColumnName + " OR @" + fkc2.ForeignColumnName + " IS NULL)" + fDeleted);

                                    yaz.WriteLine("");
                                    yaz.WriteLine("\tCOMMIT");
                                    yaz.WriteLine("GO");
                                    yaz.WriteLine("");
                                }
                            }
                        }
                        //ByLinkedIDSelect//

                        //SelectAll//
                        yaz.WriteLine("/* SelectAll */");
                        yaz.WriteLine("IF OBJECT_ID('" + schema + ".[usp_" + Table + "SelectAll]') IS NOT NULL");
                        yaz.WriteLine("BEGIN");
                        yaz.WriteLine("\tDROP PROC " + schema + ".[usp_" + Table + "SelectAll]");
                        yaz.WriteLine("END");
                        yaz.WriteLine("GO");
                        yaz.WriteLine("CREATE PROC " + schema + ".[usp_" + Table + "SelectAll]");

                        if (idType != null)
                        {
                            yaz.WriteLine("\t@" + idColumn + " " + idType);
                        }

                        yaz.WriteLine("AS");
                        yaz.WriteLine("\tSET NOCOUNT ON");
                        yaz.WriteLine("\tSET XACT_ABORT ON");
                        yaz.WriteLine("");
                        yaz.WriteLine("\tBEGIN TRAN");
                        yaz.WriteLine("");

                        sqlText = "\tSELECT ";

                        foreach (ColumnInfo column in columnNames)
                        {
                            sqlText += "[" + column.ColumnName + "],";
                        }

                        sqlText = sqlText.Remove(sqlText.Length - 1);
                        sqlText = sqlText.Replace(",", ", ");

                        yaz.WriteLine(sqlText);
                        yaz.WriteLine("\tFROM " + schema + ".[" + Table + "]");

                        if (idType != null)
                        {
                            yaz.WriteLine("\tWHERE ([" + idColumn + "] = @" + idColumn + " OR @" + idColumn + " IS NULL)");
                        }

                        yaz.WriteLine("");
                        yaz.WriteLine("\tCOMMIT");
                        yaz.WriteLine("GO");
                        yaz.WriteLine("");
                        //SelectAll//

                        //Insert//
                        yaz.WriteLine("/* Insert */");
                        yaz.WriteLine("IF OBJECT_ID('" + schema + ".[usp_" + Table + "Insert]') IS NOT NULL");
                        yaz.WriteLine("BEGIN");
                        yaz.WriteLine("\tDROP PROC " + schema + ".[usp_" + Table + "Insert]");
                        yaz.WriteLine("END");
                        yaz.WriteLine("GO");
                        yaz.WriteLine("CREATE PROC " + schema + ".[usp_" + Table + "Insert]");

                        i = 1;
                        foreach (ColumnInfo column in columnNames.Where(a => !a.ColumnName.In(DeletedColumns, InType.ToUrlLower)).ToList())
                        {
                            if (!column.IsIdentity)
                            {
                                string extra = "";

                                if (column.DataType.In(diziML))
                                    extra += column.MaxLength != "" ? "(" + column.MaxLength + ")" : "";

                                extra += column.IsNullable ? " = NULL" : "";

                                if (i != columnNames.Where(a => !a.ColumnName.In(DeletedColumns, InType.ToUrlLower)).ToList().Count)
                                    yaz.WriteLine("\t@" + column.ColumnName + " " + column.DataType + extra.TrimEnd() + ",");
                                else
                                    yaz.WriteLine("\t@" + column.ColumnName + " " + column.DataType + extra.TrimEnd());
                            }

                            i++;
                        }

                        yaz.WriteLine("AS");
                        yaz.WriteLine("BEGIN");
                        yaz.WriteLine("\tSET NOCOUNT ON");
                        yaz.WriteLine("");

                        sqlText = "\tINSERT INTO " + schema + ".[" + Table + "] (";

                        foreach (ColumnInfo column in columnNames)
                        {
                            if (!column.IsIdentity)
                            {
                                sqlText = sqlText + "[" + column.ColumnName + "],";
                            }
                        }

                        sqlText = sqlText.Remove(sqlText.Length - 1);
                        sqlText = sqlText + ")";

                        yaz.WriteLine(sqlText);

                        sqlText = "\tSELECT ";

                        foreach (ColumnInfo column in columnNames)
                        {
                            if (!column.ColumnName.In(DeletedColumns, InType.ToUrlLower))
                            {
                                if (!column.IsIdentity)
                                {
                                    sqlText = sqlText + "@" + column.ColumnName + ",";
                                }
                            }
                            else
                            {
                                if (!column.IsIdentity)
                                {
                                    sqlText = sqlText + "0,";
                                }
                            }
                        }

                        sqlText = sqlText.Remove(sqlText.Length - 1);
                        sqlText = sqlText.Replace(",", ", ");

                        yaz.WriteLine(sqlText);

                        yaz.WriteLine("");
                        yaz.WriteLine("\tSELECT cast(@@IDENTITY as int)");
                        yaz.WriteLine("END;");
                        yaz.WriteLine("GO");
                        yaz.WriteLine("");
                        //Insert//

                        //Update//
                        yaz.WriteLine("/* Update */");
                        yaz.WriteLine("IF OBJECT_ID('" + schema + ".[usp_" + Table + "Update]') IS NOT NULL");
                        yaz.WriteLine("BEGIN");
                        yaz.WriteLine("\tDROP PROC " + schema + ".[usp_" + Table + "Update]");
                        yaz.WriteLine("END");
                        yaz.WriteLine("GO");
                        yaz.WriteLine("CREATE PROC " + schema + ".[usp_" + Table + "Update]");

                        i = 1;
                        foreach (ColumnInfo column in columnNames.Where(a => !a.ColumnName.In(DeletedColumns, InType.ToUrlLower) && !a.ColumnName.In(GuidColumns, InType.ToUrlLower)).ToList())
                        {
                            string extra = "";

                            if (column.DataType.In(diziML))
                                extra += column.MaxLength != "" ? "(" + column.MaxLength + ")" : "";

                            extra += column.IsNullable ? " = NULL" : "";

                            if (i != columnNames.Where(a => !a.ColumnName.In(DeletedColumns, InType.ToUrlLower) && !a.ColumnName.In(GuidColumns, InType.ToUrlLower)).ToList().Count)
                                yaz.WriteLine("\t@" + column.ColumnName + " " + column.DataType + extra.TrimEnd() + ",");
                            else
                                yaz.WriteLine("\t@" + column.ColumnName + " " + column.DataType + extra.TrimEnd());

                            i++;
                        }

                        yaz.WriteLine("AS");
                        yaz.WriteLine("\tSET NOCOUNT ON");
                        yaz.WriteLine("\tSET XACT_ABORT ON");
                        yaz.WriteLine("");
                        yaz.WriteLine("\tBEGIN TRAN");
                        yaz.WriteLine("");

                        yaz.WriteLine("\tUPDATE " + schema + ".[" + Table + "]");

                        sqlText = "\tSET ";

                        foreach (ColumnInfo column in columnNames)
                        {
                            if (!column.ColumnName.In(DeletedColumns, InType.ToUrlLower) && !column.ColumnName.In(GuidColumns, InType.ToUrlLower))
                            {
                                if (!column.IsIdentity)
                                {
                                    sqlText = sqlText + "[" + column.ColumnName + "] = @" + column.ColumnName + ",";
                                }
                            }
                        }

                        sqlText = sqlText.Remove(sqlText.Length - 1);

                        yaz.WriteLine(sqlText);

                        if (idType != null)
                        {
                            yaz.WriteLine("\tWHERE [" + idColumn + "] = @" + idColumn);
                        }

                        yaz.WriteLine("");

                        sqlText = "\tSELECT ";

                        foreach (ColumnInfo column in columnNames)
                        {
                            if (!column.ColumnName.In(DeletedColumns, InType.ToUrlLower))
                                sqlText = sqlText + "[" + column.ColumnName + "],";
                        }

                        sqlText = sqlText.Remove(sqlText.Length - 1);
                        sqlText = sqlText.Replace(",", ", ");

                        yaz.WriteLine(sqlText);

                        yaz.WriteLine("\tFROM " + schema + ".[" + Table + "]");

                        if (idType != null)
                        {
                            yaz.WriteLine("\tWHERE [" + idColumn + "] = @" + idColumn);
                        }

                        yaz.WriteLine("");
                        yaz.WriteLine("\tCOMMIT");
                        yaz.WriteLine("GO");
                        yaz.WriteLine("");
                        //Update//

                        //Copy//
                        yaz.WriteLine("/* Copy */");
                        yaz.WriteLine("IF OBJECT_ID('" + schema + ".[usp_" + Table + "Copy]') IS NOT NULL");
                        yaz.WriteLine("BEGIN");
                        yaz.WriteLine("\tDROP PROC " + schema + ".[usp_" + Table + "Copy]");
                        yaz.WriteLine("END");
                        yaz.WriteLine("GO");
                        yaz.WriteLine("CREATE PROC " + schema + ".[usp_" + Table + "Copy]");

                        if (idType != null)
                        {
                            yaz.WriteLine("\t@" + idColumn + " " + idType);
                        }

                        yaz.WriteLine("AS");
                        yaz.WriteLine("BEGIN");
                        yaz.WriteLine("\tSET NOCOUNT ON");
                        yaz.WriteLine("");

                        sqlText = "\tINSERT INTO " + schema + ".[" + Table + "] (";

                        foreach (ColumnInfo column in columnNames)
                        {
                            if (!column.IsIdentity)
                            {
                                sqlText = sqlText + "[" + column.ColumnName + "],";
                            }
                        }

                        sqlText = sqlText.Remove(sqlText.Length - 1);
                        sqlText = sqlText + ")";

                        yaz.WriteLine(sqlText);

                        sqlText = "\tSELECT ";

                        foreach (ColumnInfo column in columnNames)
                        {
                            if (!column.IsIdentity)
                            {
                                if (column.ColumnName.In(FileColumns, InType.ToUrlLower) || column.ColumnName.In(ImageColumns, InType.ToUrlLower))
                                {
                                    sqlText = sqlText + "'Kopya_' + A.[" + column.ColumnName + "],";
                                }
                                else if (column.ColumnName == searchText)
                                {
                                    sqlText = sqlText + "A.[" + column.ColumnName + "] + ' (Kopya)',";
                                }
                                else if (column.ColumnName.In(UrlColumns, InType.ToUrlLower))
                                {
                                    sqlText = sqlText + "A.[" + column.ColumnName + "] + '-(Kopya)',";
                                }
                                else
                                    sqlText = sqlText + "A.[" + column.ColumnName + "],";
                            }
                        }

                        sqlText = sqlText.Remove(sqlText.Length - 1);
                        sqlText += " FROM " + schema + ".[" + Table + "] A WHERE A.[" + idColumn + "] = @" + idColumn;
                        sqlText = sqlText.Replace(",", ", ");

                        yaz.WriteLine(sqlText);
                        yaz.WriteLine("");
                        yaz.WriteLine("\tSELECT cast(@@IDENTITY as int)");
                        yaz.WriteLine("END;");
                        yaz.WriteLine("GO");
                        yaz.WriteLine("");
                        //Copy//

                        //Delete//
                        yaz.WriteLine("/* Delete */");
                        yaz.WriteLine("IF OBJECT_ID('" + schema + ".[usp_" + Table + "Delete]') IS NOT NULL");
                        yaz.WriteLine("BEGIN");
                        yaz.WriteLine("\tDROP PROC " + schema + ".[usp_" + Table + "Delete]");
                        yaz.WriteLine("END");
                        yaz.WriteLine("GO");
                        yaz.WriteLine("CREATE PROC " + schema + ".[usp_" + Table + "Delete]");

                        if (idType != null)
                        {
                            yaz.WriteLine("\t@" + idColumn + " " + idType);
                        }

                        yaz.WriteLine("AS");
                        yaz.WriteLine("\tSET NOCOUNT ON");
                        yaz.WriteLine("\tSET XACT_ABORT ON");
                        yaz.WriteLine("");
                        yaz.WriteLine("\tBEGIN TRAN");
                        yaz.WriteLine("");

                        yaz.WriteLine("\tDELETE");
                        yaz.WriteLine("\tFROM " + schema + ".[" + Table + "]");

                        if (idType != null)
                        {
                            yaz.WriteLine("\tWHERE [" + idColumn + "] = @" + idColumn);
                        }

                        yaz.WriteLine("");
                        yaz.WriteLine("\tCOMMIT");
                        yaz.WriteLine("GO");
                        yaz.WriteLine("");
                        //Delete//

                        //Remove//
                        if (deleted != "")
                        {
                            yaz.WriteLine("/* Remove */");
                            yaz.WriteLine("IF OBJECT_ID('" + schema + ".[usp_" + Table + "Remove]') IS NOT NULL");
                            yaz.WriteLine("BEGIN");
                            yaz.WriteLine("\tDROP PROC " + schema + ".[usp_" + Table + "Remove]");
                            yaz.WriteLine("END");
                            yaz.WriteLine("GO");
                            yaz.WriteLine("CREATE PROC " + schema + ".[usp_" + Table + "Remove]");

                            if (idType != null)
                            {
                                yaz.WriteLine("\t@" + idColumn + " " + idType);
                            }

                            yaz.WriteLine("AS");
                            yaz.WriteLine("\tSET NOCOUNT ON");
                            yaz.WriteLine("\tSET XACT_ABORT ON");
                            yaz.WriteLine("");
                            yaz.WriteLine("\tBEGIN TRAN");
                            yaz.WriteLine("");

                            yaz.WriteLine("\tUPDATE " + schema + ".[" + Table + "] SET [Deleted] = 1");

                            if (idType != null)
                            {
                                yaz.WriteLine("\tWHERE [" + idColumn + "] = @" + idColumn);
                            }

                            yaz.WriteLine("");
                            yaz.WriteLine("\tCOMMIT");
                            yaz.WriteLine("GO");
                            yaz.WriteLine("");
                        }
                        //Remove//
                    }

                    yaz.Close();
                }
            }

            CreateSplitStoredProcedure();
        }

        void CreateSplitStoredProcedure()
        {
            string schema = DefaultSchema(new SqlConnection(Helper.Helper.CreateConnectionText(connectionInfo)));

            foreach (string Table in selectedTables)
            {
                using (FileStream fs = new FileStream(PathAddress + "\\" + projectFolder + "\\StoredProcedures\\" + Table + ".sql", FileMode.Create))
                {
                    using (StreamWriter yaz = new StreamWriter(fs, Encoding.Unicode))
                    {
                        yaz.WriteLine("USE [" + DBName + "]");
                        yaz.WriteLine("GO");
                        yaz.WriteLine("");

                        List<string> identityColumns = Helper.Helper.ReturnIdentityColumn(connectionInfo, Table);
                        string idColumn = identityColumns.FirstOrDefault();

                        SqlConnection con = new SqlConnection(Helper.Helper.CreateConnectionText(connectionInfo));
                        List<ForeignKeyChecker> fkcList = ForeignKeyCheck(con, Table);
                        fkcList = fkcList.Where(a => a.PrimaryTableName == Table).ToList();

                        List<ForeignKeyChecker> fkcListForeign = ForeignKeyCheck(con);
                        fkcListForeign = fkcListForeign.Where(a => a.ForeignTableName == Table).ToList();

                        string[] diziML = new string[] { "nvarchar", "varchar", "binary", "char", "nchar", "varbinary" };

                        int i = 0;

                        List<ColumnInfo> columnNames = TableColumns(Table);
                        List<ColumnInfo> guidColumns = TableColumns(Table, ColumnType.GuidColumns);
                        List<ColumnInfo> urlColumns = TableColumns(Table, ColumnType.UrlColumns);
                        List<ColumnInfo> codeColumns = TableColumns(Table, ColumnType.CodeColumns);
                        List<ColumnInfo> searchColumns = TableColumns(Table, ColumnType.SearchColumns);
                        string deleted = columnNames.Where(a => a.ColumnName.In(DeletedColumns, InType.ToUrlLower)).ToList().Count > 0 ? " and [Deleted] = 0" : "";

                        string idType = null;

                        string searchText = GetColumnText(tableColumnInfos.Where(a => a.TableName == Table).ToList());

                        if (searchText.Contains(".ToString()"))
                            searchText = "";

                        try
                        {
                            idType = columnNames.Where(a => a.ColumnName == idColumn).FirstOrDefault().DataType;
                        }
                        catch
                        {
                        }

                        //Select//
                        yaz.WriteLine("/* Select */");
                        yaz.WriteLine("IF OBJECT_ID('" + schema + ".[usp_" + Table + "Select]') IS NOT NULL");
                        yaz.WriteLine("BEGIN");
                        yaz.WriteLine("\tDROP PROC " + schema + ".[usp_" + Table + "Select]");
                        yaz.WriteLine("END");
                        yaz.WriteLine("GO");
                        yaz.WriteLine("CREATE PROC " + schema + ".[usp_" + Table + "Select]");

                        if (idType != null)
                        {
                            yaz.WriteLine("\t@" + idColumn + " " + idType);
                        }

                        yaz.WriteLine("AS");
                        yaz.WriteLine("\tSET NOCOUNT ON");
                        yaz.WriteLine("\tSET XACT_ABORT ON");
                        yaz.WriteLine("");
                        yaz.WriteLine("\tBEGIN TRAN");
                        yaz.WriteLine("");

                        string sqlText = "\tSELECT ";

                        foreach (ColumnInfo column in columnNames)
                        {
                            if (!column.ColumnName.In(DeletedColumns, InType.ToUrlLower))
                                sqlText += "[" + column.ColumnName + "],";
                        }

                        sqlText = sqlText.Remove(sqlText.Length - 1);
                        sqlText = sqlText.Replace(",", ", ");

                        yaz.WriteLine(sqlText);
                        yaz.WriteLine("\tFROM " + schema + ".[" + Table + "]");

                        if (idType != null)
                        {
                            yaz.WriteLine("\tWHERE ([" + idColumn + "] = @" + idColumn + " OR @" + idColumn + " IS NULL)" + deleted);
                        }

                        yaz.WriteLine("");
                        yaz.WriteLine("\tCOMMIT");
                        yaz.WriteLine("GO");
                        yaz.WriteLine("");
                        //Select//

                        //SelectTop//
                        yaz.WriteLine("/* SelectTop */");
                        yaz.WriteLine("IF OBJECT_ID('" + schema + ".[usp_" + Table + "SelectTop]') IS NOT NULL");
                        yaz.WriteLine("BEGIN");
                        yaz.WriteLine("\tDROP PROC " + schema + ".[usp_" + Table + "SelectTop]");
                        yaz.WriteLine("END");
                        yaz.WriteLine("GO");
                        yaz.WriteLine("CREATE PROC " + schema + ".[usp_" + Table + "SelectTop]");

                        if (idType != null)
                        {
                            yaz.WriteLine("\t@" + idColumn + " " + idType + ",");
                        }
                        yaz.WriteLine("\t@Top int");

                        yaz.WriteLine("AS");
                        yaz.WriteLine("\tSET NOCOUNT ON");
                        yaz.WriteLine("\tSET XACT_ABORT ON");
                        yaz.WriteLine("");
                        yaz.WriteLine("\tBEGIN TRAN");
                        yaz.WriteLine("");

                        sqlText = "\tSELECT Top (@Top) ";

                        foreach (ColumnInfo column in columnNames)
                        {
                            if (!column.ColumnName.In(DeletedColumns, InType.ToUrlLower))
                                sqlText += "[" + column.ColumnName + "],";
                        }

                        sqlText = sqlText.Remove(sqlText.Length - 1);
                        sqlText = sqlText.Replace(",", ", ");

                        yaz.WriteLine(sqlText);
                        yaz.WriteLine("\tFROM " + schema + ".[" + Table + "]");

                        if (idType != null)
                        {
                            yaz.WriteLine("\tWHERE ([" + idColumn + "] = @" + idColumn + " OR @" + idColumn + " IS NULL)" + deleted);
                        }

                        yaz.WriteLine("");
                        yaz.WriteLine("\tCOMMIT");
                        yaz.WriteLine("GO");
                        yaz.WriteLine("");
                        //SelectTop//

                        foreach (ColumnInfo item in urlColumns)
                        {
                            //SelectByUrl//
                            yaz.WriteLine("/* SelectBy" + item.ColumnName + " */");
                            yaz.WriteLine("IF OBJECT_ID('" + schema + ".[usp_" + Table + "SelectBy" + item.ColumnName + "]') IS NOT NULL");
                            yaz.WriteLine("BEGIN");
                            yaz.WriteLine("\tDROP PROC " + schema + ".[usp_" + Table + "SelectBy" + item.ColumnName + "]");
                            yaz.WriteLine("END");
                            yaz.WriteLine("GO");
                            yaz.WriteLine("CREATE PROC " + schema + ".[usp_" + Table + "SelectBy" + item.ColumnName + "]");
                            yaz.WriteLine("\t@" + item.ColumnName + " " + item.DataType + "(" + item.MaxLength + ")");
                            yaz.WriteLine("AS");
                            yaz.WriteLine("\tSET NOCOUNT ON");
                            yaz.WriteLine("\tSET XACT_ABORT ON");
                            yaz.WriteLine("");
                            yaz.WriteLine("\tBEGIN TRAN");
                            yaz.WriteLine("");

                            sqlText = "\tSELECT Top (1) ";

                            foreach (ColumnInfo column in columnNames)
                            {
                                if (!column.ColumnName.In(DeletedColumns, InType.ToUrlLower))
                                    sqlText += "[" + column.ColumnName + "],";
                            }

                            sqlText = sqlText.Remove(sqlText.Length - 1);
                            sqlText = sqlText.Replace(",", ", ");

                            yaz.WriteLine(sqlText);
                            yaz.WriteLine("\tFROM " + schema + ".[" + Table + "]");
                            yaz.WriteLine("\tWHERE ([" + item.ColumnName + "] = @" + item.ColumnName + " OR @" + item.ColumnName + " IS NULL)" + deleted);
                            yaz.WriteLine("");
                            yaz.WriteLine("\tCOMMIT");
                            yaz.WriteLine("GO");
                            yaz.WriteLine("");
                            //SelectByUrl//
                        }

                        foreach (ColumnInfo item in guidColumns)
                        {
                            //SelectByGuid//
                            yaz.WriteLine("/* SelectBy" + item.ColumnName + " */");
                            yaz.WriteLine("IF OBJECT_ID('" + schema + ".[usp_" + Table + "SelectBy" + item.ColumnName + "]') IS NOT NULL");
                            yaz.WriteLine("BEGIN");
                            yaz.WriteLine("\tDROP PROC " + schema + ".[usp_" + Table + "SelectBy" + item.ColumnName + "]");
                            yaz.WriteLine("END");
                            yaz.WriteLine("GO");
                            yaz.WriteLine("CREATE PROC " + schema + ".[usp_" + Table + "SelectBy" + item.ColumnName + "]");
                            yaz.WriteLine("\t@" + item.ColumnName + " " + item.DataType + "(" + item.MaxLength + ")");
                            yaz.WriteLine("AS");
                            yaz.WriteLine("\tSET NOCOUNT ON");
                            yaz.WriteLine("\tSET XACT_ABORT ON");
                            yaz.WriteLine("");
                            yaz.WriteLine("\tBEGIN TRAN");
                            yaz.WriteLine("");

                            sqlText = "\tSELECT Top (1) ";

                            foreach (ColumnInfo column in columnNames)
                            {
                                if (!column.ColumnName.In(DeletedColumns, InType.ToUrlLower))
                                    sqlText += "[" + column.ColumnName + "],";
                            }

                            sqlText = sqlText.Remove(sqlText.Length - 1);
                            sqlText = sqlText.Replace(",", ", ");

                            yaz.WriteLine(sqlText);
                            yaz.WriteLine("\tFROM " + schema + ".[" + Table + "]");
                            yaz.WriteLine("\tWHERE ([" + item.ColumnName + "] = @" + item.ColumnName + " OR @" + item.ColumnName + " IS NULL)" + deleted);
                            yaz.WriteLine("");
                            yaz.WriteLine("\tCOMMIT");
                            yaz.WriteLine("GO");
                            yaz.WriteLine("");
                            //SelectByGuid//
                        }

                        foreach (ColumnInfo item in codeColumns)
                        {
                            //SelectByCode//
                            yaz.WriteLine("/* SelectBy" + item.ColumnName + " */");
                            yaz.WriteLine("IF OBJECT_ID('" + schema + ".[usp_" + Table + "SelectBy" + item.ColumnName + "]') IS NOT NULL");
                            yaz.WriteLine("BEGIN");
                            yaz.WriteLine("\tDROP PROC " + schema + ".[usp_" + Table + "SelectBy" + item.ColumnName + "]");
                            yaz.WriteLine("END");
                            yaz.WriteLine("GO");
                            yaz.WriteLine("CREATE PROC " + schema + ".[usp_" + Table + "SelectBy" + item.ColumnName + "]");
                            yaz.WriteLine("\t@" + item.ColumnName + " " + item.DataType + "(" + item.MaxLength + ")");
                            yaz.WriteLine("AS");
                            yaz.WriteLine("\tSET NOCOUNT ON");
                            yaz.WriteLine("\tSET XACT_ABORT ON");
                            yaz.WriteLine("");
                            yaz.WriteLine("\tBEGIN TRAN");
                            yaz.WriteLine("");

                            sqlText = "\tSELECT ";

                            foreach (ColumnInfo column in columnNames)
                            {
                                if (!column.ColumnName.In(DeletedColumns, InType.ToUrlLower))
                                    sqlText += "[" + column.ColumnName + "],";
                            }

                            sqlText = sqlText.Remove(sqlText.Length - 1);
                            sqlText = sqlText.Replace(",", ", ");

                            yaz.WriteLine(sqlText);
                            yaz.WriteLine("\tFROM " + schema + ".[" + Table + "]");
                            yaz.WriteLine("\tWHERE ([" + item.ColumnName + "] = @" + item.ColumnName + " OR @" + item.ColumnName + " IS NULL)" + deleted);
                            yaz.WriteLine("");
                            yaz.WriteLine("\tCOMMIT");
                            yaz.WriteLine("GO");
                            yaz.WriteLine("");
                            //SelectByCode//
                        }

                        foreach (ColumnInfo item in searchColumns)
                        {
                            //SelectBySearch//
                            yaz.WriteLine("/* SelectBy" + item.ColumnName + " */");
                            yaz.WriteLine("IF OBJECT_ID('" + schema + ".[usp_" + Table + "SelectBy" + item.ColumnName + "]') IS NOT NULL");
                            yaz.WriteLine("BEGIN");
                            yaz.WriteLine("\tDROP PROC " + schema + ".[usp_" + Table + "SelectBy" + item.ColumnName + "]");
                            yaz.WriteLine("END");
                            yaz.WriteLine("GO");
                            yaz.WriteLine("CREATE PROC " + schema + ".[usp_" + Table + "SelectBy" + item.ColumnName + "]");

                            if (item.Type.Name == "String")
                            {
                                yaz.WriteLine("\t@" + item.ColumnName + " " + item.DataType + "(" + item.MaxLength + ")");
                            }
                            else
                            {
                                yaz.WriteLine("\t@" + item.ColumnName + " " + item.DataType);
                            }

                            yaz.WriteLine("AS");
                            yaz.WriteLine("\tSET NOCOUNT ON");
                            yaz.WriteLine("\tSET XACT_ABORT ON");
                            yaz.WriteLine("");
                            yaz.WriteLine("\tBEGIN TRAN");
                            yaz.WriteLine("");

                            sqlText = "\tSELECT ";

                            foreach (ColumnInfo column in columnNames)
                            {
                                if (!column.ColumnName.In(DeletedColumns, InType.ToUrlLower))
                                    sqlText += "[" + column.ColumnName + "],";
                            }

                            sqlText = sqlText.Remove(sqlText.Length - 1);
                            sqlText = sqlText.Replace(",", ", ");

                            yaz.WriteLine(sqlText);
                            yaz.WriteLine("\tFROM " + schema + ".[" + Table + "]");

                            string whereClause;

                            if (item.Type.Name == "String")
                            {
                                whereClause = "\tWHERE ([" + item.ColumnName + "] LIKE '%' + @" + item.ColumnName + " + '%' OR @" + item.ColumnName + " IS NULL)" + deleted;
                            }
                            else
                            {
                                whereClause = "\tWHERE ([" + item.ColumnName + "] = @" + item.ColumnName + " OR @" + item.ColumnName + " IS NULL)" + deleted;
                            }

                            yaz.WriteLine(whereClause);
                            yaz.WriteLine("");
                            yaz.WriteLine("\tCOMMIT");
                            yaz.WriteLine("GO");
                            yaz.WriteLine("");
                            //SelectBySearch//
                        }

                        //LinkedSelect//
                        if (fkcListForeign.Count > 0)
                        {
                            yaz.WriteLine("/* LinkedSelect */");
                            yaz.WriteLine("IF OBJECT_ID('" + schema + ".[usp_" + Table + "LinkedSelect]') IS NOT NULL");
                            yaz.WriteLine("BEGIN");
                            yaz.WriteLine("\tDROP PROC " + schema + ".[usp_" + Table + "LinkedSelect]");
                            yaz.WriteLine("END");
                            yaz.WriteLine("GO");
                            yaz.WriteLine("CREATE PROC " + schema + ".[usp_" + Table + "LinkedSelect]");

                            if (idType != null)
                            {
                                yaz.WriteLine("\t@" + idColumn + " " + idType);
                            }

                            yaz.WriteLine("AS");
                            yaz.WriteLine("\tSET NOCOUNT ON");
                            yaz.WriteLine("\tSET XACT_ABORT ON");
                            yaz.WriteLine("");
                            yaz.WriteLine("\tBEGIN TRAN");
                            yaz.WriteLine("");

                            sqlText = "\tSELECT ";

                            foreach (ColumnInfo column in columnNames)
                            {
                                if (!column.ColumnName.In(DeletedColumns, InType.ToUrlLower))
                                    sqlText += "[" + column.ColumnName + "],";
                            }

                            yaz.WriteLine(sqlText);

                            i = 0;
                            int fkcCount = fkcListForeign.GroupBy(a => a.PrimaryTableName).Select(a => a.First()).ToList().Count;
                            foreach (ForeignKeyChecker fkc in fkcListForeign.GroupBy(a => a.PrimaryTableName).Select(a => a.First()).ToList())
                            {
                                sqlText = "";

                                string PrimaryTableName = fkc.PrimaryTableName;
                                string columnText = GetColumnText(tableColumnInfos.Where(a => a.TableName == PrimaryTableName).ToList()).Replace(".ToString()", "");

                                sqlText += "\t\t(SELECT " + aliases[i % 10] + "." + columnText + " FROM " + PrimaryTableName + " " + aliases[i % 10] + " WHERE " + aliases[i % 10] + "." + fkc.PrimaryColumnName + " = " + fkc.ForeignColumnName + ") as " + PrimaryTableName + "Adi,";

                                if (fkcCount == i + 1)
                                {
                                    sqlText = sqlText.Remove(sqlText.Length - 1);
                                    sqlText = sqlText.Replace(",", ", ");
                                }

                                yaz.WriteLine(sqlText);

                                i++;
                            }

                            yaz.WriteLine("\tFROM " + schema + ".[" + Table + "]");

                            if (idType != null)
                            {
                                yaz.WriteLine("\tWHERE ([" + idColumn + "] = @" + idColumn + " OR @" + idColumn + " IS NULL)" + deleted);
                            }

                            yaz.WriteLine("");
                            yaz.WriteLine("\tCOMMIT");
                            yaz.WriteLine("GO");
                            yaz.WriteLine("");
                        }
                        //LinkedSelect//

                        //ByLinkedIDSelect//
                        if (fkcList.Count > 0)
                        {
                            foreach (ForeignKeyChecker fkc in fkcList.GroupBy(a => a.PrimaryTableName).Select(a => a.First()).ToList())
                            {
                                foreach (ForeignKeyChecker fkc2 in fkcList.GroupBy(a => a.ForeignTableName).Select(a => a.First()).ToList())
                                {
                                    string PrimaryTableName = fkc.PrimaryTableName;
                                    string ForeignTableName = fkc2.ForeignTableName;
                                    string columnText = GetColumnText(tableColumnInfos.Where(a => a.TableName == Table).ToList()).Replace(".ToString()", "");

                                    List<ColumnInfo> fColumnNames = Helper.Helper.GetColumnsInfo(connectionInfo, ForeignTableName).ToList();
                                    string fDeleted = fColumnNames.Where(a => a.ColumnName.In(DeletedColumns, InType.ToUrlLower)).ToList().Count > 0 ? " and [Deleted] = 0" : "";

                                    yaz.WriteLine("/* ByLinkedIDSelect */");
                                    yaz.WriteLine("IF OBJECT_ID('" + schema + ".[usp_" + ForeignTableName + "_" + PrimaryTableName + "ByLinkedIDSelect]') IS NOT NULL");
                                    yaz.WriteLine("BEGIN");
                                    yaz.WriteLine("\tDROP PROC " + schema + ".[usp_" + ForeignTableName + "_" + PrimaryTableName + "ByLinkedIDSelect]");
                                    yaz.WriteLine("END");
                                    yaz.WriteLine("GO");
                                    yaz.WriteLine("CREATE PROC " + schema + ".[usp_" + ForeignTableName + "_" + PrimaryTableName + "ByLinkedIDSelect]");

                                    string fidType = null;
                                    try
                                    {
                                        fidType = fColumnNames.Where(a => a.ColumnName == fkc2.ForeignColumnName).FirstOrDefault().DataType;
                                    }
                                    catch
                                    {
                                    }

                                    if (fidType != null)
                                    {
                                        yaz.WriteLine("\t@" + fkc2.ForeignColumnName + " " + fidType);
                                    }

                                    yaz.WriteLine("AS");
                                    yaz.WriteLine("\tSET NOCOUNT ON");
                                    yaz.WriteLine("\tSET XACT_ABORT ON");
                                    yaz.WriteLine("");
                                    yaz.WriteLine("\tBEGIN TRAN");
                                    yaz.WriteLine("");

                                    sqlText = "\tSELECT ";

                                    foreach (ColumnInfo column in fColumnNames)
                                    {
                                        if (!column.ColumnName.In(DeletedColumns, InType.ToUrlLower))
                                            sqlText += "[" + column.ColumnName + "],";
                                    }

                                    yaz.WriteLine(sqlText);

                                    sqlText = "";

                                    sqlText += "\t(SELECT A." + columnText + " FROM " + Table + " A WHERE A." + fkc.PrimaryColumnName + " = " + fkc2.ForeignColumnName + ") as " + Table + "Adi";

                                    yaz.WriteLine(sqlText);

                                    yaz.WriteLine("\tFROM " + schema + ".[" + ForeignTableName + "]");

                                    yaz.WriteLine("\tWHERE ([" + fkc2.ForeignColumnName + "] = @" + fkc2.ForeignColumnName + " OR @" + fkc2.ForeignColumnName + " IS NULL)" + fDeleted);

                                    yaz.WriteLine("");
                                    yaz.WriteLine("\tCOMMIT");
                                    yaz.WriteLine("GO");
                                    yaz.WriteLine("");
                                }
                            }
                        }
                        //ByLinkedIDSelect//

                        //SelectAll//
                        yaz.WriteLine("/* SelectAll */");
                        yaz.WriteLine("IF OBJECT_ID('" + schema + ".[usp_" + Table + "SelectAll]') IS NOT NULL");
                        yaz.WriteLine("BEGIN");
                        yaz.WriteLine("\tDROP PROC " + schema + ".[usp_" + Table + "SelectAll]");
                        yaz.WriteLine("END");
                        yaz.WriteLine("GO");
                        yaz.WriteLine("CREATE PROC " + schema + ".[usp_" + Table + "SelectAll]");

                        if (idType != null)
                        {
                            yaz.WriteLine("\t@" + idColumn + " " + idType);
                        }

                        yaz.WriteLine("AS");
                        yaz.WriteLine("\tSET NOCOUNT ON");
                        yaz.WriteLine("\tSET XACT_ABORT ON");
                        yaz.WriteLine("");
                        yaz.WriteLine("\tBEGIN TRAN");
                        yaz.WriteLine("");

                        sqlText = "\tSELECT ";

                        foreach (ColumnInfo column in columnNames)
                        {
                            sqlText += "[" + column.ColumnName + "],";
                        }

                        sqlText = sqlText.Remove(sqlText.Length - 1);
                        sqlText = sqlText.Replace(",", ", ");

                        yaz.WriteLine(sqlText);
                        yaz.WriteLine("\tFROM " + schema + ".[" + Table + "]");

                        if (idType != null)
                        {
                            yaz.WriteLine("\tWHERE ([" + idColumn + "] = @" + idColumn + " OR @" + idColumn + " IS NULL)");
                        }

                        yaz.WriteLine("");
                        yaz.WriteLine("\tCOMMIT");
                        yaz.WriteLine("GO");
                        yaz.WriteLine("");
                        //SelectAll//

                        //Insert//
                        yaz.WriteLine("/* Insert */");
                        yaz.WriteLine("IF OBJECT_ID('" + schema + ".[usp_" + Table + "Insert]') IS NOT NULL");
                        yaz.WriteLine("BEGIN");
                        yaz.WriteLine("\tDROP PROC " + schema + ".[usp_" + Table + "Insert]");
                        yaz.WriteLine("END");
                        yaz.WriteLine("GO");
                        yaz.WriteLine("CREATE PROC " + schema + ".[usp_" + Table + "Insert]");

                        i = 1;
                        foreach (ColumnInfo column in columnNames.Where(a => !a.ColumnName.In(DeletedColumns, InType.ToUrlLower)).ToList())
                        {
                            if (!column.IsIdentity)
                            {
                                string extra = "";

                                if (column.DataType.In(diziML))
                                    extra += column.MaxLength != "" ? "(" + column.MaxLength + ")" : "";

                                extra += column.IsNullable ? " = NULL" : "";

                                if (i != columnNames.Where(a => !a.ColumnName.In(DeletedColumns, InType.ToUrlLower)).ToList().Count)
                                    yaz.WriteLine("\t@" + column.ColumnName + " " + column.DataType + extra.TrimEnd() + ",");
                                else
                                    yaz.WriteLine("\t@" + column.ColumnName + " " + column.DataType + extra.TrimEnd());
                            }

                            i++;
                        }

                        yaz.WriteLine("AS");
                        yaz.WriteLine("BEGIN");
                        yaz.WriteLine("\tSET NOCOUNT ON");
                        yaz.WriteLine("");

                        sqlText = "\tINSERT INTO " + schema + ".[" + Table + "] (";

                        foreach (ColumnInfo column in columnNames)
                        {
                            if (!column.IsIdentity)
                            {
                                sqlText = sqlText + "[" + column.ColumnName + "],";
                            }
                        }

                        sqlText = sqlText.Remove(sqlText.Length - 1);
                        sqlText = sqlText + ")";

                        yaz.WriteLine(sqlText);

                        sqlText = "\tSELECT ";

                        foreach (ColumnInfo column in columnNames)
                        {
                            if (!column.ColumnName.In(DeletedColumns, InType.ToUrlLower))
                            {
                                if (!column.IsIdentity)
                                {
                                    sqlText = sqlText + "@" + column.ColumnName + ",";
                                }
                            }
                            else
                            {
                                if (!column.IsIdentity)
                                {
                                    sqlText = sqlText + "0,";
                                }
                            }
                        }

                        sqlText = sqlText.Remove(sqlText.Length - 1);
                        sqlText = sqlText.Replace(",", ", ");

                        yaz.WriteLine(sqlText);

                        yaz.WriteLine("");
                        yaz.WriteLine("\tSELECT cast(@@IDENTITY as int)");
                        yaz.WriteLine("END;");
                        yaz.WriteLine("GO");
                        yaz.WriteLine("");
                        //Insert//

                        //Update//
                        yaz.WriteLine("/* Update */");
                        yaz.WriteLine("IF OBJECT_ID('" + schema + ".[usp_" + Table + "Update]') IS NOT NULL");
                        yaz.WriteLine("BEGIN");
                        yaz.WriteLine("\tDROP PROC " + schema + ".[usp_" + Table + "Update]");
                        yaz.WriteLine("END");
                        yaz.WriteLine("GO");
                        yaz.WriteLine("CREATE PROC " + schema + ".[usp_" + Table + "Update]");

                        i = 1;
                        foreach (ColumnInfo column in columnNames.Where(a => !a.ColumnName.In(DeletedColumns, InType.ToUrlLower) && !a.ColumnName.In(GuidColumns, InType.ToUrlLower)).ToList())
                        {
                            string extra = "";

                            if (column.DataType.In(diziML))
                                extra += column.MaxLength != "" ? "(" + column.MaxLength + ")" : "";

                            extra += column.IsNullable ? " = NULL" : "";

                            if (i != columnNames.Where(a => !a.ColumnName.In(DeletedColumns, InType.ToUrlLower) && !a.ColumnName.In(GuidColumns, InType.ToUrlLower)).ToList().Count)
                                yaz.WriteLine("\t@" + column.ColumnName + " " + column.DataType + extra.TrimEnd() + ",");
                            else
                                yaz.WriteLine("\t@" + column.ColumnName + " " + column.DataType + extra.TrimEnd());

                            i++;
                        }

                        yaz.WriteLine("AS");
                        yaz.WriteLine("\tSET NOCOUNT ON");
                        yaz.WriteLine("\tSET XACT_ABORT ON");
                        yaz.WriteLine("");
                        yaz.WriteLine("\tBEGIN TRAN");
                        yaz.WriteLine("");

                        yaz.WriteLine("\tUPDATE " + schema + ".[" + Table + "]");

                        sqlText = "\tSET ";

                        foreach (ColumnInfo column in columnNames)
                        {
                            if (!column.ColumnName.In(DeletedColumns, InType.ToUrlLower) && !column.ColumnName.In(GuidColumns, InType.ToUrlLower))
                            {
                                if (!column.IsIdentity)
                                {
                                    sqlText = sqlText + "[" + column.ColumnName + "] = @" + column.ColumnName + ",";
                                }
                            }
                        }

                        sqlText = sqlText.Remove(sqlText.Length - 1);

                        yaz.WriteLine(sqlText);

                        if (idType != null)
                        {
                            yaz.WriteLine("\tWHERE [" + idColumn + "] = @" + idColumn);
                        }

                        yaz.WriteLine("");

                        sqlText = "\tSELECT ";

                        foreach (ColumnInfo column in columnNames)
                        {
                            if (!column.ColumnName.In(DeletedColumns, InType.ToUrlLower))
                                sqlText = sqlText + "[" + column.ColumnName + "],";
                        }

                        sqlText = sqlText.Remove(sqlText.Length - 1);
                        sqlText = sqlText.Replace(",", ", ");

                        yaz.WriteLine(sqlText);

                        yaz.WriteLine("\tFROM " + schema + ".[" + Table + "]");

                        if (idType != null)
                        {
                            yaz.WriteLine("\tWHERE [" + idColumn + "] = @" + idColumn);
                        }

                        yaz.WriteLine("");
                        yaz.WriteLine("\tCOMMIT");
                        yaz.WriteLine("GO");
                        yaz.WriteLine("");
                        //Update//

                        //Copy//
                        yaz.WriteLine("/* Copy */");
                        yaz.WriteLine("IF OBJECT_ID('" + schema + ".[usp_" + Table + "Copy]') IS NOT NULL");
                        yaz.WriteLine("BEGIN");
                        yaz.WriteLine("\tDROP PROC " + schema + ".[usp_" + Table + "Copy]");
                        yaz.WriteLine("END");
                        yaz.WriteLine("GO");
                        yaz.WriteLine("CREATE PROC " + schema + ".[usp_" + Table + "Copy]");

                        if (idType != null)
                        {
                            yaz.WriteLine("\t@" + idColumn + " " + idType);
                        }

                        yaz.WriteLine("AS");
                        yaz.WriteLine("BEGIN");
                        yaz.WriteLine("\tSET NOCOUNT ON");
                        yaz.WriteLine("");

                        sqlText = "\tINSERT INTO " + schema + ".[" + Table + "] (";

                        foreach (ColumnInfo column in columnNames)
                        {
                            if (!column.IsIdentity)
                            {
                                sqlText = sqlText + "[" + column.ColumnName + "],";
                            }
                        }

                        sqlText = sqlText.Remove(sqlText.Length - 1);
                        sqlText = sqlText + ")";

                        yaz.WriteLine(sqlText);

                        sqlText = "\tSELECT ";

                        foreach (ColumnInfo column in columnNames)
                        {
                            if (!column.IsIdentity)
                            {
                                if (column.ColumnName.In(FileColumns, InType.ToUrlLower) || column.ColumnName.In(ImageColumns, InType.ToUrlLower))
                                {
                                    sqlText = sqlText + "'Kopya_' + A.[" + column.ColumnName + "],";
                                }
                                else if (column.ColumnName == searchText)
                                {
                                    sqlText = sqlText + "A.[" + column.ColumnName + "] + ' (Kopya)',";
                                }
                                else if (column.ColumnName.In(UrlColumns, InType.ToUrlLower))
                                {
                                    sqlText = sqlText + "A.[" + column.ColumnName + "] + '-(Kopya)',";
                                }
                                else
                                    sqlText = sqlText + "A.[" + column.ColumnName + "],";
                            }
                        }

                        sqlText = sqlText.Remove(sqlText.Length - 1);
                        sqlText += " FROM " + schema + ".[" + Table + "] A WHERE A.[" + idColumn + "] = @" + idColumn;
                        sqlText = sqlText.Replace(",", ", ");

                        yaz.WriteLine(sqlText);
                        yaz.WriteLine("");
                        yaz.WriteLine("\tSELECT cast(@@IDENTITY as int)");
                        yaz.WriteLine("END;");
                        yaz.WriteLine("GO");
                        yaz.WriteLine("");
                        //Copy//

                        //Delete//
                        yaz.WriteLine("/* Delete */");
                        yaz.WriteLine("IF OBJECT_ID('" + schema + ".[usp_" + Table + "Delete]') IS NOT NULL");
                        yaz.WriteLine("BEGIN");
                        yaz.WriteLine("\tDROP PROC " + schema + ".[usp_" + Table + "Delete]");
                        yaz.WriteLine("END");
                        yaz.WriteLine("GO");
                        yaz.WriteLine("CREATE PROC " + schema + ".[usp_" + Table + "Delete]");

                        if (idType != null)
                        {
                            yaz.WriteLine("\t@" + idColumn + " " + idType);
                        }

                        yaz.WriteLine("AS");
                        yaz.WriteLine("\tSET NOCOUNT ON");
                        yaz.WriteLine("\tSET XACT_ABORT ON");
                        yaz.WriteLine("");
                        yaz.WriteLine("\tBEGIN TRAN");
                        yaz.WriteLine("");

                        yaz.WriteLine("\tDELETE");
                        yaz.WriteLine("\tFROM " + schema + ".[" + Table + "]");

                        if (idType != null)
                        {
                            yaz.WriteLine("\tWHERE [" + idColumn + "] = @" + idColumn);
                        }

                        yaz.WriteLine("");
                        yaz.WriteLine("\tCOMMIT");
                        yaz.WriteLine("GO");
                        yaz.WriteLine("");
                        //Delete//

                        //Remove//
                        if (deleted != "")
                        {
                            yaz.WriteLine("/* Remove */");
                            yaz.WriteLine("IF OBJECT_ID('" + schema + ".[usp_" + Table + "Remove]') IS NOT NULL");
                            yaz.WriteLine("BEGIN");
                            yaz.WriteLine("\tDROP PROC " + schema + ".[usp_" + Table + "Remove]");
                            yaz.WriteLine("END");
                            yaz.WriteLine("GO");
                            yaz.WriteLine("CREATE PROC " + schema + ".[usp_" + Table + "Remove]");

                            if (idType != null)
                            {
                                yaz.WriteLine("\t@" + idColumn + " " + idType);
                            }

                            yaz.WriteLine("AS");
                            yaz.WriteLine("\tSET NOCOUNT ON");
                            yaz.WriteLine("\tSET XACT_ABORT ON");
                            yaz.WriteLine("");
                            yaz.WriteLine("\tBEGIN TRAN");
                            yaz.WriteLine("");

                            yaz.WriteLine("\tUPDATE " + schema + ".[" + Table + "] SET [Deleted] = 1");

                            if (idType != null)
                            {
                                yaz.WriteLine("\tWHERE [" + idColumn + "] = @" + idColumn);
                            }

                            yaz.WriteLine("");
                            yaz.WriteLine("\tCOMMIT");
                            yaz.WriteLine("GO");
                            yaz.WriteLine("");
                        }
                        //Remove//
                        yaz.Close();
                    }
                }
            }
        }

        void CreateStylelScript()
        {
            using (FileStream fs = new FileStream(PathAddress + "\\" + projectFolder + "\\Content\\js\\pathscript.js", FileMode.Create))
            {
                using (StreamWriter yaz = new StreamWriter(fs, Encoding.UTF8))
                {
                    yaz.WriteLine("var MainPath = null;");
                    yaz.WriteLine("var ScriptPath = null;");
                    yaz.WriteLine("var StylePath = null;");
                    yaz.WriteLine("var ImagePath = null;");
                    yaz.WriteLine("var AjaxPath = null;");
                    yaz.WriteLine("");
                    yaz.WriteLine("var AdminPath = null;");
                    yaz.WriteLine("var AdminScriptPath = null;");
                    yaz.WriteLine("var AdminStylePath = null;");
                    yaz.WriteLine("var AdminImagePath = null;");
                    yaz.WriteLine("var AdminAjaxPath = null;");
                    yaz.WriteLine("");
                    yaz.WriteLine("var SystemUser = \"admin\";");
                    yaz.WriteLine("var UploadPath = null;");
                    yaz.WriteLine("");
                    yaz.WriteLine("var Lang = null;");
                    yaz.WriteLine("var UserID = null;");
                    yaz.WriteLine("var Url = null;");
                    yaz.WriteLine("var Urling = new Object();");
                    yaz.WriteLine("");
                    yaz.WriteLine("$(document).ready(function () {");
                    yaz.WriteLine("\tMainPath = \"http://localhost/" + projectName + "\";");
                    yaz.WriteLine("\tScriptPath = MainPath + \"/Content/js\";");
                    yaz.WriteLine("\tStylePath = MainPath + \"/Content/css\";");
                    yaz.WriteLine("\tImagePath = MainPath + \"/Content/img\";");
                    yaz.WriteLine("\tAjaxPath = MainPath + \"/Ajax\";");
                    yaz.WriteLine("");
                    yaz.WriteLine("\tAdminPath = \"http://localhost/" + projectName + "/Admin\";");
                    yaz.WriteLine("\tAdminScriptPath = MainPath + \"/Content/admin/js\";");
                    yaz.WriteLine("\tAdminStylePath = MainPath + \"/Content/admin/css\";");
                    yaz.WriteLine("\tAdminImagePath = MainPath + \"/Content/admin/img\";");
                    yaz.WriteLine("\tAdminAjaxPath = MainPath + \"/Ajax/Ajax\";");
                    yaz.WriteLine("");
                    yaz.WriteLine("\tUploadPath = MainPath + \"/Uploads\";");
                    yaz.WriteLine("");
                    yaz.WriteLine("\tLang = $(\"#hdnLang\").val();");
                    yaz.WriteLine("\tUrl = $(\"#hdnUrl\").val();");
                    yaz.WriteLine("\tUserID = $(\"#hdnUserID\").val();");
                    yaz.WriteLine("");
                    yaz.WriteLine("\tif (Url != undefined) {");
                    yaz.WriteLine("\t\tvar tempurl = Url.replace(AdminPath + \"/\", \"\");");
                    yaz.WriteLine("\t\tvar extParams = tempurl.split('?')[1];");
                    yaz.WriteLine("");
                    yaz.WriteLine("\t\ttempurl = tempurl.replace(\"?\" + extParams, \"\");");
                    yaz.WriteLine("\t\ttempurl = tempurl.replace(MainPath, \"\");");
                    yaz.WriteLine("");
                    yaz.WriteLine("\t\tUrling.path = tempurl;");
                    yaz.WriteLine("\t\tUrling.controller = tempurl.split('/')[0];");
                    yaz.WriteLine("\t\tUrling.action = tempurl.split('/')[1];");
                    yaz.WriteLine("\t\tUrling.parameter = tempurl.split('/')[2];");
                    yaz.WriteLine("");
                    yaz.WriteLine("\t\tif (extParams != undefined)");
                    yaz.WriteLine("\t\t\tUrling.parameters = extParams.split('&');");
                    yaz.WriteLine("");
                    yaz.WriteLine("\t\t$(\"ul.menu li a\").removeClass(\"active\");");
                    yaz.WriteLine("");
                    yaz.WriteLine("\t\tif (Urling.controller != \"\") {");
                    yaz.WriteLine("\t\t\t$(\"ul.menu li a[data-url='\" + Urling.controller + \"']\").addClass(\"active\");");
                    yaz.WriteLine("\t\t}");
                    yaz.WriteLine("\t\telse {");
                    yaz.WriteLine("\t\t\t$(\"ul.menu li a[data-url='Index']\").addClass(\"active\");");
                    yaz.WriteLine("\t\t}");
                    yaz.WriteLine("\t}");
                    yaz.WriteLine("});");

                    yaz.Close();
                }
            }

            using (FileStream fs = new FileStream(PathAddress + "\\" + projectFolder + "\\Content\\admin\\js\\pathscript.js", FileMode.Create))
            {
                using (StreamWriter yaz = new StreamWriter(fs, Encoding.UTF8))
                {
                    yaz.WriteLine("var MainPath = null;");
                    yaz.WriteLine("var ScriptPath = null;");
                    yaz.WriteLine("var StylePath = null;");
                    yaz.WriteLine("var ImagePath = null;");
                    yaz.WriteLine("var AjaxPath = null;");
                    yaz.WriteLine("");
                    yaz.WriteLine("var AdminPath = null;");
                    yaz.WriteLine("var AdminScriptPath = null;");
                    yaz.WriteLine("var AdminStylePath = null;");
                    yaz.WriteLine("var AdminImagePath = null;");
                    yaz.WriteLine("var AdminAjaxPath = null;");
                    yaz.WriteLine("");
                    yaz.WriteLine("var SystemUser = \"admin\";");
                    yaz.WriteLine("var UploadPath = null;");
                    yaz.WriteLine("");
                    yaz.WriteLine("var Lang = null;");
                    yaz.WriteLine("var UserID = null;");
                    yaz.WriteLine("var Url = null;");
                    yaz.WriteLine("var Urling = new Object();");
                    yaz.WriteLine("");
                    yaz.WriteLine("$(document).ready(function () {");
                    yaz.WriteLine("\tMainPath = \"http://localhost/" + projectName + "\";");
                    yaz.WriteLine("\tScriptPath = MainPath + \"/Content/js\";");
                    yaz.WriteLine("\tStylePath = MainPath + \"/Content/css\";");
                    yaz.WriteLine("\tImagePath = MainPath + \"/Content/img\";");
                    yaz.WriteLine("\tAjaxPath = MainPath + \"/Ajax\";");
                    yaz.WriteLine("");
                    yaz.WriteLine("\tAdminPath = \"http://localhost/" + projectName + "/Admin\";");
                    yaz.WriteLine("\tAdminScriptPath = MainPath + \"/Content/admin/js\";");
                    yaz.WriteLine("\tAdminStylePath = MainPath + \"/Content/admin/css\";");
                    yaz.WriteLine("\tAdminImagePath = MainPath + \"/Content/admin/img\";");
                    yaz.WriteLine("\tAdminAjaxPath = MainPath + \"/Ajax/Ajax\";");
                    yaz.WriteLine("");
                    yaz.WriteLine("\tUploadPath = MainPath + \"/Uploads\";");
                    yaz.WriteLine("");
                    yaz.WriteLine("\tLang = $(\"#hdnLang\").val();");
                    yaz.WriteLine("\tUrl = $(\"#hdnUrl\").val();");
                    yaz.WriteLine("\tUserID = $(\"#hdnUserID\").val();");
                    yaz.WriteLine("");
                    yaz.WriteLine("\tif (Url != undefined) {");
                    yaz.WriteLine("\t\tvar tempurl = Url.replace(AdminPath + \"/\", \"\");");
                    yaz.WriteLine("\t\tvar extParams = tempurl.split('?')[1];");
                    yaz.WriteLine("");
                    yaz.WriteLine("\t\ttempurl = tempurl.replace(\"?\" + extParams, \"\");");
                    yaz.WriteLine("\t\ttempurl = tempurl.replace(AdminPath, \"\");");
                    yaz.WriteLine("");
                    yaz.WriteLine("\t\tUrling.path = tempurl;");
                    yaz.WriteLine("\t\tUrling.controller = tempurl.split('/')[0];");
                    yaz.WriteLine("\t\tUrling.action = tempurl.split('/')[1];");
                    yaz.WriteLine("\t\tUrling.parameter = tempurl.split('/')[2];");
                    yaz.WriteLine("");
                    yaz.WriteLine("\t\tif (extParams != undefined)");
                    yaz.WriteLine("\t\t\tUrling.parameters = extParams.split('&');");
                    yaz.WriteLine("\t}");
                    yaz.WriteLine("});");

                    yaz.Close();
                }
            }

            CreateJquery();
            CreateImages();
            CreateCKEditor();
            CreateFontAwesome();

            if (chkAngular.Checked)
            {
                CopyFromResource(StringToByteArray(Properties.Resources.Shared_Content_css_main_css), PathAddress + "\\" + projectFolder + "\\Content\\css\\main.css");
                CopyFromResource(StringToByteArray(Properties.Resources.Shared_Content_js_main_js), PathAddress + "\\" + projectFolder + "\\Content\\js\\main.js");
                CopyFromResource(StringToByteArray(Properties.Resources.Angular_Content_admin_js_main_js), PathAddress + "\\" + projectFolder + "\\Content\\admin\\js\\main.js");
                CopyFromResource(StringToByteArray(Properties.Resources.Angular_Content_admin_js_matrix_js), PathAddress + "\\" + projectFolder + "\\Content\\admin\\js\\matrix.js");

                using (FileStream fs = new FileStream(PathAddress + "\\" + projectFolder + "\\Content\\admin\\css\\matrix-style.css", FileMode.Create))
                {
                    using (StreamWriter yaz = new StreamWriter(fs, Encoding.UTF8))
                    {
                        yaz.WriteLine("*{outline:0!important;-moz-outline:none!important}body,html{height:100%;margin-top:-5px!important}body{overflow-x:hidden;margin-top:-10px;font-family:'Open Sans',sans-serif;font-size:12px;color:#666}a{color:#666}a:focus,a:hover{text-decoration:none;color:#28b779}tr.odd{background-color:#fff}.dropdown-menu .divider{margin:4px 0}.dropdown-menu{min-width:65px}.dropdown-menu>li>a{padding:3px 10px;color:#666;font-size:12px}.dropdown-menu>li>a i{padding-right:3px}.userphoto img{width:19px;height:19px}.alert{display:none}.alert,.btn,.btn-group>.btn:first-child,.btn-group>.btn:last-child,.btn-group>.dropdown-toggle,.dropdown-menu,.label,.progress,.table-bordered,.uneditable-input,.well,input[type=color],input[type=date],input[type=datetime-local],input[type=datetime],input[type=email],input[type=month],input[type=number],input[type=password],input[type=search],input[type=tel],input[type=text],input[type=time],input[type=url],input[type=week],select,textarea{border-radius:0}.btn,.uneditable-input,input[type=color],input[type=date],input[type=datetime-local],input[type=datetime],input[type=email],input[type=month],input[type=number],input[type=password],input[type=search],input[type=tel],input[type=text],input[type=time],input[type=url],input[type=week],textarea{box-shadow:none}.btn,.btn-primary,.progress,.progress .bar-danger,.progress .bar-info,.progress .bar-success,.progress .bar-warning,.progress-danger .bar,.progress-info .bar,.progress-success .bar,.progress-warning .bar{background-image:none}.accordion-heading h5{width:70%}.form-horizontal .form-actions{padding-left:20px}#footer{padding:10px;text-align:center}hr{border-top-color:#dadada}.carousel{margin-bottom:0}.fl{float:left}.fr{float:right}.badge-important,.label-important{background:#f74d4d}.bg_lb{background:#27a9e3}.bg_db{background:#2295c9}.bg_lg{background:#28b779}.bg_dg{background:#28b779}.bg_ly{background:#ffb848}.bg_dy{background:#da9628}.bg_ls{background:#2255a4}.bg_lo{background:#da542e}.bg_lr{background:#f74d4d}.bg_lv{background:#603bbc}.bg_lh{background:#b6b3b3}#header{height:77px;position:relative;width:100%;z-index:-9}#header h1{height:67px;left:0;overflow:hidden;position:relative;top:5px;width:211px;margin:0}#header h1 a{display:block;text-align:center}#header h1 a img{height:67px;vertical-align:top}#search{position:absolute;z-index:25;top:6px;right:10px}#search input[type=text]{padding:4px 10px 5px;border:0;width:100px}#search button{border:0;margin-left:-3px;margin-top:-11px;padding:5px 10px 4px}#search button i{opacity:.8;color:#fff}#search button:active i,#search button:hover i{opacity:1}#user-nav{position:absolute;left:220px;top:0;z-index:20;margin:0}#user-nav>ul{margin:0;padding:0;list-style:none;border-right:1px solid #2e363f;border-left:1px solid #000}#user-nav>ul>li{float:left;list-style-type:none;margin:0;position:relative;padding:0;border-left:1px solid #2e363f;border-right:1px solid #000}#user-nav>ul>li>a{padding:9px 10px;display:block;font-size:11px;cursor:pointer}#user-nav>ul>li.open>a,#user-nav>ul>li>a:hover{color:#fff;background:#000}#sidebar li a i,#user-nav>ul>li>a>i{opacity:.5;margin-top:2px}#user-nav>ul>li.open>a>i,#user-nav>ul>li>a:hover>i{opacity:1}#user-nav>ul>li>a>.label{vertical-align:middle;padding:1px 4px 1px;margin:-2px 4px 0;display:inline-block}#user-nav>ul ul>li>a{text-align:left}#sidebar{display:block;float:left;position:relative;width:220px;z-index:16}#sidebar>ul{list-style:none;margin:0 0 0;padding:0;position:absolute;width:220px}#sidebar>ul>li{display:block;position:relative}#sidebar>ul>li>a{padding:10px 0 10px 15px;display:block;color:#939da8}#sidebar>ul>li>a>i{margin-right:10px}#sidebar>ul>li.active>a{background:url(/" + projectName + "/Content/admin/img/menu-active.png) no-repeat scroll right center transparent!important;text-decoration:none}#sidebar>ul>li>a>.label{margin:0 20px 0 0;float:right;padding:3px 5px 2px}#sidebar>ul li ul{display:none;margin:0;padding:0}#sidebar>ul li.open ul{display:block}#sidebar>ul li ul li a{padding:10px 0 10px 25px;display:block;color:#777}#sidebar>ul li ul li:first-child a{border-top:0}#sidebar>ul li ul li:last-child a{border-bottom:0}#content{background:none repeat scroll 0 0 #eee;margin-left:220px;margin-right:0;padding-bottom:25px;position:relative;min-height:100%;width:auto}#content-header{position:abslute;width:100%;margin-top:-38px;z-index:20}#content-header h1{color:#555;font-size:28px;font-weight:400;float:none;text-shadow:0 1px 0 #fff;margin-left:20px;position:relative}#content-header .btn-group{float:right;right:20px;position:absolute}#content-header .btn-group,#content-header h1{margin-top:20px}#content-header .btn-group .btn{padding:11px 14px 9px}#content-header .btn-group .btn .label{position:absolute;top:-7px}.container-fluid .row-fluid:first-child{margin-top:20px}#breadcrumb{background-color:#fff;border-bottom:1px solid #e3ebed;text-align:center}#breadcrumb a{padding:8px 20px 8px 10px;display:inline-block;background-image:url(/../img/breadcrumb.png);background-position:center right;background-repeat:no-repeat;font-size:16px;font-weight:700;color:#2255a4;cursor:pointer}#breadcrumb a:hover{color:#333}#breadcrumb a:last-child{background-image:none}#breadcrumb a.current{font-weight:700;color:#444}#breadcrumb a i{margin-right:5px;opacity:.6}#breadcrumb a:hover i{margin-right:5px;opacity:.8}.todo ul{list-style:none outside none;margin:0}.todo li{border-bottom:1px solid #ebebeb;margin-bottom:0;padding:10px 0}.todo li:hover{background:none repeat scroll 0 0 #fcfcfc;border-color:#d9d9d9}.todo li:last-child{border-bottom:0}.todo li .txt{float:left}.todo li .by{margin-left:10px;margin-right:10px}.todo li .date{margin-right:10px}.quick-actions_homepage{width:100%;text-align:center;position:relative;float:left;margin-top:10px}.quick-actions,.quick-actions-horizontal,.stat-boxes,.stats-plain{display:block;list-style:none outside none;margin:15px 0;text-align:center}.stat-boxes2{display:inline-block;list-style:none outside none;margin:0;text-align:center}.stat-boxes2 li{display:inline-block;line-height:18px;margin:0 10px 10px;padding:0 10px;background:#fff;border:1px solid #dcdcdc}.stat-boxes2 li:hover{background:#f6f6f6}.stat-boxes .right,.stat-boxes2 .left{text-shadow:0 1px 0 #fff;float:left}.stat-boxes2 .left{border-right:1px solid #dcdcdc;box-shadow:1px 0 0 0 #fff;font-size:10px;font-weight:700;margin-right:10px;padding:10px 14px 6px 4px}.stat-boxes2 .right{color:#666;font-size:12px;padding:9px 10px 7px 0;text-align:center;min-width:70px;float:left}.stat-boxes2 .left span,.stat-boxes2 .right strong{display:block}.stat-boxes2 .right strong{font-size:26px;margin-bottom:3px;margin-top:6px}.quick-actions_homepage .quick-actions li{position:relative}.quick-actions_homepage .quick-actions li .label{position:absolute;padding:5px;top:-10px;right:-5px}.stats-plain{width:100%}.quick-actions li,.quick-actions-horizontal li,.stat-boxes li{float:left;line-height:18px;margin:0 10px 10px 0;padding:0 10px}.quick-actions li a:hover,.quick-actions li:hover,.quick-actions-horizontal li a:hover,.quick-actions-horizontal li:hover,.stat-boxes li a:hover,.stat-boxes li:hover{background:#2e363f}.quick-actions li{height:124px;width:210px}.quick-actions_homepage .quick-actions .span3{width:30%}.quick-actions li,.quick-actions-horizontal li{padding:0}.stats-plain li{padding:0 30px;display:inline-block;margin:0 10px 20px}.quick-actions li a{padding:20px 10px}.stats-plain li h4{font-size:40px;margin-bottom:15px}.stats-plain li span{font-size:14px;color:#fff}.quick-actions-horizontal li a span{padding:10px 12px 10px 10px;display:inline-block}.quick-actions li a,.quick-actions-horizontal li a{display:block;color:#fff;font-size:14px;font-weight:lighter}.quick-actions li a i[class*=\" icon-\"],.quick-actions li a i[class^=icon-]{font-size:60px;display:block;margin:0 auto 5px}.quick-actions-horizontal li a i[class*=\" icon-\"],.quick-actions-horizontal li a i[class^=icon-]{background-repeat:no-repeat;background-attachment:scroll;background-position:center;background-color:transparent;width:16px;height:16px;display:inline-block;margin:-2px 0 0!important;border-right:1px solid #ddd;margin-right:10px;padding:10px;vertical-align:middle}.quick-actions li:active,.quick-actions-horizontal li:active{background-image:-webkit-gradient(linear,0 0,0 100%,from(#eee),to(#f4f4f4));background-image:-webkit-linear-gradient(top,#eee 0,#f4f4f4 100%);background-image:-moz-linear-gradient(top,#eee 0,#f4f4f4 100%);background-image:-ms-linear-gradient(top,#eee 0,#f4f4f4 100%);background-image:-o-linear-gradient(top,#eee 0,#f4f4f4 100%);background-image:linear-gradient(top,#eee 0,#f4f4f4 100%);box-shadow:0 1px 4px 0 rgba(0,0,0,.2) inset,0 1px 0 rgba(255,255,255,.4)}.stat-boxes .left,.stat-boxes .right{text-shadow:0 1px 0 #fff;float:left}.stat-boxes .left{border-right:1px solid #dcdcdc;box-shadow:1px 0 0 0 #fff;font-size:10px;font-weight:700;margin-right:10px;padding:10px 14px 6px 4px}.stat-boxes .right{color:#666;font-size:12px;padding:9px 10px 7px 0;text-align:center;min-width:70px}.stat-boxes .left span,.stat-boxes .right strong{display:block}.stat-boxes .right strong{font-size:26px;margin-bottom:3px;margin-top:6px},.stat-boxes .peity_bar_good,.stat-boxes .peity_line_good{color:#459d1c}.stat-boxes .peity_bar_neutral,.stat-boxes .peity_line_neutral{color:#757575}.stat-boxes .peity_bar_bad,.stat-boxes .peity_line_bad{color:#ba1e20}.site-stats{margin:0;padding:0;text-align:center;list-style:none}.site-stats li{cursor:pointer;display:inline-block;margin:0 5px 10px;text-align:center;width:42%;padding:10px 0;color:#fff;position:relative}.site-stats li i{font-size:24px;clear:both}.site-stats li:hover{background:#2e363f}.site-stats li i{vertical-align:baseline}.site-stats li strong{font-weight:700;font-size:20px;width:100%;float:left;margin-left:0}.site-stats li small{margin-left:0;font-size:11px;width:100%;float:left}#donut,#interactive,#placeholder,#placeholder2,.bars,.chart,.pie{height:300px;max-width:100%}#choices{border-top:1px solid #dcdcdc;margin:10px 0;padding:10px}#choices br{display:none}#choices input{margin:-5px 5px 0 0}#choices label{display:inline-block;padding-right:20px}#tooltip{position:absolute;display:none;border:none;padding:3px 8px;border-radius:3px;font-size:10px;background-color:#222;color:#fff;z-index:25}.widget-box{background:none repeat scroll 0 0 #f9f9f9;border-left:1px solid #cdcdcd;border-top:1px solid #cdcdcd;border-right:1px solid #cdcdcd;clear:both;margin-top:16px;margin-bottom:16px;position:relative}.widget-box.widget-calendar,.widget-box.widget-chat{overflow:hidden!important}.accordion .widget-box{margin-top:-2px;margin-bottom:0;border-radius:0}.widget-box.widget-plain{background:0 0;border:none;margin-top:0;margin-bottom:0}.modal-header,.table th,.widget-title,div.dataTables_wrapper .ui-widget-header{background:#efefef;border-bottom:1px solid #cdcdcd;height:36px}.widget-title .nav-tabs{border-bottom:0 none}.widget-title .nav-tabs li a{border-bottom:medium none!important;border-left:1px solid #ddd;border-radius:0;border-right:1px solid #ddd;border-top:medium none;color:#999;margin:0;outline:medium none;padding:9px 10px 8px;font-weight:700;text-shadow:0 1px 0 #fff}.widget-title .nav-tabs li:first-child a{border-left:medium none!important}.widget-title .nav-tabs li a:hover{background-color:transparent!important;border-color:#d6d6d6;border-width:0 1px;color:#2b2b2b}.widget-title .nav-tabs li.active a{background-color:#f9f9f9!important;color:#444}.widget-title span.icon{padding:9px 10px 7px 11px;float:left;border-right:1px solid #dadada}.widget-title h5{color:#666;float:left;font-size:12px;font-weight:700;padding:12px;line-height:12px;margin:0}.widget-title .buttons{float:right;margin:8px 10px 0 0}.widget-title .label{padding:3px 5px 2px;float:right;margin:9px 11px 0 0;box-shadow:0 1px 2px rgba(0,0,0,.3) inset,0 1px 0 #fff}.widget-calendar .widget-title .label{margin-right:190px}.widget-content{padding:15px;border-bottom:1px solid #cdcdcd}.widget-box.widget-plain .widget-content{padding:12px 0 0}.widget-box.collapsible .collapse.in .widget-content{border-bottom:1px solid #cdcdcd}.recent-comments,.recent-posts,.recent-users{margin:0;padding:0}.article-post li,.recent-comments li,.recent-posts li,.recent-users li{border-bottom:1px dotted #aebdc8;list-style:none outside none;padding:10px}.recent-comments li.viewall,.recent-posts li.viewall,.recent-users li.viewall{padding:0}.recent-comments li.viewall a,.recent-posts li.viewall a,.recent-users li.viewall a{padding:5px;text-align:center;display:block;color:#888}.recent-comments li.viewall a:hover,.recent-posts li.viewall a:hover,.recent-users li.viewall a:hover{background-color:#eee}.recent-comments li:last-child,.recent-posts li:last-child,.recent-users li:last-child{border-bottom:none!important}.user-thumb{background:none repeat scroll 0 0 #fff;float:left;height:40px;margin-right:10px;margin-top:5px;padding:2px;width:40px}.user-info{color:#666;font-size:11px}.invoice-content{padding:20px}.invoice-action{margin-bottom:30px}.invoice-head{clear:both;margin-bottom:40px;overflow:hidden;width:auto}.invoice-meta{font-size:18px;margin-bottom:40px}.invoice-date{float:right;font-size:80%}.invoice-content h5{color:#333;font-size:16px;font-weight:400;margin-bottom:10px}.invoice-content ul{list-style:none;margin:0;padding:0}.invoice-to{float:left;width:370px}.invoice-from{float:right;width:300px}.invoice-from li,.invoice-to li{clear:left}.invoice-from li span,.invoice-to li span{display:block}.invoice-content th.total-label{text-align:right}.invoice-content th.total-amount{text-align:left}.amount-word{color:#666;margin-bottom:40px;margin-top:40px}.amount-word span{color:#5476a6;font-weight:700;padding-left:20px}.panel-left{margin-top:103px}.panel-left2{margin-left:176px}.panel-right{width:100%;background-color:#fff;border-bottom:1px solid #ddd;position:absolute;right:0;overflow:auto;top:38px;height:76px}.panel-right2{width:100%;background-color:#fff;border-right:1px solid #ddd;position:absolute;left:0;overflow:auto;top:0;height:94%;width:175px}.panel-right .panel-title,.panel-right2 .panel-title{width:100%;background-color:#ececec;border-bottom:1px solid #ddd}.panel-right .panel-title h5,.panel-right2 .panel-title h5{font-size:12px;color:#777;text-shadow:0 1px 0 #fff;padding:6px 10px 5px;margin:0}.panel-right .panel-content{padding:10px}.chat-content{height:470px;padding:15px}.chat-messages{height:420px;overflow:auto;position:relative}.chat-message{padding:7px 15px;margin:7px 0 0}.chat-message input[type=text]{margin-bottom:0!important;width:100%}.chat-message .input-box{display:block;margin-right:90px}.chat-message button{float:right}#chat-messages-inner p{padding:0;margin:10px 0 0 0}#chat-messages-inner p img{display:inline-block;float:left;vertical-align:middle;width:28px;height:28px;margin-top:-1px;margin-right:10px}#chat-messages-inner .msg-block,#chat-messages-inner p.offline span{background:none repeat scroll 0 0 #fff;border:1px solid #ccc;box-shadow:1px 1px 0 1px rgba(0,0,0,.05);display:block;margin-left:0;padding:10px;position:relative}#chat-messages-inner p.offline span{background:none repeat scroll 0 0 #fff5f5}#chat-messages-inner .time{color:#999;font-size:11px;float:right}#chat-messages-inner .msg{display:block;margin-top:13px;border-top:1px solid #dadada}#chat-messages-inner .msg-block:before{border-right:7px solid rgba(0,0,0,.1);border-top:7px solid transparent;border-bottom:7px solid transparent;content:\"\";display:none;left:-7px;position:absolute;top:11px}#chat-messages-inner .msg-block:after{border-right:6px solid #fff;border-top:6px solid transparent;border-bottom:6px solid transparent;content:\"\";display:none;left:-6px;position:absolute;top:12px}.chat-users{padding:0 0 30px}.chat-users .contact-list{line-height:21px;list-style:none outside none;margin:0;padding:0;font-size:10px}.chat-users .contact-list li{border:1px solid #dadada;margin:5px 5px;padding:1px;position:relative}.chat-users .contact-list li:hover{background-color:#efefef}.chat-users .contact-list li a{color:#666;display:block;padding:8px 5px}.chat-users .contact-list li.online a{font-weight:700}.chat-users .contact-list li.new{background-color:#eaeaea}.chat-users .contact-list li.offline{background-color:#ede0e0}.chat-users .contact-list li a img{display:inline-block;margin-right:10px;vertical-align:middle;width:28px;height:28px}.chat-users .contact-list li .msg-count{padding:3px 5px;position:absolute;right:10px;top:12px}.taskDesc i{margin:1px 5px 0}.taskOptions,.taskStatus{text-align:center!important}.taskStatus .in-progress{color:#64909e}.taskStatus .pending{color:#ac6363}.taskStatus .done{color:#75b468}.activity-list{list-style:none outside none;margin:0}.activity-list li{border-bottom:1px solid #eee;display:block}.activity-list li:last-child{border-bottom:medium none}.activity-list li a{display:block;padding:7px 10px}.activity-list li a:hover{background-color:#fbfbfb}.activity-list li a span{color:#aaa;font-size:11px;font-style:italic}.activity-list li a i{margin-right:10px;opacity:.6;vertical-align:middle}.new-update{border-top:1px solid #ddd;padding:10px 12px}.new-update:first-child{border-top:medium none}.new-update span{display:block}.new-update i{float:left;margin-top:3px;margin-right:13px}.new-update .update-date{color:#bbb;float:right;margin:4px -2px 0 0;text-align:center;width:30px}.new-update .update-date .update-day{display:block;font-size:20px;font-weight:700;margin-bottom:-4px}.update-alert,.update-done,.update-notice{display:block;float:left;max-width:76%}tr:hover{background:#f6f6f6}span.icon .checker{margin-top:-5px;margin-right:0}.dataTables_length{color:#878787;margin:7px 5px 0;position:relative;left:5px;width:50%;top:-2px}.dataTables_length div{vertical-align:middle}.dataTables_paginate{line-height:16px;text-align:right;margin-top:5px;margin-right:10px}.dataTables_paginate{line-height:16px;text-align:right;margin-top:5px;margin-right:10px}.dataTables_paginate .ui-button,.pagination.alternate li a{font-size:12px;padding:4px 10px!important;border-style:solid;border-width:1px;border-color:#ddd #ddd #ccc;border-color:rgba(0,0,0,.1) rgba(0,0,0,.1) rgba(0,0,0,.25);display:inline-block;line-height:16px;background:#f5f5f5;color:#333;text-shadow:0 1px 0 #fff}.dataTables_paginate .ui-button:hover,.pagination.alternate li a:hover{background:#e8e8e8;color:#222;text-shadow:0 1px 0 #fff;cursor:pointer}.dataTables_paginate .first{border-radius:4px 0 0 4px}.dataTables_paginate .last{border-radius:0 4px 4px 0}.dataTables_paginate .ui-state-disabled,.fc-state-disabled,.pagination.alternate li.disabled a{color:#aaa!important}.dataTables_paginate .ui-state-disabled:hover,.fc-state-disabled:hover,.pagination.alternate li.disabled a:hover{background:#f5f5f5;cursor:default!important}.dataTables_paginate span .ui-state-disabled,.pagination.alternate li.active a{background:#41bedd!important;color:#fff!important;cursor:default!important}div.dataTables_wrapper .ui-widget-header{border-right:medium none;border-top:1px solid #d5d5d5;font-weight:400;margin-top:-1px}.dataTables_wrapper .ui-toolbar{padding:5px}.dataTables_filter{color:#878787;font-size:11px;right:0;top:37px;margin:4px 8px 2px 10px;position:absolute;text-align:left}.dataTables_filter input{margin-bottom:0}.table th{height:auto;font-size:10px;padding:5px 10px 2px;border-bottom:0;text-align:center;color:#666}.table.with-check tr td:first-child,.table.with-check tr th:first-child{width:10px}.table.with-check tr th:first-child i{margin-top:-2px;opacity:.6}.table.with-check tr td:first-child .checker{margin-right:0}.table tr.checked td{background-color:#ffffe3!important}.nopadding{padding:0!important}.nopadding .table{margin-bottom:0}.nopadding .table-bordered{border:0}.thumbnails{margin-left:-2.12766%!important}.thumbnails [class*=span]{margin-left:2.12766%!important;position:relative}.thumbnails .actions{width:auto;height:16px;background-color:#000;padding:4px 8px 8px 8px;position:absolute;bottom:0;left:50%;margin-top:-13px;margin-left:-24px;opacity:0;top:10%;transition:1s ease-out;-moz-transition:opacity .3s ease-in-out}.thumbnails li:hover .actions,.thumbnails li:hover .actions a:hover{opacity:1;color:#fff;top:50%;transition:1s ease-out}.line{background:url(/../img/line.png) repeat-x scroll 0 0 transparent;display:block;height:8px}.modal{z-index:99999!important}.modal-backdrop{z-index:999!important}.modal-header{height:auto;padding:8px 15px 5px}.modal-header h3{font-size:12px;text-shadow:0 1px 0 #fff}.notify-ui ul{list-style:none;margin:0;padding:0}.notify-ui li{background:#eee;margin-bottom:5px;padding:5px 10px;text-align:center;border:1px solid #ddd}.notify-ui li:hover{cursor:pointer;color:#777}form{margin-bottom:0}.form-horizontal .control-group{border-top:1px solid #fff;border-bottom:1px solid #eee;margin-bottom:0}.form-horizontal .control-group:last-child{border-bottom:0}.form-horizontal .control-label{padding-top:15px;width:180px}.form-horizontal .controls{margin-left:200px;padding:10px 0}.row-fluid .span20{width:97.8%}.form-horizontal .form-actions{margin-top:0;margin-bottom:0}.help-block,.help-inline{color:#999}#lightbox{position:fixed;top:0;left:0;width:100%;height:100%;background:url(/overlay.png) repeat #000;text-align:center;z-index:9999}#lightbox p{position:absolute;top:10px;right:10px;width:22px;height:22px;cursor:pointer;z-index:22;border:1px solid #fff;border-radius:100%;padding:2px;text-align:center;transition:.5s}#lightbox p:hover{transform:rotate(180deg)}#imgbox{position:absolute;left:0;top:0;width:100%;height:100%;background:url(/overlay.png) repeat #000;text-align:center;z-index:21}#imgbox img{margin-top:100px;border:10px solid #fff}.error_ex{text-align:center}.error_ex h1{font-size:140px;font-weight:700;padding:50px 0;color:#28b779}#sidebar .content{padding:10px;position:relative;color:#939da8}#sidebar .percent{font-weight:700;position:absolute;right:10px;top:25px}#sidebar .progress{margin-bottom:2px;margin-top:2px;width:70%}#sidebar .progress-mini{height:6px}#sidebar .stat{font-size:11px}.btn-icon-pg ul{margin:0;padding:0}.btn-icon-pg ul li{margin:5px;padding:5px;list-style:none;display:inline-block;border:1px solid #dadada;min-width:187px;cursor:pointer}.btn-icon-pg ul li:hover i{transition:.3s;-moz-transition:.3s;-webkit-transition:.3s;-o-transition:.3s;margin-left:8px}.accordion{margin-top:16px}.fix_hgt{height:115px;overflow-x:auto}.input-append .add-on:last-child,.input-append .btn:last-child{border-radius:0;padding:6px 5px 2px}.input-append input,.input-append input[class*=span],.input-prepend input,.input-prepend input[class*=span]{width:none}.input-append input,.input-append select,.input-prepend input,.input-prepend span{border-radius:0!important}.bs-docs-tooltip-examples{list-style:none outside none;margin:0 0 10px;position:relative;text-align:center}.bs-docs-tooltip-examples li{display:inline;padding:0 10px;list-style:none;position:relative}@media (max-width:480px){#header{height:115px}#header h1{top:10px;left:5px;margin:3px auto}#user-nav{position:relative;left:auto;right:auto;width:100%;margin-top:-31px;border-top:1px solid #363e48;margin-bottom:0;background:#2e363f;float:right}.navbar>.nav{float:none}#my_menu{display:none}#my_menu_input{display:block}#user-nav>ul{right:0;margin-left:20%!important;margin-top:0;width:100%;background:#000;position:relative}#user-nav>ul>li{padding:0 0}#user-nav>ul>li>a{padding:5px 10px}#sidebar .content{display:none}#content{margin-left:0!important;border-top-left-radius:0;margin-top:0}#content-header{margin-top:0;text-align:center}#content-header .btn-group,#content-header h1{float:none}#content-header h1{display:block;text-align:center;margin-left:auto;margin-top:0;padding-top:15px;width:100%}#content-header .btn-group{margin-top:70px;margin-bottom:0;margin-right:0;left:30%}#sidebar{float:none;width:100%!important;display:block;position:relative;top:0}#sidebar>ul{margin:0;padding:0;width:100%;display:block;z-index:999;position:relative}#sidebar>ul>li{list-style-type:none;display:block;border-top:1px solid #41bedd;float:none!important;margin:0;position:relative;padding:2px 10px;cursor:pointer}#sidebar>ul>li:hover ul{display:none}#sidebar>ul>li:hover{background-color:#27a9e3}#sidebar>ul>li:hover a{background:0 0}#sidebar>ul li ul{margin:0;padding:0;top:35px;left:0;z-index:999;display:none;position:absolute;width:100%;min-width:100%;border-radius:none}#sidebar>ul li ul li{list-style-type:none;margin:0;font-size:12px;line-height:30px}#sidebar>ul li ul li a{display:block;padding:5px 10px;color:#fff;text-decoration:none;font-weight:700}#sidebar>ul li ul li:hover a{border-radius:0}#sidebar>ul li span{cursor:pointer;margin:0 2px 0 5px;font-weight:700;color:#fff;font-size:12px}#sidebar>ul li a i{background-image:url(/../img/glyphicons-halflings-white.png);margin-top:4px;vertical-align:top}#sidebar>a{padding:9px 20px 9px 15px;display:block!important;color:#eee;float:none!important;font-size:12px;font-weight:700}#sidebar>ul>li>a{padding:5px;display:block;color:#aaa}.widget-title .buttons>.btn{width:11px;white-space:nowrap;overflow:hidden}.form-horizontal .control-label{padding-left:30px}.form-horizontal .controls{margin-left:0;padding:10px 30px}.form-actions{text-align:center}.panel-right2{width:100%;background-color:#fff;border-right:1px solid #ddd;position:relative;left:0;overflow:auto;top:0;height:87%;width:100%}.panel-left2{margin-left:0}.dataTables_paginate .ui-button,.pagination.alternate li a{padding:4px 4px!important}.table th{padding:5px 4px 2px}}@media (min-width:481px) and (max-width:970px){body{background:#49cced}#header h1{top:5px;left:5px;width:35px}#header h1 a img{height:30px}#search{top:5px}#my_menu{display:none}#my_menu_input{display:block}#content{margin-top:0}#sidebar>ul>li{float:none}#sidebar>ul>li:hover ul{display:block}#sidebar,#sidebar>ul{width:43px;display:block;position:absolute;height:100%;z-index:1}#sidebar>ul ul{display:none;position:absolute;left:43px;top:0;min-width:150px;list-style:none}#sidebar>ul ul li a{white-space:nowrap;padding:10px 25px}#sidebar>ul ul:before{border-top:7px solid transparent;border-bottom:7px solid transparent;content:\"\";display:inline-block;left:-6px;position:absolute;top:11px}#sidebar>ul ul:after{content:\"\";display:inline-block;left:-5px;position:absolute;top:12px}#sidebar>a{display:none!important}#sidebar>ul>li.open.submenu>a{border-bottom:none!important}#sidebar>ul>li>a>span{display:none}#content{margin-left:43px}#sidebar .content{display:none}}@media (max-width:600px){.widget-title .buttons{float:left}.panel-left{margin-right:0}.panel-right{border-top:1px solid #ddd;border-left:none;position:relative;top:auto;right:auto;height:auto;width:auto}#sidebar .content{display:none}}@media (max-width:767px){body{padding:0!important}.container-fluid{padding-left:20px;padding-right:20px}#search{display:none}#user-nav>ul>li>a>span.text{display:none}#sidebar .content{display:none}}@media (min-width:768px) and (max-width:979px){.row-fluid [class*=span],[class*=span]{display:block;float:none;margin-left:0;width:auto}}@media (max-width:979px){div.dataTables_wrapper .ui-widget-header{height:68px}.dataTables_filter{position:relative;top:0}.dataTables_length{width:100%;text-align:center}.dataTables_filter,.dataTables_paginate{text-align:center}#sidebar .content{display:none}}");
                        yaz.Close();
                    }
                }
                using (FileStream fs = new FileStream(PathAddress + "\\" + projectFolder + "\\Content\\admin\\css\\main.css", FileMode.Create))
                {
                    using (StreamWriter yaz = new StreamWriter(fs, Encoding.UTF8))
                    {
                        yaz.WriteLine("fieldset {width: 546px;text-align: center;margin: 20px auto 0px auto;}");
                        yaz.WriteLine("");
                        yaz.WriteLine("fieldset > p {margin-top: 50px;}");
                        yaz.WriteLine("");
                        yaz.WriteLine(".field-validation-error {color: #ff0000;}");
                        yaz.WriteLine("");
                        yaz.WriteLine(".editor-label {font-weight: bold;padding-bottom: 5px;}");
                        yaz.WriteLine(".editor-label label {font-weight: bold;font-size: 12px;}");
                        yaz.WriteLine(".editor-field {padding-bottom: 10px;}");
                        yaz.WriteLine(".editor-field > input[type='text'][disabled='disabled'] {display: inline-block;font-size: 11.844px;font-weight: bold;line-height: 14px;color: #fff;text-shadow: 0 -1px 0 rgba(0,0,0,0.25);");
                        yaz.WriteLine("\twhite-space: nowrap;vertical-align: baseline;background-color: #3a87ad;}");
                        yaz.WriteLine("");
                        yaz.WriteLine(".ck-content {text-align: left;height: 400px;width: 530px;}");
                        yaz.WriteLine("input[type='text'],input[type='password'] {text-align: left;width: 530px;}");
                        yaz.WriteLine("input[type='file'] {width: 220px;}");
                        yaz.WriteLine("input[type='number'] {width: 100px;}");
                        yaz.WriteLine("select {width: 546px;}");
                        yaz.WriteLine("label > select {width: auto!important; }");
                        yaz.WriteLine("label > input[type='text'] {width: auto!important; }");
                        yaz.WriteLine("input[type='checkbox'] {-webkit-appearance: none;height: 30px;width: 30px;background-image: url(/" + projectName + "/Content/admin/img/passive.png);background-size: 30px 30px;}");
                        yaz.WriteLine("input[type='checkbox']:hover {opacity:0.7;}");
                        yaz.WriteLine("input[type='checkbox']:checked {background-image: url(/" + projectName + "/Content/admin/img/active.png);}");
                        yaz.WriteLine("");
                        yaz.WriteLine(".pagelinks {width: 220px;margin: 20px auto 0px auto;text-align: center;}");
                        yaz.WriteLine("");
                        yaz.WriteLine("img.active {background-image:url(/" + projectName + "/Content/admin/img/active.png);width:20px;height:20px;background-size: 20px 20px;background-repeat: no-repeat;}");
                        yaz.WriteLine("img.passive {background-image:url(/" + projectName + "/Content/admin/img/passive.png);width:20px;height:20px;background-size: 20px 20px;background-repeat: no-repeat;}");
                        yaz.WriteLine("");
                        yaz.WriteLine(".tdAlert {position:fixed;top:10px;right:10px;list-style-type:none;padding: 0px 0px 0px 0px;z-index:9999;top: 10px; right: 10px;}");
                        yaz.WriteLine(".tdAlert li {margin: 0px 0px 20px 0px;padding: 5px 5px 5px 5px;background-color: red; border-color: red;display:none;width:200px;");
                        yaz.WriteLine("\t\t  -webkit-border-radius: 10px;-moz-border-radius: 10px;border-radius: 10px;}");
                        yaz.WriteLine(".tdAlert li .tdAlertMessage {margin-top: 5px;color: white; background-color: red;}");
                        yaz.WriteLine("");
                        yaz.WriteLine("@media only screen and (min-width: 414px) and (max-width: 600px) {");
                        yaz.WriteLine("\tfieldset {width: 378px;}");
                        yaz.WriteLine("\t.ck-content {width: 362px;}");
                        yaz.WriteLine("\tinput[type='text'],input[type='password'] {width: 360px;}");
                        yaz.WriteLine("\tselect {width: 370px; }");
                        yaz.WriteLine("\tlabel > select {width: auto!important; }");
                        yaz.WriteLine("\tlabel > input[type='text'] {width: auto!important; }");
                        yaz.WriteLine("}");
                        yaz.WriteLine("");
                        yaz.WriteLine("@media only screen and (max-width: 413px) {");
                        yaz.WriteLine("\tfieldset {width: 228px;}");
                        yaz.WriteLine("\t.ck-content {width: 212px;}");
                        yaz.WriteLine("\tinput[type='text'],input[type='password'] {width: 210px;}");
                        yaz.WriteLine("\tselect {width: 220px; }");
                        yaz.WriteLine("\tlabel > select {width: auto!important; }");
                        yaz.WriteLine("\tlabel > input[type='text'] {width: auto!important; }");
                        yaz.WriteLine("}");
                        yaz.WriteLine("");
                        yaz.WriteLine("@media (max-width: 375px) {");
                        yaz.WriteLine("\t.hideColumn2 { display: none; }");
                        yaz.WriteLine("}");
                        yaz.WriteLine("");
                        yaz.WriteLine("@media (max-width: 480px) {");
                        yaz.WriteLine("\t.hideColumn {display: none;}");
                        yaz.WriteLine("}");

                        yaz.Close();
                    }
                }
                using (FileStream fs = new FileStream(PathAddress + "\\" + projectFolder + "\\Content\\admin\\css\\font-awesome\\css\\font-awesome.css", FileMode.Create))
                {
                    using (StreamWriter yaz = new StreamWriter(fs, Encoding.UTF8))
                    {
                        yaz.WriteLine("@font-face {");
                        yaz.WriteLine("\tfont-family: 'FontAwesome';");
                        yaz.WriteLine("\tsrc: url('/" + projectName + "/Content/admin/css/font-awesome/fontawesome-webfont.eot');");
                        yaz.WriteLine("\tsrc: url('/" + projectName + "/Content/admin/css/font-awesome/fontawesome-webfont.eot?#iefix') format('embedded-opentype'),");
                        yaz.WriteLine("\t\t url('/" + projectName + "/Content/admin/css/font-awesome/fontawesome-webfont.woff') format('woff'), ");
                        yaz.WriteLine("\t\t url('/" + projectName + "/Content/admin/css/font-awesome/fontawesome-webfont.ttf') format('truetype');");
                        yaz.WriteLine("\tfont-weight: normal;");
                        yaz.WriteLine("\tfont-style: normal;");
                        yaz.WriteLine("}");
                        yaz.WriteLine("");
                        yaz.WriteLine("[class^=\"icon-\"],");
                        yaz.WriteLine("[class*=\" icon-\"] {");
                        yaz.WriteLine("\tfont-family: FontAwesome;");
                        yaz.WriteLine("\tfont-weight: normal;");
                        yaz.WriteLine("\tfont-style: normal;");
                        yaz.WriteLine("\ttext-decoration: inherit;");
                        yaz.WriteLine("\tdisplay: inline;");
                        yaz.WriteLine("\twidth: auto;");
                        yaz.WriteLine("\theight: auto;");
                        yaz.WriteLine("\tline-height: normal;");
                        yaz.WriteLine("\tvertical-align: baseline;");
                        yaz.WriteLine("\tbackground-image: none !important;");
                        yaz.WriteLine("\tbackground-position: 0% 0%;");
                        yaz.WriteLine("\tbackground-repeat: repeat;");
                        yaz.WriteLine("}");
                        yaz.WriteLine("[class^=\"icon-\"]:before,");
                        yaz.WriteLine("[class*=\" icon-\"]:before {");
                        yaz.WriteLine("\ttext-decoration: inherit;");
                        yaz.WriteLine("\tdisplay: inline-block;");
                        yaz.WriteLine("\tspeak: none;");
                        yaz.WriteLine("}");
                        yaz.WriteLine("/* makes sure icons active on rollover in links */");
                        yaz.WriteLine("a [class^=\"icon-\"],");
                        yaz.WriteLine("a [class*=\" icon-\"] {");
                        yaz.WriteLine("\tdisplay: inline-block;");
                        yaz.WriteLine("}");
                        yaz.WriteLine("/* makes the font 33% larger relative to the icon container */");
                        yaz.WriteLine(".icon-large:before {");
                        yaz.WriteLine("\tvertical-align: -10%;");
                        yaz.WriteLine("\tfont-size: 1.3333333333333333em;");
                        yaz.WriteLine("}");
                        yaz.WriteLine(".btn [class^=\"icon-\"],");
                        yaz.WriteLine(".nav [class^=\"icon-\"],");
                        yaz.WriteLine(".btn [class*=\" icon-\"],");
                        yaz.WriteLine(".nav [class*=\" icon-\"] {");
                        yaz.WriteLine("\tdisplay: inline;");
                        yaz.WriteLine("\t/* keeps button heights with and without icons the same */");
                        yaz.WriteLine("");
                        yaz.WriteLine("\tline-height: .6em;");
                        yaz.WriteLine("}");
                        yaz.WriteLine(".btn [class^=\"icon-\"].icon-spin,");
                        yaz.WriteLine(".nav [class^=\"icon-\"].icon-spin,");
                        yaz.WriteLine(".btn [class*=\" icon-\"].icon-spin,");
                        yaz.WriteLine(".nav [class*=\" icon-\"].icon-spin {");
                        yaz.WriteLine("\tdisplay: inline-block;");
                        yaz.WriteLine("}");
                        yaz.WriteLine("li [class^=\"icon-\"],");
                        yaz.WriteLine("li [class*=\" icon-\"] {");
                        yaz.WriteLine("\tdisplay: inline-block;");
                        yaz.WriteLine("\twidth: 1.25em;");
                        yaz.WriteLine("\ttext-align: center;");
                        yaz.WriteLine("}");
                        yaz.WriteLine("li [class^=\"icon-\"].icon-large,");
                        yaz.WriteLine("li [class*=\" icon-\"].icon-large {");
                        yaz.WriteLine("\t/* increased font size for icon-large */");
                        yaz.WriteLine("");
                        yaz.WriteLine("\twidth: 1.5625em;");
                        yaz.WriteLine("}");
                        yaz.WriteLine("ul.icons {");
                        yaz.WriteLine("\tlist-style-type: none;");
                        yaz.WriteLine("\ttext-indent: -0.75em;");
                        yaz.WriteLine("}");
                        yaz.WriteLine("ul.icons li [class^=\"icon-\"],");
                        yaz.WriteLine("ul.icons li [class*=\" icon-\"] {");
                        yaz.WriteLine("\twidth: .75em;");
                        yaz.WriteLine("}");
                        yaz.WriteLine(".icon-muted {");
                        yaz.WriteLine("\tcolor: #eeeeee;");
                        yaz.WriteLine("}");
                        yaz.WriteLine(".icon-border {");
                        yaz.WriteLine("\tborder: solid 1px #eeeeee;");
                        yaz.WriteLine("\tpadding: .2em .25em .15em;");
                        yaz.WriteLine("\t-webkit-border-radius: 3px;");
                        yaz.WriteLine("\t-moz-border-radius: 3px;");
                        yaz.WriteLine("\tborder-radius: 3px;");
                        yaz.WriteLine("}");
                        yaz.WriteLine(".icon-2x {");
                        yaz.WriteLine("\tfont-size: 2em;");
                        yaz.WriteLine("}");
                        yaz.WriteLine(".icon-2x.icon-border {");
                        yaz.WriteLine("\tborder-width: 2px;");
                        yaz.WriteLine("\t-webkit-border-radius: 4px;");
                        yaz.WriteLine("\t-moz-border-radius: 4px;");
                        yaz.WriteLine("\tborder-radius: 4px;");
                        yaz.WriteLine("}");
                        yaz.WriteLine(".icon-3x {");
                        yaz.WriteLine("\tfont-size: 3em;");
                        yaz.WriteLine("}");
                        yaz.WriteLine(".icon-3x.icon-border {");
                        yaz.WriteLine("\tborder-width: 3px;");
                        yaz.WriteLine("\t-webkit-border-radius: 5px;");
                        yaz.WriteLine("\t-moz-border-radius: 5px;");
                        yaz.WriteLine("\tborder-radius: 5px;");
                        yaz.WriteLine("}");
                        yaz.WriteLine(".icon-4x {");
                        yaz.WriteLine("\tfont-size: 4em;");
                        yaz.WriteLine("}");
                        yaz.WriteLine(".icon-4x.icon-border {");
                        yaz.WriteLine("\tborder-width: 4px;");
                        yaz.WriteLine("\t-webkit-border-radius: 6px;");
                        yaz.WriteLine("\t-moz-border-radius: 6px;");
                        yaz.WriteLine("\tborder-radius: 6px;");
                        yaz.WriteLine("}");
                        yaz.WriteLine(".pull-right {");
                        yaz.WriteLine("\tfloat: right;");
                        yaz.WriteLine("}");
                        yaz.WriteLine(".pull-left {");
                        yaz.WriteLine("\tfloat: left;");
                        yaz.WriteLine("}");
                        yaz.WriteLine("[class^=\"icon-\"].pull-left,");
                        yaz.WriteLine("[class*=\" icon-\"].pull-left {");
                        yaz.WriteLine("\tmargin-right: .35em;");
                        yaz.WriteLine("}");
                        yaz.WriteLine("[class^=\"icon-\"].pull-right,");
                        yaz.WriteLine("[class*=\" icon-\"].pull-right {");
                        yaz.WriteLine("\tmargin-left: .35em;");
                        yaz.WriteLine("}");
                        yaz.WriteLine(".btn [class^=\"icon-\"].pull-left.icon-2x,");
                        yaz.WriteLine(".btn [class*=\" icon-\"].pull-left.icon-2x,");
                        yaz.WriteLine(".btn [class^=\"icon-\"].pull-right.icon-2x,");
                        yaz.WriteLine(".btn [class*=\" icon-\"].pull-right.icon-2x {");
                        yaz.WriteLine("\tmargin-top: .35em;");
                        yaz.WriteLine("}");
                        yaz.WriteLine(".btn [class^=\"icon-\"].icon-spin.icon-large,");
                        yaz.WriteLine(".btn [class*=\" icon-\"].icon-spin.icon-large {");
                        yaz.WriteLine("\theight: .75em;");
                        yaz.WriteLine("}");
                        yaz.WriteLine(".btn.btn-small [class^=\"icon-\"].pull-left.icon-2x,");
                        yaz.WriteLine(".btn.btn-small [class*=\" icon-\"].pull-left.icon-2x,");
                        yaz.WriteLine(".btn.btn-small [class^=\"icon-\"].pull-right.icon-2x,");
                        yaz.WriteLine(".btn.btn-small [class*=\" icon-\"].pull-right.icon-2x {");
                        yaz.WriteLine("\tmargin-top: .45em;");
                        yaz.WriteLine("}");
                        yaz.WriteLine(".btn.btn-large [class^=\"icon-\"].pull-left.icon-2x,");
                        yaz.WriteLine(".btn.btn-large [class*=\" icon-\"].pull-left.icon-2x,");
                        yaz.WriteLine(".btn.btn-large [class^=\"icon-\"].pull-right.icon-2x,");
                        yaz.WriteLine(".btn.btn-large [class*=\" icon-\"].pull-right.icon-2x {");
                        yaz.WriteLine("\tmargin-top: .2em;");
                        yaz.WriteLine("}");
                        yaz.WriteLine(".icon-spin {");
                        yaz.WriteLine("\tdisplay: inline-block;");
                        yaz.WriteLine("\t-moz-animation: spin 2s infinite linear;");
                        yaz.WriteLine("\t-o-animation: spin 2s infinite linear;");
                        yaz.WriteLine("\t-webkit-animation: spin 2s infinite linear;");
                        yaz.WriteLine("\tanimation: spin 2s infinite linear;");
                        yaz.WriteLine("}");
                        yaz.WriteLine("@-moz-keyframes spin {");
                        yaz.WriteLine("\t0% { -moz-transform: rotate(0deg); }");
                        yaz.WriteLine("\t100% { -moz-transform: rotate(359deg); }");
                        yaz.WriteLine("}");
                        yaz.WriteLine("@-webkit-keyframes spin {");
                        yaz.WriteLine("\t0% { -webkit-transform: rotate(0deg); }");
                        yaz.WriteLine("\t100% { -webkit-transform: rotate(359deg); }");
                        yaz.WriteLine("}");
                        yaz.WriteLine("@-o-keyframes spin {");
                        yaz.WriteLine("\t0% { -o-transform: rotate(0deg); }");
                        yaz.WriteLine("\t100% { -o-transform: rotate(359deg); }");
                        yaz.WriteLine("}");
                        yaz.WriteLine("@-ms-keyframes spin {");
                        yaz.WriteLine("\t0% { -ms-transform: rotate(0deg); }");
                        yaz.WriteLine("\t100% { -ms-transform: rotate(359deg); }");
                        yaz.WriteLine("}");
                        yaz.WriteLine("@keyframes spin {");
                        yaz.WriteLine("\t0% { transform: rotate(0deg); }");
                        yaz.WriteLine("\t100% { transform: rotate(359deg); }");
                        yaz.WriteLine("}");
                        yaz.WriteLine("");
                        yaz.WriteLine(".icon-glass:before                { content: \"\\f000\"; }");
                        yaz.WriteLine(".icon-music:before                { content: \"\\f001\"; }");
                        yaz.WriteLine(".icon-search:before               { content: \"\\f002\"; }");
                        yaz.WriteLine(".icon-envelope:before             { content: \"\\f003\"; }");
                        yaz.WriteLine(".icon-heart:before                { content: \"\\f004\"; }");
                        yaz.WriteLine(".icon-star:before                 { content: \"\\f005\"; }");
                        yaz.WriteLine(".icon-star-empty:before           { content: \"\\f006\"; }");
                        yaz.WriteLine(".icon-user:before                 { content: \"\\f007\"; }");
                        yaz.WriteLine(".icon-film:before                 { content: \"\\f008\"; }");
                        yaz.WriteLine(".icon-th-large:before             { content: \"\\f009\"; }");
                        yaz.WriteLine(".icon-th:before                   { content: \"\\f00a\"; }");
                        yaz.WriteLine(".icon-th-list:before              { content: \"\\f00b\"; }");
                        yaz.WriteLine(".icon-ok:before                   { content: \"\\f00c\"; }");
                        yaz.WriteLine(".icon-remove:before               { content: \"\\f00d\"; }");
                        yaz.WriteLine(".icon-zoom-in:before              { content: \"\\f00e\"; }");
                        yaz.WriteLine("");
                        yaz.WriteLine(".icon-zoom-out:before             { content: \"\\f010\"; }");
                        yaz.WriteLine(".icon-off:before                  { content: \"\\f011\"; }");
                        yaz.WriteLine(".icon-signal:before               { content: \"\\f012\"; }");
                        yaz.WriteLine(".icon-cog:before                  { content: \"\\f013\"; }");
                        yaz.WriteLine(".icon-trash:before                { content: \"\\f014\"; }");
                        yaz.WriteLine(".icon-home:before                 { content: \"\\f015\"; }");
                        yaz.WriteLine(".icon-file:before                 { content: \"\\f016\"; }");
                        yaz.WriteLine(".icon-time:before                 { content: \"\\f017\"; }");
                        yaz.WriteLine(".icon-road:before                 { content: \"\\f018\"; }");
                        yaz.WriteLine(".icon-download-alt:before         { content: \"\\f019\"; }");
                        yaz.WriteLine(".icon-download:before             { content: \"\\f01a\"; }");
                        yaz.WriteLine(".icon-upload:before               { content: \"\\f01b\"; }");
                        yaz.WriteLine(".icon-inbox:before                { content: \"\\f01c\"; }");
                        yaz.WriteLine(".icon-play-circle:before          { content: \"\\f01d\"; }");
                        yaz.WriteLine(".icon-repeat:before               { content: \"\\f01e\"; }");
                        yaz.WriteLine("");
                        yaz.WriteLine("/* \\f020 doesn't work in Safari. all shifted one down */");
                        yaz.WriteLine(".icon-refresh:before              { content: \"\\f021\"; }");
                        yaz.WriteLine(".icon-list-alt:before             { content: \"\\f022\"; }");
                        yaz.WriteLine(".icon-lock:before                 { content: \"\\f023\"; }");
                        yaz.WriteLine(".icon-flag:before                 { content: \"\\f024\"; }");
                        yaz.WriteLine(".icon-headphones:before           { content: \"\\f025\"; }");
                        yaz.WriteLine(".icon-volume-off:before           { content: \"\\f026\"; }");
                        yaz.WriteLine(".icon-volume-down:before          { content: \"\\f027\"; }");
                        yaz.WriteLine(".icon-volume-up:before            { content: \"\\f028\"; }");
                        yaz.WriteLine(".icon-qrcode:before               { content: \"\\f029\"; }");
                        yaz.WriteLine(".icon-barcode:before              { content: \"\\f02a\"; }");
                        yaz.WriteLine(".icon-tag:before                  { content: \"\\f02b\"; }");
                        yaz.WriteLine(".icon-tags:before                 { content: \"\\f02c\"; }");
                        yaz.WriteLine(".icon-book:before                 { content: \"\\f02d\"; }");
                        yaz.WriteLine(".icon-bookmark:before             { content: \"\\f02e\"; }");
                        yaz.WriteLine(".icon-print:before                { content: \"\\f02f\"; }");
                        yaz.WriteLine("");
                        yaz.WriteLine(".icon-camera:before               { content: \"\\f030\"; }");
                        yaz.WriteLine(".icon-font:before                 { content: \"\\f031\"; }");
                        yaz.WriteLine(".icon-bold:before                 { content: \"\\f032\"; }");
                        yaz.WriteLine(".icon-italic:before               { content: \"\\f033\"; }");
                        yaz.WriteLine(".icon-text-height:before          { content: \"\\f034\"; }");
                        yaz.WriteLine(".icon-text-width:before           { content: \"\\f035\"; }");
                        yaz.WriteLine(".icon-align-left:before           { content: \"\\f036\"; }");
                        yaz.WriteLine(".icon-align-center:before         { content: \"\\f037\"; }");
                        yaz.WriteLine(".icon-align-right:before          { content: \"\\f038\"; }");
                        yaz.WriteLine(".icon-align-justify:before        { content: \"\\f039\"; }");
                        yaz.WriteLine(".icon-list:before                 { content: \"\\f03a\"; }");
                        yaz.WriteLine(".icon-indent-left:before          { content: \"\\f03b\"; }");
                        yaz.WriteLine(".icon-indent-right:before         { content: \"\\f03c\"; }");
                        yaz.WriteLine(".icon-facetime-video:before       { content: \"\\f03d\"; }");
                        yaz.WriteLine(".icon-picture:before              { content: \"\\f03e\"; }");
                        yaz.WriteLine("");
                        yaz.WriteLine(".icon-pencil:before               { content: \"\\f040\"; }");
                        yaz.WriteLine(".icon-map-marker:before           { content: \"\\f041\"; }");
                        yaz.WriteLine(".icon-adjust:before               { content: \"\\f042\"; }");
                        yaz.WriteLine(".icon-tint:before                 { content: \"\\f043\"; }");
                        yaz.WriteLine(".icon-edit:before                 { content: \"\\f044\"; }");
                        yaz.WriteLine(".icon-share:before                { content: \"\\f045\"; }");
                        yaz.WriteLine(".icon-check:before                { content: \"\\f046\"; }");
                        yaz.WriteLine(".icon-move:before                 { content: \"\\f047\"; }");
                        yaz.WriteLine(".icon-step-backward:before        { content: \"\\f048\"; }");
                        yaz.WriteLine(".icon-fast-backward:before        { content: \"\\f049\"; }");
                        yaz.WriteLine(".icon-backward:before             { content: \"\\f04a\"; }");
                        yaz.WriteLine(".icon-play:before                 { content: \"\\f04b\"; }");
                        yaz.WriteLine(".icon-pause:before                { content: \"\\f04c\"; }");
                        yaz.WriteLine(".icon-stop:before                 { content: \"\\f04d\"; }");
                        yaz.WriteLine(".icon-forward:before              { content: \"\\f04e\"; }");
                        yaz.WriteLine("");
                        yaz.WriteLine(".icon-fast-forward:before         { content: \"\\f050\"; }");
                        yaz.WriteLine(".icon-step-forward:before         { content: \"\\f051\"; }");
                        yaz.WriteLine(".icon-eject:before                { content: \"\\f052\"; }");
                        yaz.WriteLine(".icon-chevron-left:before         { content: \"\\f053\"; }");
                        yaz.WriteLine(".icon-chevron-right:before        { content: \"\\f054\"; }");
                        yaz.WriteLine(".icon-plus-sign:before            { content: \"\\f055\"; }");
                        yaz.WriteLine(".icon-minus-sign:before           { content: \"\\f056\"; }");
                        yaz.WriteLine(".icon-remove-sign:before          { content: \"\\f057\"; }");
                        yaz.WriteLine(".icon-ok-sign:before              { content: \"\\f058\"; }");
                        yaz.WriteLine(".icon-question-sign:before        { content: \"\\f059\"; }");
                        yaz.WriteLine(".icon-info-sign:before            { content: \"\\f05a\"; }");
                        yaz.WriteLine(".icon-screenshot:before           { content: \"\\f05b\"; }");
                        yaz.WriteLine(".icon-remove-circle:before        { content: \"\\f05c\"; }");
                        yaz.WriteLine(".icon-ok-circle:before            { content: \"\\f05d\"; }");
                        yaz.WriteLine(".icon-ban-circle:before           { content: \"\\f05e\"; }");
                        yaz.WriteLine("");
                        yaz.WriteLine(".icon-arrow-left:before           { content: \"\\f060\"; }");
                        yaz.WriteLine(".icon-arrow-right:before          { content: \"\\f061\"; }");
                        yaz.WriteLine(".icon-arrow-up:before             { content: \"\\f062\"; }");
                        yaz.WriteLine(".icon-arrow-down:before           { content: \"\\f063\"; }");
                        yaz.WriteLine(".icon-share-alt:before            { content: \"\\f064\"; }");
                        yaz.WriteLine(".icon-resize-full:before          { content: \"\\f065\"; }");
                        yaz.WriteLine(".icon-resize-small:before         { content: \"\\f066\"; }");
                        yaz.WriteLine(".icon-plus:before                 { content: \"\\f067\"; }");
                        yaz.WriteLine(".icon-minus:before                { content: \"\\f068\"; }");
                        yaz.WriteLine(".icon-asterisk:before             { content: \"\\f069\"; }");
                        yaz.WriteLine(".icon-exclamation-sign:before     { content: \"\\f06a\"; }");
                        yaz.WriteLine(".icon-gift:before                 { content: \"\\f06b\"; }");
                        yaz.WriteLine(".icon-leaf:before                 { content: \"\\f06c\"; }");
                        yaz.WriteLine(".icon-fire:before                 { content: \"\\f06d\"; }");
                        yaz.WriteLine(".icon-eye-open:before             { content: \"\\f06e\"; }");
                        yaz.WriteLine("");
                        yaz.WriteLine(".icon-eye-close:before            { content: \"\\f070\"; }");
                        yaz.WriteLine(".icon-warning-sign:before         { content: \"\\f071\"; }");
                        yaz.WriteLine(".icon-plane:before                { content: \"\\f072\"; }");
                        yaz.WriteLine(".icon-calendar:before             { content: \"\\f073\"; }");
                        yaz.WriteLine(".icon-random:before               { content: \"\\f074\"; }");
                        yaz.WriteLine(".icon-comment:before              { content: \"\\f075\"; }");
                        yaz.WriteLine(".icon-magnet:before               { content: \"\\f076\"; }");
                        yaz.WriteLine(".icon-chevron-up:before           { content: \"\\f077\"; }");
                        yaz.WriteLine(".icon-chevron-down:before         { content: \"\\f078\"; }");
                        yaz.WriteLine(".icon-retweet:before              { content: \"\\f079\"; }");
                        yaz.WriteLine(".icon-shopping-cart:before        { content: \"\\f07a\"; }");
                        yaz.WriteLine(".icon-folder-close:before         { content: \"\\f07b\"; }");
                        yaz.WriteLine(".icon-folder-open:before          { content: \"\\f07c\"; }");
                        yaz.WriteLine(".icon-resize-vertical:before      { content: \"\\f07d\"; }");
                        yaz.WriteLine(".icon-resize-horizontal:before    { content: \"\\f07e\"; }");
                        yaz.WriteLine("");
                        yaz.WriteLine(".icon-bar-chart:before            { content: \"\\f080\"; }");
                        yaz.WriteLine(".icon-twitter-sign:before         { content: \"\\f081\"; }");
                        yaz.WriteLine(".icon-facebook-sign:before        { content: \"\\f082\"; }");
                        yaz.WriteLine(".icon-camera-retro:before         { content: \"\\f083\"; }");
                        yaz.WriteLine(".icon-key:before                  { content: \"\\f084\"; }");
                        yaz.WriteLine(".icon-cogs:before                 { content: \"\\f085\"; }");
                        yaz.WriteLine(".icon-comments:before             { content: \"\\f086\"; }");
                        yaz.WriteLine(".icon-thumbs-up:before            { content: \"\\f087\"; }");
                        yaz.WriteLine(".icon-thumbs-down:before          { content: \"\\f088\"; }");
                        yaz.WriteLine(".icon-star-half:before            { content: \"\\f089\"; }");
                        yaz.WriteLine(".icon-heart-empty:before          { content: \"\\f08a\"; }");
                        yaz.WriteLine(".icon-signout:before              { content: \"\\f08b\"; }");
                        yaz.WriteLine(".icon-linkedin-sign:before        { content: \"\\f08c\"; }");
                        yaz.WriteLine(".icon-pushpin:before              { content: \"\\f08d\"; }");
                        yaz.WriteLine(".icon-external-link:before        { content: \"\\f08e\"; }");
                        yaz.WriteLine("");
                        yaz.WriteLine(".icon-signin:before               { content: \"\\f090\"; }");
                        yaz.WriteLine(".icon-trophy:before               { content: \"\\f091\"; }");
                        yaz.WriteLine(".icon-github-sign:before          { content: \"\\f092\"; }");
                        yaz.WriteLine(".icon-upload-alt:before           { content: \"\\f093\"; }");
                        yaz.WriteLine(".icon-lemon:before                { content: \"\\f094\"; }");
                        yaz.WriteLine(".icon-phone:before                { content: \"\\f095\"; }");
                        yaz.WriteLine(".icon-check-empty:before          { content: \"\\f096\"; }");
                        yaz.WriteLine(".icon-bookmark-empty:before       { content: \"\\f097\"; }");
                        yaz.WriteLine(".icon-phone-sign:before           { content: \"\\f098\"; }");
                        yaz.WriteLine(".icon-twitter:before              { content: \"\\f099\"; }");
                        yaz.WriteLine(".icon-facebook:before             { content: \"\\f09a\"; }");
                        yaz.WriteLine(".icon-github:before               { content: \"\\f09b\"; }");
                        yaz.WriteLine(".icon-unlock:before               { content: \"\\f09c\"; }");
                        yaz.WriteLine(".icon-credit-card:before          { content: \"\\f09d\"; }");
                        yaz.WriteLine(".icon-rss:before                  { content: \"\\f09e\"; }");
                        yaz.WriteLine("");
                        yaz.WriteLine(".icon-hdd:before                  { content: \"\\f0a0\"; }");
                        yaz.WriteLine(".icon-bullhorn:before             { content: \"\\f0a1\"; }");
                        yaz.WriteLine(".icon-bell:before                 { content: \"\\f0a2\"; }");
                        yaz.WriteLine(".icon-certificate:before          { content: \"\\f0a3\"; }");
                        yaz.WriteLine(".icon-hand-right:before           { content: \"\\f0a4\"; }");
                        yaz.WriteLine(".icon-hand-left:before            { content: \"\\f0a5\"; }");
                        yaz.WriteLine(".icon-hand-up:before              { content: \"\\f0a6\"; }");
                        yaz.WriteLine(".icon-hand-down:before            { content: \"\\f0a7\"; }");
                        yaz.WriteLine(".icon-circle-arrow-left:before    { content: \"\\f0a8\"; }");
                        yaz.WriteLine(".icon-circle-arrow-right:before   { content: \"\\f0a9\"; }");
                        yaz.WriteLine(".icon-circle-arrow-up:before      { content: \"\\f0aa\"; }");
                        yaz.WriteLine(".icon-circle-arrow-down:before    { content: \"\\f0ab\"; }");
                        yaz.WriteLine(".icon-globe:before                { content: \"\\f0ac\"; }");
                        yaz.WriteLine(".icon-wrench:before               { content: \"\\f0ad\"; }");
                        yaz.WriteLine(".icon-tasks:before                { content: \"\\f0ae\"; }");
                        yaz.WriteLine("");
                        yaz.WriteLine(".icon-filter:before               { content: \"\\f0b0\"; }");
                        yaz.WriteLine(".icon-briefcase:before            { content: \"\\f0b1\"; }");
                        yaz.WriteLine(".icon-fullscreen:before           { content: \"\\f0b2\"; }");
                        yaz.WriteLine("");
                        yaz.WriteLine(".icon-group:before                { content: \"\\f0c0\"; }");
                        yaz.WriteLine(".icon-link:before                 { content: \"\\f0c1\"; }");
                        yaz.WriteLine(".icon-cloud:before                { content: \"\\f0c2\"; }");
                        yaz.WriteLine(".icon-beaker:before               { content: \"\\f0c3\"; }");
                        yaz.WriteLine(".icon-cut:before                  { content: \"\\f0c4\"; }");
                        yaz.WriteLine(".icon-copy:before                 { content: \"\\f0c5\"; }");
                        yaz.WriteLine(".icon-paper-clip:before           { content: \"\\f0c6\"; }");
                        yaz.WriteLine(".icon-save:before                 { content: \"\\f0c7\"; }");
                        yaz.WriteLine(".icon-sign-blank:before           { content: \"\\f0c8\"; }");
                        yaz.WriteLine(".icon-reorder:before              { content: \"\\f0c9\"; }");
                        yaz.WriteLine(".icon-list-ul:before              { content: \"\\f0ca\"; }");
                        yaz.WriteLine(".icon-list-ol:before              { content: \"\\f0cb\"; }");
                        yaz.WriteLine(".icon-strikethrough:before        { content: \"\\f0cc\"; }");
                        yaz.WriteLine(".icon-underline:before            { content: \"\\f0cd\"; }");
                        yaz.WriteLine(".icon-table:before                { content: \"\\f0ce\"; }");
                        yaz.WriteLine("");
                        yaz.WriteLine(".icon-magic:before                { content: \"\\f0d0\"; }");
                        yaz.WriteLine(".icon-truck:before                { content: \"\\f0d1\"; }");
                        yaz.WriteLine(".icon-pinterest:before            { content: \"\\f0d2\"; }");
                        yaz.WriteLine(".icon-pinterest-sign:before       { content: \"\\f0d3\"; }");
                        yaz.WriteLine(".icon-google-plus-sign:before     { content: \"\\f0d4\"; }");
                        yaz.WriteLine(".icon-google-plus:before          { content: \"\\f0d5\"; }");
                        yaz.WriteLine(".icon-money:before                { content: \"\\f0d6\"; }");
                        yaz.WriteLine(".icon-caret-down:before           { content: \"\\f0d7\"; }");
                        yaz.WriteLine(".icon-caret-up:before             { content: \"\\f0d8\"; }");
                        yaz.WriteLine(".icon-caret-left:before           { content: \"\\f0d9\"; }");
                        yaz.WriteLine(".icon-caret-right:before          { content: \"\\f0da\"; }");
                        yaz.WriteLine(".icon-columns:before              { content: \"\\f0db\"; }");
                        yaz.WriteLine(".icon-sort:before                 { content: \"\\f0dc\"; }");
                        yaz.WriteLine(".icon-sort-down:before            { content: \"\\f0dd\"; }");
                        yaz.WriteLine(".icon-sort-up:before              { content: \"\\f0de\"; }");
                        yaz.WriteLine("");
                        yaz.WriteLine(".icon-envelope-alt:before         { content: \"\\f0e0\"; }");
                        yaz.WriteLine(".icon-linkedin:before             { content: \"\\f0e1\"; }");
                        yaz.WriteLine(".icon-undo:before                 { content: \"\\f0e2\"; }");
                        yaz.WriteLine(".icon-legal:before                { content: \"\\f0e3\"; }");
                        yaz.WriteLine(".icon-dashboard:before            { content: \"\\f0e4\"; }");
                        yaz.WriteLine(".icon-comment-alt:before          { content: \"\\f0e5\"; }");
                        yaz.WriteLine(".icon-comments-alt:before         { content: \"\\f0e6\"; }");
                        yaz.WriteLine(".icon-bolt:before                 { content: \"\\f0e7\"; }");
                        yaz.WriteLine(".icon-sitemap:before              { content: \"\\f0e8\"; }");
                        yaz.WriteLine(".icon-umbrella:before             { content: \"\\f0e9\"; }");
                        yaz.WriteLine(".icon-paste:before                { content: \"\\f0ea\"; }");
                        yaz.WriteLine(".icon-lightbulb:before            { content: \"\\f0eb\"; }");
                        yaz.WriteLine(".icon-exchange:before             { content: \"\\f0ec\"; }");
                        yaz.WriteLine(".icon-cloud-download:before       { content: \"\\f0ed\"; }");
                        yaz.WriteLine(".icon-cloud-upload:before         { content: \"\\f0ee\"; }");
                        yaz.WriteLine("");
                        yaz.WriteLine(".icon-user-md:before              { content: \"\\f0f0\"; }");
                        yaz.WriteLine(".icon-stethoscope:before          { content: \"\\f0f1\"; }");
                        yaz.WriteLine(".icon-suitcase:before             { content: \"\\f0f2\"; }");
                        yaz.WriteLine(".icon-bell-alt:before             { content: \"\\f0f3\"; }");
                        yaz.WriteLine(".icon-coffee:before               { content: \"\\f0f4\"; }");
                        yaz.WriteLine(".icon-food:before                 { content: \"\\f0f5\"; }");
                        yaz.WriteLine(".icon-file-alt:before             { content: \"\\f0f6\"; }");
                        yaz.WriteLine(".icon-building:before             { content: \"\\f0f7\"; }");
                        yaz.WriteLine(".icon-hospital:before             { content: \"\\f0f8\"; }");
                        yaz.WriteLine(".icon-ambulance:before            { content: \"\\f0f9\"; }");
                        yaz.WriteLine(".icon-medkit:before               { content: \"\\f0fa\"; }");
                        yaz.WriteLine(".icon-fighter-jet:before          { content: \"\\f0fb\"; }");
                        yaz.WriteLine(".icon-beer:before                 { content: \"\\f0fc\"; }");
                        yaz.WriteLine(".icon-h-sign:before               { content: \"\\f0fd\"; }");
                        yaz.WriteLine(".icon-plus-sign-alt:before        { content: \"\\f0fe\"; }");
                        yaz.WriteLine("");
                        yaz.WriteLine(".icon-double-angle-left:before    { content: \"\\f100\"; }");
                        yaz.WriteLine(".icon-double-angle-right:before   { content: \"\\f101\"; }");
                        yaz.WriteLine(".icon-double-angle-up:before      { content: \"\\f102\"; }");
                        yaz.WriteLine(".icon-double-angle-down:before    { content: \"\\f103\"; }");
                        yaz.WriteLine(".icon-angle-left:before           { content: \"\\f104\"; }");
                        yaz.WriteLine(".icon-angle-right:before          { content: \"\\f105\"; }");
                        yaz.WriteLine(".icon-angle-up:before             { content: \"\\f106\"; }");
                        yaz.WriteLine(".icon-angle-down:before           { content: \"\\f107\"; }");
                        yaz.WriteLine(".icon-desktop:before              { content: \"\\f108\"; }");
                        yaz.WriteLine(".icon-laptop:before               { content: \"\\f109\"; }");
                        yaz.WriteLine(".icon-tablet:before               { content: \"\\f10a\"; }");
                        yaz.WriteLine(".icon-mobile-phone:before         { content: \"\\f10b\"; }");
                        yaz.WriteLine(".icon-circle-blank:before         { content: \"\\f10c\"; }");
                        yaz.WriteLine(".icon-quote-left:before           { content: \"\\f10d\"; }");
                        yaz.WriteLine(".icon-quote-right:before          { content: \"\\f10e\"; }");
                        yaz.WriteLine("");
                        yaz.WriteLine(".icon-spinner:before              { content: \"\\f110\"; }");
                        yaz.WriteLine(".icon-circle:before               { content: \"\\f111\"; }");
                        yaz.WriteLine(".icon-reply:before                { content: \"\\f112\"; }");
                        yaz.WriteLine(".icon-github-alt:before           { content: \"\\f113\"; }");
                        yaz.WriteLine(".icon-folder-close-alt:before     { content: \"\\f114\"; }");
                        yaz.WriteLine(".icon-folder-open-alt:before      { content: \"\\f115\"; }");
                        yaz.Close();
                    }
                }
            }
            else
            {
                CopyFromResource(StringToByteArray(Properties.Resources.Shared_Content_css_main_css), PathAddress + "\\" + projectFolder + "\\Content\\css\\style.css");
                CopyFromResource(StringToByteArray(Properties.Resources.Shared_Content_js_main_js), PathAddress + "\\" + projectFolder + "\\Content\\js\\script.js");
                CopyFromResource(StringToByteArray(Properties.Resources.Normal_Content_admin_css_style_css), PathAddress + "\\" + projectFolder + "\\Content\\admin\\css\\style.css");

                CopyFromResource(StringToByteArray(Properties.Resources.Normal_Content_admin_css_font_awesome_css_font_awesome_css), PathAddress + "\\" + projectFolder + "\\Content\\admin\\css\\font-awesome\\css\\font-awesome.css");
                CopyFromResource(StringToByteArray(Properties.Resources.Normal_Content_admin_js_jquery_gritter_min_js), PathAddress + "\\" + projectFolder + "\\Content\\admin\\js\\jquery.gritter.min.js");
                CopyFromResource(StringToByteArray(Properties.Resources.Normal_Content_admin_css_jquery_gritter_css), PathAddress + "\\" + projectFolder + "\\Content\\admin\\css\\jquery.gritter.css");
                CopyFromResource(StringToByteArray(Properties.Resources.Normal_Content_admin_css_matrix_style_css), PathAddress + "\\" + projectFolder + "\\Content\\admin\\css\\matrix-style.css");
                CopyFromResource(StringToByteArray(Properties.Resources.Normal_Content_admin_js_matrix_js), PathAddress + "\\" + projectFolder + "\\Content\\admin\\js\\matrix.js");
                CopyFromResource(StringToByteArray(Properties.Resources.Normal_Content_admin_js_matrix_tables_js), PathAddress + "\\" + projectFolder + "\\Content\\admin\\js\\matrix.tables.js");

                using (FileStream fs = new FileStream(PathAddress + "\\" + projectFolder + "\\Content\\admin\\js\\script.js", FileMode.Create))
                {
                    using (StreamWriter yaz = new StreamWriter(fs, Encoding.UTF8))
                    {
                        ckEditors = new List<string>();

                        foreach (string Table in selectedTables)
                        {
                            List<ColumnInfo> colNames = tableColumnInfos.Where(a => a.TableName == Table).ToList();
                            colNames = colNames.Where(a => !a.ColumnName.In(UrlColumns, InType.ToUrlLower)).ToList();
                            colNames = colNames.Where(a => !a.ColumnName.In(FileColumns, InType.ToUrlLower)).ToList();
                            colNames = colNames.Where(a => !a.ColumnName.In(ImageColumns, InType.ToUrlLower)).ToList();

                            foreach (ColumnInfo column in colNames)
                            {
                                if (column.Type != null)
                                {
                                    if (column.Type.Name == "String")
                                    {
                                        if (column.CharLength == -1)
                                        {
                                            if (!ckEditors.Contains(column.ColumnName))
                                            {
                                                ckEditors.Add(column.ColumnName);
                                            }
                                        }
                                    }
                                }
                            }
                        }

                        yaz.WriteLine("$(document).ready(function () {");

                        foreach (string item in ckEditors)
                        {
                            yaz.WriteLine("\tif ($(\"#" + item + "\").length > 0) {");
                            yaz.WriteLine("\t\tClassicEditor");
                            yaz.WriteLine("\t\t\t.create(document.querySelector('#" + item + "'), {");
                            yaz.WriteLine("\t\t\t})");
                            yaz.WriteLine("\t\t\t.then(editor => {");
                            yaz.WriteLine("\t\t\t\twindow.editor = editor;");
                            yaz.WriteLine("\t\t\t});");
                            yaz.WriteLine("\t}");
                            yaz.WriteLine("");
                        }

                        yaz.WriteLine("\t/* Login Sayfası*/");
                        yaz.WriteLine("\tif ($(\"#loginbox\").length > 0) {");
                        yaz.WriteLine("\t\t$(\"#txtUserName\").focus();");
                        yaz.WriteLine("");
                        yaz.WriteLine("\t\t$(\"#btnGiris\").click(function () {");
                        yaz.WriteLine("\t\t\tGirisYap();");
                        yaz.WriteLine("\t\t});");
                        yaz.WriteLine("");
                        yaz.WriteLine("\t\t$(\"button.close\").click(function () {");
                        yaz.WriteLine("\t\t\t$(\".alert-error\").fadeOut(\"slow\");");
                        yaz.WriteLine("\t\t});");
                        yaz.WriteLine("");
                        yaz.WriteLine("\t\t$(\"#txtUserName, #txtPassword\").keyup(function (event) {");
                        yaz.WriteLine("\t\t\tif (event.keyCode == 13) {");
                        yaz.WriteLine("\t\t\t\tGirisYap();");
                        yaz.WriteLine("\t\t\t}");
                        yaz.WriteLine("\t\t});");
                        yaz.WriteLine("\t}");
                        yaz.WriteLine("\t/* Login Sayfası*/");
                        yaz.WriteLine("");
                        yaz.WriteLine("\t/* Logout Olayı*/");
                        yaz.WriteLine("\tif ($(\"a.logout\").length > 0) {");
                        yaz.WriteLine("\t\t$(\"a.logout\").click(function () {");
                        yaz.WriteLine("\t\t\t$.ajax({");
                        yaz.WriteLine("\t\t\t\ttype: 'GET',");
                        yaz.WriteLine("\t\t\t\turl: AdminAjaxPath + \"/Logout\",");
                        yaz.WriteLine("\t\t\t\tsuccess: function (answer) {");
                        yaz.WriteLine("\t\t\t\t\tif (answer == true) {");
                        yaz.WriteLine("\t\t\t\t\t\twindow.location = AdminPath + \"/Home/Login\";");
                        yaz.WriteLine("\t\t\t\t\t}");
                        yaz.WriteLine("\t\t\t\t}");
                        yaz.WriteLine("\t\t\t});");
                        yaz.WriteLine("\t\t});");
                        yaz.WriteLine("\t}");
                        yaz.WriteLine("\t/* Logout Olayı*/");
                        yaz.WriteLine("");
                        yaz.WriteLine("\tif (Urling.controller != undefined) {");
                        yaz.WriteLine("\t\tvar activeLi = $(\"#sidebar li[data-url='\" + Urling.controller + \"']\");");
                        yaz.WriteLine("\t\tvar submenuLi = activeLi.parent(\"ul\").parent(\"li\");");
                        yaz.WriteLine("");
                        yaz.WriteLine("\t\t$(\"#sidebar li\").removeClass(\"active\");");
                        yaz.WriteLine("\t\tactiveLi.addClass(\"active\");");
                        yaz.WriteLine("");
                        yaz.WriteLine("\t\tif (submenuLi.hasClass(\"submenu\")) {");
                        yaz.WriteLine("\t\t\tif ($(\"body\").width() > 970 || $(\"body\").width() <= 480) {");
                        yaz.WriteLine("\t\t\t\tsubmenuLi.addClass(\"open\");");
                        yaz.WriteLine("\t\t\t}");
                        yaz.WriteLine("\t\t\tsubmenuLi.addClass(\"active\");");
                        yaz.WriteLine("\t\t}");
                        yaz.WriteLine("\t}");
                        yaz.WriteLine("});");
                        yaz.WriteLine("");
                        yaz.WriteLine("function GirisYap() {");
                        yaz.WriteLine("\t$(\"#imgLoading\").fadeIn(\"slow\");");
                        yaz.WriteLine("");
                        yaz.WriteLine("\tvar username = $(\"#txtUserName\").val();");
                        yaz.WriteLine("\tvar password = $(\"#txtPassword\").val();");
                        yaz.WriteLine("");
                        yaz.WriteLine("\tif (!isValid(username, \"username\")) {");
                        yaz.WriteLine("\t\t$(\"#hataMesaj\").text(\"Lütfen geçerli bir kullanıcı adı giriniz.\");");
                        yaz.WriteLine("\t\t$(\".alert-error\").fadeIn(\"slow\");");
                        yaz.WriteLine("");
                        yaz.WriteLine("\t\t$(\"#imgLoading\").fadeOut(\"slow\");");
                        yaz.WriteLine("\t\treturn false;");
                        yaz.WriteLine("\t}");
                        yaz.WriteLine("");
                        yaz.WriteLine("\tif (!isValid(password, \"password\")) {");
                        yaz.WriteLine("\t\t$(\"#hataMesaj\").text(\"Lütfen geçerli bir şifre giriniz.\");");
                        yaz.WriteLine("\t\t$(\".alert-error\").fadeIn(\"slow\");");
                        yaz.WriteLine("");
                        yaz.WriteLine("\t\t$(\"#imgLoading\").fadeOut(\"slow\");");
                        yaz.WriteLine("\t\treturn false;");
                        yaz.WriteLine("\t}");
                        yaz.WriteLine("");
                        yaz.WriteLine("\tvar loginInfo = new Object();");
                        yaz.WriteLine("\tloginInfo.Username = username;");
                        yaz.WriteLine("\tloginInfo.Password = password;");
                        yaz.WriteLine("");
                        yaz.WriteLine("\t$.ajax({");
                        yaz.WriteLine("\t\ttype: \"POST\",");
                        yaz.WriteLine("\t\turl: AdminAjaxPath + \"/Login\",");
                        yaz.WriteLine("\t\tdata: \"{ login: '\" + JSON.stringify(loginInfo) + \"' }\",");
                        yaz.WriteLine("\t\tdataType: \"json\",");
                        yaz.WriteLine("\t\tcontentType: \"application/json; charset=utf-8\",");
                        yaz.WriteLine("\t\tsuccess: function (answer) {");
                        yaz.WriteLine("\t\t\tif (answer == true) {");
                        yaz.WriteLine("\t\t\t\twindow.location = AdminPath;");
                        yaz.WriteLine("\t\t\t}");
                        yaz.WriteLine("\t\t\telse {");
                        yaz.WriteLine("\t\t\t\t$(\"#hataMesaj\").text(\"Lütfen kullanıcı adı ve şifrenizi kontrol ediniz.\");");
                        yaz.WriteLine("\t\t\t\t$(\".alert-error\").fadeIn(\"slow\");");
                        yaz.WriteLine("");
                        yaz.WriteLine("\t\t\t\t$(\"#imgLoading\").fadeOut(\"slow\");");
                        yaz.WriteLine("\t\t\t}");
                        yaz.WriteLine("\t\t}");
                        yaz.WriteLine("\t});");
                        yaz.WriteLine("}");
                        yaz.WriteLine("");
                        yaz.WriteLine("/* Validation Control */");
                        yaz.WriteLine("function isValid(text, type) {");
                        yaz.WriteLine("\tvar pattern;");
                        yaz.WriteLine("");
                        yaz.WriteLine("\tswitch (type) {");
                        yaz.WriteLine("\t\tcase \"username\": pattern = new RegExp(/^[a-z0-9_-]{3,16}$/); break;");
                        yaz.WriteLine("\t\tcase \"password\": pattern = new RegExp(/^[a-z0-9_-]{3,18}$/); break;");
                        yaz.WriteLine("\t\tcase \"hex\": pattern = new RegExp(/^#?([a-f0-9]{6}|[a-f0-9]{3})$/); break;");
                        yaz.WriteLine("\t\tcase \"rewrite\": pattern = new RegExp(/^[a-z0-9-]+$/); break;");
                        yaz.WriteLine("\t\tcase \"email\": pattern = new RegExp(/^([a-z0-9_\\.-]+)@([\\da-z\\.-]+)\\.([a-z\\.]{2,6})$/); break;");
                        yaz.WriteLine("\t\tcase \"url\": pattern = new RegExp(/^(https?:\\/\\/)?([\\da-z\\.-]+)\\.([a-z\\.]{2,6})([\\/\\w \\.-]*)*\\/?$/); break;");
                        yaz.WriteLine("\t\tcase \"ipaddress\": pattern = new RegExp(/^(?:(?:25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)\\.){3}(?:25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)$/); break;");
                        yaz.WriteLine("\t\tcase \"htmltag\": pattern = new RegExp(/^<([a-z]+)([^<]+)*(?:>(.*)<\\/\\1>|\\s+\\/>)$/); break;");
                        yaz.WriteLine("\t\tdefault: pattern = new RegExp(/^([a-z0-9_\\.-]+)@([\\da-z\\.-]+)\\.([a-z\\.]{2,6})$/); break;");
                        yaz.WriteLine("\t}");
                        yaz.WriteLine("");
                        yaz.WriteLine("\treturn pattern.test(text);");
                        yaz.WriteLine("}");
                        yaz.WriteLine("");
                        yaz.WriteLine("function KelimeAra(txtValue) {");
                        yaz.WriteLine("\tswitch (txtValue) {");
                        foreach (string Table in selectedTables)
                        {
                            yaz.WriteLine("\t\tcase \"" + Table + "\":");
                            yaz.WriteLine("\t\t\twindow.location.href = AdminPath + \"/" + Table + "\";");
                            yaz.WriteLine("\t\t\tbreak;");
                        }
                        yaz.WriteLine("\t\tdefault:");
                        yaz.WriteLine("\t\t\t$.gritter.add({");
                        yaz.WriteLine("\t\t\t\ttitle: 'Arama Sonuç',");
                        yaz.WriteLine("\t\t\t\ttext: 'Aradığınız kelimeye uygun sonuç bulunamadı...',");
                        yaz.WriteLine("\t\t\t\tsticky: false");
                        yaz.WriteLine("\t\t\t});");
                        yaz.WriteLine("\t\t\tbreak;");
                        yaz.WriteLine("\t}");
                        yaz.WriteLine("}");
                        yaz.WriteLine("");
                        yaz.WriteLine("$(function () {");
                        yaz.WriteLine("\tif($('#search input[type=text]').length > 0)");
                        yaz.WriteLine("\t{");
                        yaz.WriteLine("\t\t$('#search input[type=text]').typeahead({");
                        yaz.WriteLine("\t\t\tsource: [");
                        foreach (string Table in selectedTables)
                        {
                            yaz.WriteLine("\t\t\t\t'" + Table + "',");
                        }
                        yaz.WriteLine("\t\t\t],");
                        yaz.WriteLine("\t\t\titems: 4");
                        yaz.WriteLine("\t\t});");
                        yaz.WriteLine("\t}");
                        yaz.WriteLine("");
                        yaz.WriteLine("\t$(document).on(\"keyup\", \"#txtMainSearch\", function () {");
                        yaz.WriteLine("\t\tif (event.keyCode == 13) {");
                        yaz.WriteLine("\t\t\tKelimeAra($(\"#txtMainSearch\").val());");
                        yaz.WriteLine("\t\t}");
                        yaz.WriteLine("\t});");
                        yaz.WriteLine("");
                        yaz.WriteLine("\t$(document).on(\"click\", \"#btnMainSearch\", function () {");
                        yaz.WriteLine("\t\tKelimeAra($(\"#txtMainSearch\").val());");
                        yaz.WriteLine("\t});");
                        yaz.WriteLine("");
                        yaz.WriteLine("\t$(document).on(\"change\", \"input[type=file]\", function () {");
                        yaz.WriteLine("\t\tvar fileid = \"#\" + $(this).attr(\"id\");");
                        yaz.WriteLine("\t\t$(fileid).val($(this).val().replace(\"C:\\\\fakepath\\\\\", \"\"));");
                        yaz.WriteLine("\t});");
                        yaz.WriteLine("");

                        yaz.WriteLine("\t$(document).on(\"click\", \"a.cpyLink, a.btn-copy\", function () {");
                        yaz.WriteLine("\t\t$(\".cpy-yes\").attr(\"data-id\", $(this).attr(\"data-id\"));");
                        yaz.WriteLine("\t\t$(\".cpy-yes\").attr(\"data-link\", $(this).attr(\"data-link\"));");
                        yaz.WriteLine("\t});");
                        yaz.WriteLine("\t$(document).on(\"click\", \"a.cpy-yes\", function () {");
                        yaz.WriteLine("\t\tvar link = $(this);");
                        yaz.WriteLine("\t\tvar url = link.attr(\"data-link\");");
                        yaz.WriteLine("\t\tvar dataID = parseInt(link.attr(\"data-id\"));");
                        yaz.WriteLine("");
                        yaz.WriteLine("\t\t$.ajax({");
                        yaz.WriteLine("\t\t\ttype: 'POST',");
                        yaz.WriteLine("\t\t\turl: AdminPath + \"/\" + url + \"/Copy\",");
                        yaz.WriteLine("\t\t\tdata: \"{ id: \" + dataID + \" }\",");
                        yaz.WriteLine("\t\t\tdataType: \"json\",");
                        yaz.WriteLine("\t\t\tcontentType: \"application/json; charset=utf-8\",");
                        yaz.WriteLine("\t\t\tsuccess: function (answer) {");
                        yaz.WriteLine("\t\t\t\tif (answer == true) {");
                        yaz.WriteLine("\t\t\t\t\t$.gritter.add({");
                        yaz.WriteLine("\t\t\t\t\t\ttitle: 'Sonuç',");
                        yaz.WriteLine("\t\t\t\t\t\ttext: 'İlgili veri kopyalandı.',");
                        yaz.WriteLine("\t\t\t\t\t\tsticky: false");
                        yaz.WriteLine("\t\t\t\t\t});");
                        yaz.WriteLine("");
                        yaz.WriteLine("\t\t\t\t\tsetTimeout(function () {");
                        yaz.WriteLine("\t\t\t\t\t\twindow.location.href = Url;");
                        yaz.WriteLine("\t\t\t\t\t}, 2000);");
                        yaz.WriteLine("\t\t\t\t}");
                        yaz.WriteLine("\t\t\t\telse {");
                        yaz.WriteLine("\t\t\t\t\t$.gritter.add({");
                        yaz.WriteLine("\t\t\t\t\t\ttitle: 'Sonuç',");
                        yaz.WriteLine("\t\t\t\t\t\ttext: 'İlgili veri kopyalanamadı.',");
                        yaz.WriteLine("\t\t\t\t\t\tsticky: false");
                        yaz.WriteLine("\t\t\t\t\t});");
                        yaz.WriteLine("\t\t\t\t}");
                        yaz.WriteLine("\t\t\t}");
                        yaz.WriteLine("\t\t});");
                        yaz.WriteLine("\t});");
                        yaz.WriteLine("\t$(document).on(\"click\", \"a.cpy-no\", function () {");
                        yaz.WriteLine("\t\t$(\".cpy-yes\").removeAttr(\"data-id\");");
                        yaz.WriteLine("\t\t$(\".cpy-yes\").removeAttr(\"data-link\");");
                        yaz.WriteLine("\t});");

                        yaz.WriteLine("\t$(document).on(\"click\", \"a.dltLink\", function () {");
                        yaz.WriteLine("\t\t$(this).addClass(\"active-dlt\");");
                        yaz.WriteLine("\t\t$(\".dlt-yes\").attr(\"data-id\", $(this).attr(\"data-id\"));");
                        yaz.WriteLine("\t\t$(\".dlt-yes\").attr(\"data-link\", $(this).attr(\"data-link\"));");
                        yaz.WriteLine("\t});");
                        yaz.WriteLine("");
                        yaz.WriteLine("\t$(document).on(\"click\", \"a.dlt-yes\", function () {");
                        yaz.WriteLine("\t\tvar link = $(this);");
                        yaz.WriteLine("\t\tvar url = link.attr(\"data-link\");");
                        yaz.WriteLine("\t\tvar dataID = parseInt(link.attr(\"data-id\"));");
                        yaz.WriteLine("");
                        yaz.WriteLine("\t\t$.ajax({");
                        yaz.WriteLine("\t\t\ttype: 'POST',");
                        yaz.WriteLine("\t\t\turl: AdminPath + \"/\" + url + \"/Delete\",");
                        yaz.WriteLine("\t\t\tdata: \"{ id: \" + dataID + \" }\",");
                        yaz.WriteLine("\t\t\tdataType: \"json\",");
                        yaz.WriteLine("\t\t\tcontentType: \"application/json; charset=utf-8\",");
                        yaz.WriteLine("\t\t\tsuccess: function (answer) {");
                        yaz.WriteLine("\t\t\t\tif (answer == true) {");
                        yaz.WriteLine("\t\t\t\t\t$.gritter.add({");
                        yaz.WriteLine("\t\t\t\t\t\ttitle: 'Sonuç',");
                        yaz.WriteLine("\t\t\t\t\t\ttext: 'İlgili veri silindi.',");
                        yaz.WriteLine("\t\t\t\t\t\tsticky: false");
                        yaz.WriteLine("\t\t\t\t\t});");
                        yaz.WriteLine("");
                        yaz.WriteLine("\t\t\t\t\t$(\"a.dltLink.active-dlt\").parent(\"li\").parent(\"ul\").parent(\"div\").parent(\"td\").parent(\"tr\").fadeOut(\"slow\", function () {");
                        yaz.WriteLine("\t\t\t\t\t\t$(this).remove();");
                        yaz.WriteLine("\t\t\t\t\t});");
                        yaz.WriteLine("\t\t\t\t}");
                        yaz.WriteLine("\t\t\t\telse {");
                        yaz.WriteLine("\t\t\t\t\t$.gritter.add({");
                        yaz.WriteLine("\t\t\t\t\t\ttitle: 'Sonuç',");
                        yaz.WriteLine("\t\t\t\t\t\ttext: 'İlgili veri silinemedi.',");
                        yaz.WriteLine("\t\t\t\t\t\tsticky: false");
                        yaz.WriteLine("\t\t\t\t\t});");
                        yaz.WriteLine("\t\t\t\t}");
                        yaz.WriteLine("\t\t\t}");
                        yaz.WriteLine("\t\t});");
                        yaz.WriteLine("\t});");
                        yaz.WriteLine("");
                        yaz.WriteLine("\t$(document).on(\"click\", \"a.dlt-no\", function () {");
                        yaz.WriteLine("\t\t$(\".dlt-yes\").removeAttr(\"data-id\");");
                        yaz.WriteLine("\t\t$(\".dlt-yes\").removeAttr(\"data-link\");");
                        yaz.WriteLine("\t\t$(\"a.dltLink\").removeClass(\"active-dlt\");");
                        yaz.WriteLine("\t});");
                        yaz.WriteLine("");

                        yaz.WriteLine("\t$(document).on(\"click\", \"a.rmvLink\", function () {");
                        yaz.WriteLine("\t\t$(this).addClass(\"active-rmv\");");
                        yaz.WriteLine("\t\t$(\".rmv-yes\").attr(\"data-id\", $(this).attr(\"data-id\"));");
                        yaz.WriteLine("\t\t$(\".rmv-yes\").attr(\"data-link\", $(this).attr(\"data-link\"));");
                        yaz.WriteLine("\t});");
                        yaz.WriteLine("");
                        yaz.WriteLine("\t$(document).on(\"click\", \"a.rmv-yes\", function () {");
                        yaz.WriteLine("\t\tvar link = $(this);");
                        yaz.WriteLine("\t\tvar url = link.attr(\"data-link\");");
                        yaz.WriteLine("\t\tvar dataID = parseInt(link.attr(\"data-id\"));");
                        yaz.WriteLine("");
                        yaz.WriteLine("\t\t$.ajax({");
                        yaz.WriteLine("\t\t\ttype: 'POST',");
                        yaz.WriteLine("\t\t\turl: AdminPath + \"/\" + url + \"/Remove\",");
                        yaz.WriteLine("\t\t\tdata: \"{ id: \" + dataID + \" }\",");
                        yaz.WriteLine("\t\t\tdataType: \"json\",");
                        yaz.WriteLine("\t\t\tcontentType: \"application/json; charset=utf-8\",");
                        yaz.WriteLine("\t\t\tsuccess: function (answer) {");
                        yaz.WriteLine("\t\t\t\tif (answer == true) {");
                        yaz.WriteLine("\t\t\t\t\t$.gritter.add({");
                        yaz.WriteLine("\t\t\t\t\t\ttitle: 'Sonuç',");
                        yaz.WriteLine("\t\t\t\t\t\ttext: 'İlgili veri kaldırıldı.',");
                        yaz.WriteLine("\t\t\t\t\t\tsticky: false");
                        yaz.WriteLine("\t\t\t\t\t});");
                        yaz.WriteLine("");
                        yaz.WriteLine("\t\t\t\t\t$(\"a.rmvLink.active-rmv\").parent(\"li\").parent(\"ul\").parent(\"div\").parent(\"td\").parent(\"tr\").fadeOut(\"slow\", function () {");
                        yaz.WriteLine("\t\t\t\t\t\t$(this).remove();");
                        yaz.WriteLine("\t\t\t\t\t});");
                        yaz.WriteLine("\t\t\t\t}");
                        yaz.WriteLine("\t\t\t\telse {");
                        yaz.WriteLine("\t\t\t\t\t$.gritter.add({");
                        yaz.WriteLine("\t\t\t\t\t\ttitle: 'Sonuç',");
                        yaz.WriteLine("\t\t\t\t\t\ttext: 'İlgili veri kaldırılamadı.',");
                        yaz.WriteLine("\t\t\t\t\t\tsticky: false");
                        yaz.WriteLine("\t\t\t\t\t});");
                        yaz.WriteLine("\t\t\t\t}");
                        yaz.WriteLine("\t\t\t}");
                        yaz.WriteLine("\t\t});");
                        yaz.WriteLine("\t});");
                        yaz.WriteLine("");
                        yaz.WriteLine("\t$(document).on(\"click\", \"a.rmv-no\", function () {");
                        yaz.WriteLine("\t\t$(\".rmv-yes\").removeAttr(\"data-id\");");
                        yaz.WriteLine("\t\t$(\".rmv-yes\").removeAttr(\"data-link\");");
                        yaz.WriteLine("\t\t$(\"a.rmvLink\").removeClass(\"active-rmv\");");
                        yaz.WriteLine("\t});");
                        yaz.WriteLine("");
                        yaz.WriteLine("\t$(document).on(\"click\", \".dropdown-toggle\", function () {");
                        yaz.WriteLine("\t\t$(this).parent().addClass(\"open\");");
                        yaz.WriteLine("\t});");
                        yaz.WriteLine("});");

                        yaz.Close();
                    }
                }
            }

            CopyFromResource(StringToByteArray(Properties.Resources.Shared_Content_admin_js_bootstrap_min_js), PathAddress + "\\" + projectFolder + "\\Content\\admin\\js\\bootstrap.min.js");
            CopyFromResource(StringToByteArray(Properties.Resources.Shared_Content_admin_js_jquery_dataTables_min_js), PathAddress + "\\" + projectFolder + "\\Content\\admin\\js\\jquery.dataTables.min.js");
            CopyFromResource(StringToByteArray(Properties.Resources.Shared_Content_admin_css_bootstrap_min_css), PathAddress + "\\" + projectFolder + "\\Content\\admin\\css\\bootstrap.min.css");
            CopyFromResource(StringToByteArray(Properties.Resources.Shared_Content_admin_css_bootstrap_responsive_min_css), PathAddress + "\\" + projectFolder + "\\Content\\admin\\css\\bootstrap-responsive.min.css");
            CopyFromResource(StringToByteArray(Properties.Resources.Shared_Content_admin_css_matrix_login_css), PathAddress + "\\" + projectFolder + "\\Content\\admin\\css\\matrix-login.css");
            CopyFromResource(StringToByteArray(Properties.Resources.Shared_Content_admin_css_matrix_media_css), PathAddress + "\\" + projectFolder + "\\Content\\admin\\css\\matrix-media.css");
        }

        void CreateJquery()
        {
            CopyFromResource(StringToByteArray(Properties.Resources.Shared_Content_js_jquery_jquery_min_js), PathAddress + "\\" + projectFolder + "\\Content\\js\\jquery\\jquery.min.js");

            if (!chkAngular.Checked)
            {
                CopyFromResource(StringToByteArray(Properties.Resources.Shared_Content_js_jquery_json2_js), PathAddress + "\\" + projectFolder + "\\Content\\js\\jquery\\json2.js");
            }
        }

        void CreateImages()
        {
            CopyFromResource(BitmapToByteArray(Properties.Resources.Shared_Content_admin_img_active_png), PathAddress + "\\" + projectName + "\\Content\\admin\\img\\active.png");
            CopyFromResource(BitmapToByteArray(Properties.Resources.Shared_Content_admin_img_breadcrumb_png), PathAddress + "\\" + projectName + "\\Content\\admin\\img\\breadcrumb.png");
            CopyFromResource(BitmapToByteArray(Properties.Resources.Shared_Content_admin_img_glyphicons_halflings_white_png), PathAddress + "\\" + projectName + "\\Content\\admin\\img\\glyphicons-halflings-white.png");
            CopyFromResource(BitmapToByteArray(Properties.Resources.Shared_Content_admin_img_line_png), PathAddress + "\\" + projectName + "\\Content\\admin\\img\\line.png");
            CopyFromResource(BitmapToByteArray(Properties.Resources.Shared_Content_admin_img_logo_png), PathAddress + "\\" + projectName + "\\Content\\admin\\img\\logo.png");
            CopyFromResource(BitmapToByteArray(Properties.Resources.Shared_Content_admin_img_menu_active_png), PathAddress + "\\" + projectName + "\\Content\\admin\\img\\menu-active.png");
            CopyFromResource(BitmapToByteArray(Properties.Resources.Shared_Content_admin_img_passive_png), PathAddress + "\\" + projectName + "\\Content\\admin\\img\\passive.png");
            CopyFromResource(BitmapToByteArray(Properties.Resources.Shared_Content_admin_img_loading_gif), PathAddress + "\\" + projectName + "\\Content\\admin\\img\\loading.gif");
            CopyFromResource(BitmapToByteArray(Properties.Resources.Shared_Content_admin_img_gritter_png), PathAddress + "\\" + projectName + "\\Content\\admin\\img\\gritter.png");
            CopyFromResource(BitmapToByteArray(Properties.Resources.Shared_Content_admin_img_larrow_png), PathAddress + "\\" + projectName + "\\Content\\admin\\img\\larrow.png");
            CopyFromResource(BitmapToByteArray(Properties.Resources.Shared_Content_admin_img_rarrow_png), PathAddress + "\\" + projectName + "\\Content\\admin\\img\\rarrow.png");
        }

        void CreateFontAwesome()
        {
            CopyFromResource(Properties.Resources.Angular_Content_admin_css_font_awesome_FontAwesome_otf, PathAddress + "\\" + projectFolder + "\\Content\\admin\\css\\font-awesome\\FontAwesome.otf");
            CopyFromResource(Properties.Resources.Angular_Content_admin_css_font_awesome_fontawesome_webfont_eot, PathAddress + "\\" + projectFolder + "\\Content\\admin\\css\\font-awesome\\fontawesome-webfont.eot");
            CopyFromResource(Properties.Resources.Angular_Content_admin_css_font_awesome_fontawesome_webfont_svg, PathAddress + "\\" + projectFolder + "\\Content\\admin\\css\\font-awesome\\fontawesome-webfont.svg");
            CopyFromResource(Properties.Resources.Angular_Content_admin_css_font_awesome_fontawesome_webfont_ttf, PathAddress + "\\" + projectFolder + "\\Content\\admin\\css\\font-awesome\\fontawesome-webfont.ttf");
            CopyFromResource(Properties.Resources.Angular_Content_admin_css_font_awesome_fontawesome_webfont_woff, PathAddress + "\\" + projectFolder + "\\Content\\admin\\css\\font-awesome\\fontawesome-webfont.woff");
        }

        void CreateCKEditor()
        {
            CopyFromResource(StringToByteArray(Properties.Resources.Shared_Content_admin_js_ckeditor_ckeditor_js), PathAddress + "\\" + projectFolder + "\\Content\\admin\\js\\ckeditor\\ckeditor.js");
            CopyFromResource(StringToByteArray(Properties.Resources.Shared_Content_admin_js_ckeditor_ckeditor_js_map), PathAddress + "\\" + projectFolder + "\\Content\\admin\\js\\ckeditor\\ckeditor.js.map");
            CopyFromResource(StringToByteArray(Properties.Resources.Shared_Content_admin_js_ckeditor_translations_en_au_js), PathAddress + "\\" + projectFolder + "\\Content\\admin\\js\\ckeditor\\translations\\en-au.js");
            CopyFromResource(StringToByteArray(Properties.Resources.Shared_Content_admin_js_ckeditor_translations_tr_js), PathAddress + "\\" + projectFolder + "\\Content\\admin\\js\\ckeditor\\translations\\tr.js");
        }

        void CreateDllFiles()
        {
            CopyFromResource(Properties.Resources.Shared_bin_TDLibrary_dll, PathAddress + "\\" + projectFolder + "\\bin\\TDLibrary.dll");
        }

        void CopyFromResource(byte[] resourceFile, string destFile)
        {
            File.WriteAllBytes(destFile, resourceFile);
        }

        byte[] StringToByteArray(string file, FileFormat fileFormat = FileFormat.UTF8)
        {
            if (fileFormat == FileFormat.UTF8)
                return Encoding.UTF8.GetBytes(file);
            else if(fileFormat == FileFormat.Unicode)
                return Encoding.Unicode.GetBytes(file);
            else if (fileFormat == FileFormat.ASCII)
                return Encoding.ASCII.GetBytes(file);
            else
                return Encoding.UTF8.GetBytes(file);
        }

        public enum FileFormat
        {
            UTF8,
            Unicode,
            ASCII
        }

        byte[] BitmapToByteArray(Bitmap file)
        {
            byte[] result = null;

            if (file != null)
            {
                MemoryStream stream = new MemoryStream();
                file.Save(stream, file.RawFormat);
                result = stream.ToArray();
            }

            return result;
        }

        #endregion
    }
}
