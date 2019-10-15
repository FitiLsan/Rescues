
using Rescues;
using System.Collections.Generic;
using System;


public class ToolBox : Singleton<ToolBox>
{
    private Dictionary<Type, Object> data = new Dictionary<Type, Object>();

    // Type - ключ менеджера// 

    public static void Add(object obj)
    {
        var add = obj;
        var manager = obj as ManagerBase;

        if (manager != null)
            add = Instantiate(manager);
        else return;

        Instance.data.Add(obj.GetType(), add);

        if (add is IAwake)
        {
            (add as IAwake).OnAwake();
        }
    }
    //можем добавить в тул бокс менеджеры//

    public static T Get<T>()
    {
        object resolve;
        Instance.data.TryGetValue(typeof(T), out resolve);
        return (T)resolve;
    }
    // можем можем получить данные о менеджере//



    public void ClearScene()
    {

    }
}