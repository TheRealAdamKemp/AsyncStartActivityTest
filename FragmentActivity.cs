using Android.App;
using Android.Content;
using Android.OS;

namespace AsyncStartActivityTest
{
    /// <summary>
    /// An Activity that uses a retained Fragment for its implementation.
    /// </summary>
    public abstract class FragmentActivity<TFragment> : Activity where TFragment : FragmentBase, new()
    {
        /// <summary>
        /// The top-level fragment which manages the view and state for this activity.
        /// </summary>
        public FragmentBase Fragment { get; protected set; }

        /// <summary>
        /// The tag string to use when finding or creating this activity's fragment. This will be contructed using the type of this generic instance.
        /// </summary>
        protected string FragmentTag
        {
            get
            {
                return GetType().Name;
            }
        }

        /// <summary>
        /// Loads the fragment for this activity and stores it in the Fragment property.
        /// </summary>
        protected virtual void LoadFragment()
        {
            Fragment = FragmentBase.FindOrCreateFragment<TFragment>(this, FragmentTag, Android.Resource.Id.Content);
        }

        /// <inheritdoc />
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            LoadFragment();
        }

        /// <inheritdoc />
        public override void OnAttachedToWindow()
        {
            base.OnAttachedToWindow();
            Fragment.OnAttachedToWindow();
        }

        /// <inheritdoc />
        protected override void OnNewIntent(Intent intent)
        {
            Fragment.OnNewIntent(intent);
        }

        /// <inheritdoc />
        protected override void OnActivityResult(int requestCode, Result resultCode, Intent data)
        {
            Fragment.OnActivityResult(requestCode, resultCode, data);
        }
    }
}
