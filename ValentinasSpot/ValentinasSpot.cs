using UnityEngine;

namespace ValentinasSpot
{
    public class ValentinasSpot : PartModule
    {
        private KerbalSeat _seatModule;

        [KSPAction(advancedTweakable = true, guiName = "#SSC_VS_000001",
            requireFullControl = false)]
        public void EjectAction(KSPActionParam param)
        {
            Debug.Log("[SSC_VS] Boop");
            if (_seatModule != null && _seatModule.Occupant != null)
            {
                KerbalEVA evaModule = _seatModule.Occupant.FindModuleImplementing<KerbalEVA>();
                if(evaModule == null)
                {
                    Debug.LogError("[SSC_VS] Kerbal EVA module not found.");
                    return;
                }

                evaModule.OnDeboardSeat();
            }
        }

        public override void OnStartFinished(StartState state)
        {
            base.OnStartFinished(state);

            Actions["EjectAction"].active = false;

            _seatModule = part.Modules.GetModule<KerbalSeat>();
            if (_seatModule == null)
            {
                Debug.LogError("[SSC_VS] KerbalSeat module not found.");
                return;
            }

            Actions["EjectAction"].active = true;
        }
    }
}
