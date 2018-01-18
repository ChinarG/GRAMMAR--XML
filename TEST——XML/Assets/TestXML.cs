using UnityEngine;
using System.Xml; //引用XML命名空间


/// <summary>
/// 测试读取XML脚本
/// </summary>
public class TestXML : MonoBehaviour
{
	/// <summary>
	/// 初始化函数
	/// </summary>
	void Start()
	{
		//FistMethod(); //调用第一种方法
		SecondMethod(); //调用第二种XPath绝对路径 读取方法
		//thirdMethod(); //调用第三种XPath相对路径 读取方法
	}


	/// <summary>
	/// 第一种读取方法
	/// </summary>
	void FistMethod()
	{
		XmlDocument doc = new XmlDocument();                    //实例化一个XmlDocument类对象 :创建一个XML文档
		doc.Load(Application.dataPath + "/Data/XML.xml");       //读取XML文档
		XmlElement rootEle = (XmlElement) doc.LastChild;        //获得根节点 ：由于根节点就是最后一个节点，所以用Lastchild
		foreach (XmlElement childNodeEle in rootEle.ChildNodes) //遍历根节点中的子节点：rootEle.ChildNodes —— 返回的是所有子节点
		{
			print(childNodeEle.GetAttribute("ID"));                       //打印子节点的属性
			XmlElement nameEle = (XmlElement) childNodeEle.ChildNodes[0]; //打印子节点<People>中的：子节点的第一个节点 <Name>
			XmlElement ageEle  = (XmlElement) childNodeEle.ChildNodes[1]; //打印子节点<People>中的：子节点的第二个节点 <Age>
			print(nameEle.InnerText + " " + ageEle.InnerText);            //打印Name 和 Age
		}

		//rootEle.GetElementsByTagName("Name")  通过名字标签来获取元素
		XmlNodeList list = rootEle.GetElementsByTagName("Name"); //找到节点<Root>中，名叫<Name>的子节点. (系统会自动找<People>下的所有<Name>节点)
		foreach (XmlElement ele in list)                         //遍历集合
		{
			print(ele.InnerText); //打印元素文本Name
		}
	}


	/// <summary>
	/// 第二种XPath绝对路径 读取方法
	/// </summary>
	void SecondMethod()
	{
		XmlDocument doc = new XmlDocument();                    //实例化一个XmlDocument类对象 :创建一个XML文档
		doc.Load(Application.dataPath + " " + "/Data/XML.xml"); //读取XML文档

		//XPath表达式来解析 ：一个路径语法
		/*
		 * "/Root/People/Name" ———————————————— 绝对路径
		 * "/Root/People[1]/Name" ————————————— 取得第二个节点<People>中的第一节点<Name>   （<Name>也叫元素）
		 * "/Root/People[last()]/Name" ———————— 取得第二个节点<People>中的最后节点<Name>
		 * "/Root/People[last()-1]/Name" —————— 取得第二个节点<People>中的倒数第二个节点<Name>
		 * "/Root/People[position()<4]/Name" —— 取得第二个节点<People>中的前三个节点<Name>
		 * "/Root/People[@ID]/Name" ——————————— 取得第二个节点<People>中有ID属性的节点<Name>，（没有ID属性的会被剔除）
		 * "/Root/People[@ID=3]/Name" ————————— 取得第二个节点<People>中有ID属性等于3的节点<Name>，（ID属性不等于3的会被剔除）
		 */

		//doc.SelectSingleNode(""); 查找单个节点
		//doc.SelectNodes(""); 查找多个节点
		//doc.SelectNodes("")返回值 ：一个 XmlnodeList 集合
		XmlNodeList list = doc.SelectNodes("/Root/People[@ID<3]/Name"); //“/根节点Root/第一节点People/第二节点Name”
		foreach (XmlElement ele in list)                                //遍历集合中的元素
		{
			print(ele.InnerText); //打印元素文本
		}
	}


	/// <summary>
	/// 第三种XPath相对路径 读取方法
	/// </summary>
	void thirdMethod()
	{
		XmlDocument doc = new XmlDocument();                    //实例化一个XmlDocument类对象 :创建一个XML文档
		doc.Load(Application.dataPath + " " + "/Data/XML.xml"); //读取XML文档
		//XPath表达式来解析 ：一个路径语法
		//doc.SelectSingleNode(""); 查找单个节点
		//doc.SelectNodes(""); 查找多个节点
		//doc.SelectNodes("")返回值 ：一个 XmlnodeList 集合
		XmlNodeList list = doc.SelectNodes("//Age"); //直接找“//第二节点Age” —— 性能上没有绝对路径好
		foreach (XmlElement ele in list)             //遍历集合中的元素
		{
			print(ele.InnerText); //打印元素文本
		}
	}
}