using UnityEngine;
using System.Collections;

public abstract class AbstractTargetFollower : MonoBehaviour {

    public enum UpdateType
    {
        Fixed,
        Late,
        Manual,
    }

    [SerializeField]    protected Transform m_Target;
    [SerializeField]    private bool m_AutoTargetPlayer = true;
    [SerializeField]    private UpdateType m_UpdateType;

    protected Rigidbody m_TargetRigidBody;

	// Use this for initialization
	protected virtual void Start () 
    {
        if (m_AutoTargetPlayer)
        {
            FindAndTargetPlayer();
        }

        if (m_Target == null) return;
        m_TargetRigidBody = m_Target.GetComponent<Rigidbody>();
	}
	
	// Update is called once per frame
	private void FixedUpdate () 
    {

        if (m_AutoTargetPlayer && (m_Target == null || !m_Target.gameObject.activeSelf))
        {
            FindAndTargetPlayer();
        }

        if (m_UpdateType == UpdateType.Fixed)
        {
            FollowTarget(Time.deltaTime);
        }
	}

    private void LateUpdate()
    {
        if (m_AutoTargetPlayer && (m_Target == null || !m_Target.gameObject.activeSelf))
        {
            FindAndTargetPlayer();
        }

        if (m_UpdateType == UpdateType.Late)
        {
            FollowTarget(Time.deltaTime);
        }
    }


    public void ManualUpdate()
    {
        if (m_AutoTargetPlayer && (m_Target == null || !m_Target.gameObject.activeSelf))
        {
            FindAndTargetPlayer();
        }

        if (m_UpdateType == UpdateType.Manual)
        {
            FollowTarget(Time.deltaTime);
        }
    }

    protected abstract void FollowTarget(float deltaTime);

    public void FindAndTargetPlayer()
    {
        var targetObj = GameObject.FindGameObjectWithTag("Player");
        if (targetObj)
        {
            SetTarget(targetObj.transform);
        }
    }

    public virtual void SetTarget(Transform newTransform)
    {
        m_Target = newTransform;
    }

    public Transform Target
    {
        get { return m_Target; }
    }
}
