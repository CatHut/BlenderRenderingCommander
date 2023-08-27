using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace CatHut
{
    // Queue<T>を継承したシリアライズ可能なクラス
    [Serializable]
    public class SerializableQueue<T> : Queue<T>
    {
        // パラメーターのないコンストラクター
        public SerializableQueue() : base()
        {
        }

        // コレクションを引数にとるコンストラクター
        public SerializableQueue(IEnumerable<T> collection) : base(collection)
        {
        }

        // キャパシティを引数にとるコンストラクター
        public SerializableQueue(int capacity) : base(capacity)
        {
        }

        // インデクサー（Itemプロパティ）の実装
        [XmlIgnore]
        public T this[int index]
        {
            get
            {
                // キューを配列に変換してインデックスでアクセス
                return this.ToArray()[index];
            }
            set
            {
                // キューをリストに変換してインデックスで更新
                List<T> list = new List<T>(this);
                list[index] = value;
                // リストをキューに戻す
                this.Clear();
                foreach (T item in list)
                {
                    this.Enqueue(item);
                }
            }
        }

        // Addメソッドの実装
        public void Add(T item)
        {
            // キューにオブジェクトを追加
            this.Enqueue(item);
        }

        // Clearメソッドの実装
        public void Clear()
        {
            // キューからすべての要素を削除
            while (this.Count > 0)
            {
                this.Dequeue();
            }
        }

        // Removeメソッドの実装
        public bool Remove(T item)
        {
            // キューから特定の要素を削除
            // キューをリストに変換してリストから要素を削除
            List<T> list = new List<T>(this);
            bool result = list.Remove(item);
            // リストをキューに戻す
            this.Clear();
            foreach (T element in list)
            {
                this.Enqueue(element);
            }
            return result;
        }
    }
}
