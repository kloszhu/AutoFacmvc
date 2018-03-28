using Com.Models.EasyUI;
using Models;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace LayuiPortalApi.Controllers
{
    public class TreeMenuController : ApiController
    {
        public List<EasyUITree> Get() {
            List<EasyUITree> trees = new List<EasyUITree>();
            for (int i = 0; i < 100; i++)
            {
                if (i % 3 == 0)
                {
                    EasyUITree item1 = new EasyUITree();
                    item1.id = i;
                    item1.text = "根节点" + i.ToString();
                    item1.pid = null;
                    trees.Add(item1);
                    continue;
                }

                else if (i%3==1)
                {
                    EasyUITree item1 = new EasyUITree();
                    item1.id = i;
                    item1.text = "一级节点" + i.ToString();
                    item1.pid = 0;
                    trees.Add(item1);
                    continue;
                }
                else { 
                EasyUITree item = new EasyUITree();
                item.id = i;
                item.text = "金城武" + i.ToString();
                Random random = new Random();
                item.pid= random.Next(1, 3);
                trees.Add(item);
                }
            }
            TreeList(trees, trees.Where(a => a.pid == null).FirstOrDefault());
            return trees;
        }

        public EasyUITree Get(int g)
        {
            List<EasyUITree> trees = new List<EasyUITree>();
            for (int i = 0; i < 100; i++)
            {
                if (i % 3 == 0)
                {
                    EasyUITree item1 = new EasyUITree();
                    item1.id = i;
                    item1.text = "根节点" + i.ToString();
                    item1.pid = null;
                    trees.Add(item1);
                    continue;
                }

                else if (i % 3 == 1)
                {
                    EasyUITree item1 = new EasyUITree();
                    item1.id = i;
                    item1.text = "一级节点" + i.ToString();
                    item1.pid = 0;
                    trees.Add(item1);
                    continue;
                }
                else
                {
                    EasyUITree item = new EasyUITree();
                    item.id = i;
                    item.text = "金城武" + i.ToString();
                    Random random = new Random();
                    item.pid = random.Next(1, 3);
                    trees.Add(item);
                }
            }
           TreeList(trees, trees.Where(a => a.pid == null).FirstOrDefault());
            return trees.Where(a => a.pid == null).FirstOrDefault();
        }

        [Route("api/TreeMenu/Tree")]
        public JArray GetMenus() {
            string result = "";
         string treepath=   AppDomain.CurrentDomain.BaseDirectory + "JSON/" + "tree1.json";
            if (File.Exists(treepath))
            {
                StreamReader streamReader = new StreamReader(treepath);
              result=  streamReader.ReadToEnd();
                streamReader.Close();
            }
            return JArray.Parse( result);
        }

        private void TreeList(List<EasyUITree> all, EasyUITree curItem)
        {
            var subItems = all.Where(ee => ee.pid == curItem.id).ToList();
            curItem.children = new List<EasyUITree>();
            curItem.children.(subItems);
          
                foreach (var subItem in subItems)
                {

                    TreeList(all, subItem);//递归

                }
            
           
            

        }
    


     

    }
}
