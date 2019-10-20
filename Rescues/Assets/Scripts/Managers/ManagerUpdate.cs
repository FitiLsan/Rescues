using UnityEngine;
using UnityEditor;
using System.Collections;
using System.Collections.Generic;
using Rescues;


namespace Rescues
{
    [CreateAssetMenu(fileName = "ManagerUpdate", menuName = "Managers/ManagerUpdate")]

    public class ManagerUpdate : ManagerBase, IAwake
    {

        private List<ITick> _ticks = new List<ITick>();
        private List<ITickFixed> _ticksFixes = new List<ITickFixed>();
        private List<ITickLate> _ticksLate = new List<ITickLate>();


        public static void AddTo(object updateble)
        {
            var mngUpdate = ToolBox.Get<ManagerUpdate>();
            if (updateble is ITick)
                mngUpdate._ticks.Add(updateble as ITick);

            if (updateble is ITickFixed)
                mngUpdate._ticksFixes.Add(updateble as ITickFixed);

            if (updateble is ITickLate)
                mngUpdate._ticksLate.Add(updateble as ITickLate);
        }


        public static void RemoveFrom(object updateble)
        {

            var mngUpdate = ToolBox.Get<ManagerUpdate>();
            if (updateble is ITick)
                mngUpdate._ticks.Remove(updateble as ITick);

            if (updateble is ITickFixed)
                mngUpdate._ticksFixes.Remove(updateble as ITickFixed);

            if (updateble is ITickLate)
                mngUpdate._ticksLate.Remove(updateble as ITickLate);
        }


        public void Tick()
        {
            for (var i = 0; i < _ticks.Count; i++)
            {
                _ticks[i].Tick();
            }
        }

        public void TickFixed()
        {
            for (var i = 0; i < _ticksFixes.Count; i++)
            {
                _ticksFixes[i].TickFixed();
            }
        }

        public void TickLate()
        {
            for (var i = 0; i < _ticksLate.Count; i++)
            {
                _ticksLate[i].TickLate();
            }
        }

        public void OnAwake()
        {
            GameObject.Find("[SETUP]").AddComponent<ManagerUpdateComponent>().Setup(this);
        }
    }
}