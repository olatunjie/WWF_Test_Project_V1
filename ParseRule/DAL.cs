using System;
using System.Data;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using System.Collections;
using System.Collections.Generic;
using System.Data.SqlClient;


/// <summary>
/// Summary description for DAL
/// </summary>
public class DAL
{
    private static string connectionString = ConfigurationManager.ConnectionStrings["ORMConnectionString"].ConnectionString;
    private static SqlConnection conn;
    private static SqlCommand cmd;

	static DAL()
	{
        conn = new SqlConnection(connectionString);
        conn.Open();
	}
    
    public static IDataReader getDocPropertyByID(int docPropID)
    {
        cmd = new SqlCommand("spGetDocumentPropertyByID", conn);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.Add("@DocPropID", SqlDbType.Int).Value = docPropID;
        return cmd.ExecuteReader();        
    }


    public static DocProperty getDocProperties(int docID)
    {
        cmd = new SqlCommand("spGetDocumentsProperties", conn);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.Add("@DocPropID", SqlDbType.Int).Value = docID;
        return cmd.ExecuteReader();
    }


    public static int saveDocProperty(DocProperty docProperty, int docID, string docType)
    {
        cmd = new SqlCommand("spSaveDocumentsProperties", conn);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.Add("@DocPropID", SqlDbType.Int).Value = DocProperty.DocPropID;
        cmd.Parameters.Add("@Type", SqlDbType.NVarChar, 200).Value = DocProperty.Type;
        cmd.Parameters.Add("@Value", SqlDbType.NVarChar,200).Value = DocProperty.Value;
        cmd.Parameters.Add("@DocID", SqlDbType.Int).Value = docID;
        cmd.Parameters.Add("@DocType", SqlDbType.NVarChar, 50).Value = docType;
        return cmd.ExecuteNonQuery();
    }

    public static IDataReader getRuleByID(int ruleID)
    {
        cmd = new SqlCommand("spGetRuleByID", conn);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.Add("@RuleID", SqlDbType.Int).Value = ruleID;
        return cmd.ExecuteReader();        
    }

    public static int saveRule(Rule rule)
    {
        cmd = new SqlCommand("spSaveDocumentsProperties", conn);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.Add("@RuleID", SqlDbType.Int).Value = Rule.RuleID;
        cmd.Parameters.Add("@Name", SqlDbType.NVarChar, 100).Value = Rule.Name;
        cmd.Parameters.Add("@Action", SqlDbType.NVarChar, 100).Value = Rule.Action;
        cmd.Parameters.Add("@Condition", SqlDbType.NVarChar, 250).Value = Rule.Condition;
        cmd.Parameters.Add("@ErrorMsg", SqlDbType.NVarChar, 250).Value = Rule.ErrorMsg;
        cmd.Parameters.Add("@Precedence", SqlDbType.Int).Value = Rule.Precedence;
        cmd.Parameters.Add("@ElseAction", SqlDbType.NVarChar, 100).Value = Rule.ElseAction;
        return cmd.ExecuteNonQuery();
    }

    public static Assignment getAssignmentByID(int assID)
    { 
        cmd = new SqlCommand("spGetAssignmentsByID", conn);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.Add("@assID", SqlDbType.Int).Value = assID;
        return cmd.ExecuteReader();
    }

    public static int saveAssignment(Assignment assignment)
    {
        cmd = new SqlCommand("spSaveDocumentsProperties", conn);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.Add("@AssID", SqlDbType.Int).Value = Assignment.AssID;
        cmd.Parameters.Add("@Task", SqlDbType.NVarChar, 100).Value = Assignment.Task;
        cmd.Parameters.Add("@DocumentID", SqlDbType.Int).Value = Assignment.DocumentID;
        cmd.Parameters.Add("@AssignedUser", SqlDbType.NVarChar, 100).Value = Assignment.AssignedUser;
        cmd.Parameters.Add("@OriginatingUser", SqlDbType.NVarChar, 100).Value = Assignment.OriginatingUser;
        cmd.Parameters.Add("@ParentTask", SqlDbType.NVarChar, 100).Value = Assignment.ParentTask;
        cmd.Parameters.Add("@State", SqlDbType.NVarChar, 100).Value = Assignment.State;
        cmd.Parameters.Add("@Request", SqlDbType.NVarChar, 100).Value = Assignment.Request;
        return cmd.ExecuteNonQuery();
    }    


}
