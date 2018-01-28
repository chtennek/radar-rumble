using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefaultInputReceiver : InputReceiver
{
    public AxesList axesList;
    private static List<string> axes;
    private Hashtable buttonDown = new Hashtable();
    private Hashtable buttonUp = new Hashtable();

    private void Awake()
    {
        if (axes == null && axesList != null && axesList.axes.Length > 0)
        {
            axes = new List<string>();
            foreach (string axisName in axesList.axes)
            {
                axes.Add(axisName);
            }
        }
    }

    private new void Update()
    {
        base.Update();
        foreach (string s in axes)
        {
            if (Input.GetButtonDown(s) && !buttonDown.ContainsKey(s))
            {
                buttonDown.Add(s, true);
            }
            else if (Input.GetButtonUp(s) && !buttonUp.ContainsKey(s))
            {
                buttonUp.Add(s, true);
            }
        }
    }

    private void FixedUpdate()
    {
        buttonDown.Clear();
        buttonUp.Clear();
    }

    private string WrapPlayerNum(string id)
    {
        return (playerId + 1).ToString() + ". " + id;
    }

    // [TODO] don't hardcode axes names
    public override Vector2 PollMovementVector()
    {
        float x = Input.GetAxisRaw(WrapPlayerNum("Move Horizontal"));
        float y = Input.GetAxisRaw(WrapPlayerNum("Move Vertical"));
        return new Vector2(x, y);
    }

    public override Vector2 PollAimVector()
    {
        float x = Input.GetAxisRaw(WrapPlayerNum("Aim Horizontal"));
        float y = Input.GetAxisRaw(WrapPlayerNum("Aim Vertical"));
        return new Vector2(x, y);
    }

    public override bool GetButtonDown(string id)
    {
        if (!Time.inFixedTimeStep)
        {
            return Input.GetButtonDown(WrapPlayerNum(id));
        }
        else
        {
            return buttonDown.ContainsKey(WrapPlayerNum(id));
        }
    }

    public override bool GetButtonUp(string id)
    {
        if (!Time.inFixedTimeStep)
        {
            return Input.GetButtonUp(WrapPlayerNum(id));
        }
        else
        {
            return buttonUp.ContainsKey(WrapPlayerNum(id));
        }
    }

    public override bool GetButton(string id) { return Input.GetButton(WrapPlayerNum(id)); }
}
