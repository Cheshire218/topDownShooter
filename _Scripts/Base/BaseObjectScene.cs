using System;
using System.Linq;
using UnityEngine;

namespace MyShooter
{
    /// <summary>
    /// Базовый класс для всех объектов сцены
    /// </summary>
    public abstract class BaseObjectScene : MonoBehaviour
    {

        private int _layer;
        private Color _color;
        private Vector3 _center;
        private Bounds _bound;
        private bool _isVisible;
        [HideInInspector] public Rigidbody MyRigidBody;
        [HideInInspector] public Transform MyTransform { get; set; }
        [HideInInspector] public GameObject MyGameObject { get; set; }

        #region Properties;
        /// <summary>
        /// Свойство возвращает слой объекта или меняет слой объекту и всем вложенным дочерним объектам
        /// </summary>
        public int Layer
        {
            get
            {
                return _layer;
            }

            set
            {
                _layer = value;
                AskLayer(MyTransform, _layer);
            }
        }

        /// <summary>
        /// Имя объекта
        /// </summary>
        public string Name
        {
            get
            {
                return MyGameObject.name;
            }
            set
            {
                MyGameObject.name = value;
            }
        }

        /// <summary>
        /// Цвет объекта
        /// </summary>
        public Color Color
        {
            get
            {
                return _color;
            }
            set
            {
                _color = value;
                AskColor(MyTransform, _color);
            }
        }

        //public Vector3 Center
        //{
        //    get
        //    {
        //        var rends = MyGameObject.GetComponentsInChildren<Renderer>();
        //        var bounds = rends[0].bounds;
        //        bounds = rends.Aggregate(bounds, (current, rend) => current.GrowBounds(current));
        //        _center = bounds.center;
        //        return _center;
        //    }
        //}

        //public Bounds Bound
        //{
        //    get
        //    {
        //        var rends = MyGameObject.GetComponentsInChildren<Renderer>();
        //        var bounds = rends[0].bounds;
        //        bounds = rends.Aggregate(bounds, (current, rend) => current.GrowBounds(current));
        //        _bound = bounds;
        //        return _bound;
        //    }
        //}

        public bool IsVisible
        {
            get { return _isVisible; }
            set
            {
                _isVisible = value;
                var tempRenderer = MyGameObject.GetComponent<Renderer>();
                if (tempRenderer)
                    tempRenderer.enabled = _isVisible;
                if (MyTransform.childCount <= 0) return;
                foreach (Transform child in MyTransform)
                {
                    tempRenderer = child.gameObject.GetComponent<Renderer>();
                    if (tempRenderer)
                        tempRenderer.enabled = _isVisible;
                }
            }
        }
        #endregion;



        #region Private functions;
        /// <summary>
        /// Рекурсивный метод для смены слоя объекту и всем дочерним объектам
        /// </summary>
        /// <param name="obj">Объект, слой которого нужно изменить</param>
        /// <param name="layer">Слой</param>
        private void AskLayer(Transform obj, int layer)
        {
            obj.gameObject.layer = layer;
            if(obj.childCount <= 0)
            {
                return;
            }
            foreach (Transform o in obj)
            {
                AskLayer(o, layer);
            }
        }

        /// <summary>
        /// Рекурсивный метод для смены цвета материала у объекта и всех его дочерних объектов 
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="color"></param>
        private void AskColor(Transform obj, Color color)
        {
            foreach (var curMaterial in obj.GetComponent<Renderer>().materials)
            {
                curMaterial.color = color;
            }
            if(obj.childCount <= 0)
            {
                return;
            }
            foreach (Transform child in obj)
            {
                AskColor(child, color);
            }
        }
        #endregion;


        #region Unity functions;
        protected virtual void Awake()
        {
            MyTransform = transform;
            MyGameObject = gameObject;
            MyRigidBody = GetComponent<Rigidbody>();
        }
        #endregion;
    }
}