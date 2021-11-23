public class CameraObserver : Singleton<CameraObserver>
{
    public CameraObservable CameraObservable { get; set; }

    private void LateUpdate()
    {
        Observe();
    }

    public void Observe()
    {
        if (CameraObservable == null) return;

        transform.position = CameraObservable.CameraPosition;

        if (CameraObservable.LookAtObject)
        {
            transform.LookAt(CameraObservable.Transform);
            transform.eulerAngles += CameraObservable.LookAtEulers;
        }
        else 
        {
            transform.eulerAngles = CameraObservable.Eulers;
        }       
    }
}