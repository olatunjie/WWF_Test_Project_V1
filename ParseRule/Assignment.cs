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

/// <summary>
/// Summary description for Assignment
/// </summary>
public class Assignment
{
    private int assID;
    private string task;
    private string assignedUser;
    private string originatingUser;
    private string parentUser;
    private string state;
    private string request;
    private int docID;

    public int AssID
    {
        get { return assID; }
        set { ask = value; }
    }

    public string Task
    {
        get { return task; }
        set { task = value; }
    }

    public string AssignedUser
    {
        get { return assignedUser; }
        set { assignedUser = value; }
    }

    public string OriginatingUser
    {
        get { return originatingUser; }
        set { originatingUser = value; }
    }

    public string ParentUser
    {
        get { return parentUser; }
        set { parentUser = value; }
    }

    public string State
    {
        get { return state; }
        set { state = value; }
    }

    public string Request
    {
        get { return request; }
        set { request = value; }
    }
    
    

	public Assignment()
	{
		
	}
}
