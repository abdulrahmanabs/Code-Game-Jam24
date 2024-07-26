using MoreMountains.Feedbacks;
using MoreMountains.Tools;
#if MM_TEXTMESHPRO
using TMPro;
#endif
using UnityEngine;
using UnityEngine.UI;

namespace MoreMountains.Feel
{
	[AddComponentMenu("")]
	public class FeelSpringsVector2Demo : MonoBehaviour
	{
		[Header("Spring")]
		public MMSpringFloat SpringX;
		public MMSpringFloat SpringY;

		[Header("Bindings")] 
		public FeelSpringsDemoSlider DampingXSlider;
		public FeelSpringsDemoSlider FrequencyXSlider;
		public FeelSpringsDemoSlider DampingYSlider;
		public FeelSpringsDemoSlider FrequencyYSlider;
		public FeelSpringsDemoSlider BumpAmountSlider;
		public Transform MovingObject;

		protected Vector3 _newPosition;
		protected float _range = 0.375f;

		protected virtual void OnEnable()
		{
			SpringX.CurrentValue = 0f;
			SpringX.TargetValue = 0f;
			SpringX.Velocity = 0f;
			SpringY.CurrentValue = 0f;
			SpringY.TargetValue = 0f;
			SpringY.Velocity = 0f;
		}
		
		public virtual void RandomMove()
		{
			SpringX.MoveTo(UnityEngine.Random.Range(-1f,1f));
			SpringY.MoveTo(UnityEngine.Random.Range(-1f,1f));
		}

		public virtual void RandomBump()
		{
			float bumpAmount = BumpAmountSlider.value;
			SpringX.BumpRandom(-bumpAmount, bumpAmount);
			SpringY.BumpRandom(-bumpAmount, bumpAmount);
		}
		
		protected virtual void Update()
		{
			SpringX.Damping = DampingXSlider.value;
			SpringY.Damping = DampingYSlider.value;
			SpringX.Frequency = FrequencyXSlider.value;
			SpringY.Frequency = FrequencyYSlider.value;
			SpringX.UpdateSpringValue(Time.deltaTime);
			SpringY.UpdateSpringValue(Time.deltaTime);

			_newPosition = MovingObject.transform.localPosition;
			
			_newPosition.x = MMMaths.Remap(SpringX.CurrentValue, -1f, 1f, -_range, _range);
			_newPosition.y = MMMaths.Remap(SpringY.CurrentValue, -1f, 1f, -_range, _range);

			MovingObject.transform.localPosition = _newPosition;
		}
	}
}
