using UnityEngine;

public class Movement : MonoBehaviour, IWaypointable
{
    [SerializeField] private float _movementSpeed, _turnSpeed, _accelerationSpeed;
    [SerializeField] private Transform _lookAtHanndler, _modelOrigin;

    private float _elapsedTime, _currentMoveSpeed, _currentTurnSpeed, _h;
    private bool _canRotate = false;
    private Vector3 _lookAtPos;

    public Vector3 AimTracker;
    // Update is called once per frame
    private void FixedUpdate()
    {
        Move();
        if (_elapsedTime <= _accelerationSpeed)
        {
            LerpSpeed();
        }
        /*if (Input.GetKeyDown(KeyCode.F))
        {
            DoRotate();
        }*/
        if (_canRotate)
        {
            Rotate();
            RotateModel();
        }
    }

    public void SetStats(float moveSpeed, float turnSpeed, float accelerationSpeed)
    {
        _movementSpeed = moveSpeed;
        _turnSpeed = turnSpeed;
        _accelerationSpeed = accelerationSpeed;
    }

    public void SetWaypoint(Vector3 pos)
    {
        _lookAtPos = new Vector3(pos.x, 0, pos.z);
        DoRotate();
    }

    private void Move()
    {
        this.transform.position += this.transform.forward * (_currentMoveSpeed * 0.1f);
    }

    public void DoRotate()
    {
        _currentTurnSpeed = _turnSpeed * 0.01f;
        _canRotate = true;

       
    }
        

    void Rotate()
    {
        Vector3 targetDir = _lookAtPos - this.transform.position;
        targetDir.y = 0;
        float step = _currentTurnSpeed * Time.deltaTime * 10;

        Vector3 newDir = Vector3.RotateTowards(transform.forward, targetDir, step, 0f);
        transform.rotation = Quaternion.LookRotation(newDir);

        if(transform.position.x == targetDir.x && transform.position.z == targetDir.z)
        {
            _canRotate = false;
        }
    }
    void RotateModel()
    {
        if(_h > 1)
        {
            _modelOrigin.rotation = Quaternion.Euler(new Vector3(0, 0, 20 -_currentTurnSpeed) + this.transform.rotation.eulerAngles);
             
        }
    }

        private void LerpSpeed()
        {
            _elapsedTime += Time.deltaTime;
            float percentageComplete = _elapsedTime / _accelerationSpeed;

            _currentMoveSpeed = Mathf.Lerp(0, _movementSpeed, percentageComplete);
        }
        private void ResetTime()
        {
            _elapsedTime = 0;
        }

   

    /*void Rotate()
        {
            if (_lookAtHanndler.rotation.y < 0)
            {
                _currentTurnSpeed = _turnSpeed * -1;
            }
            _lookAtHanndler.LookAt(new Vector3(_lookAtPos.x, 0, _lookAtPos.z));
            _h = _lookAtHanndler.localRotation.eulerAngles.y;

            this.transform.Rotate(Vector3.up * _currentTurnSpeed);

            if (_h >= -3 && _h <= 3)
            {
                this.transform.LookAt(new Vector3(_lookAtPos.x, 0, _lookAtPos.z));

                _canRotate = false;
                _currentTurnSpeed = _turnSpeed;
                _modelOrigin.rotation = this.transform.rotation;
            }
        }*/
}