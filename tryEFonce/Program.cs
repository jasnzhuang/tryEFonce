using System;
using System.Linq;
using tryEFonce.Models;

namespace tryEFonce
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            Console.ForegroundColor = ConsoleColor.White;
            var result = -1;
            while (result != 9) result = GetMainMenu();
        }

        /// <summary>
        ///     主菜单
        /// </summary>
        /// <returns></returns>
        private static int GetMainMenu()
        {
            var result = -1;
            ConsoleKeyInfo cki;
            var cont = false;
            do
            {
                Console.Clear();
                WriteHeader("欢迎来到EF&LINQ教学系统");
                WriteHeader("主菜单");
                Console.WriteLine("\r\n嗨！SB们！今天来玩点什么呢？");
                Console.WriteLine("1. 数据录入菜单");
                Console.WriteLine("2. 数据列表菜单");
                Console.WriteLine("3. 数据维护页面");
                Console.WriteLine("4. 没想好");
                Console.WriteLine("5. 也是没想好");
                Console.WriteLine("6. 还是没想好");
                Console.WriteLine("9. 退出！跑路！");
                cki = Console.ReadKey();
                try
                {
                    result = Convert.ToInt16(cki.KeyChar.ToString());
                    if (result == 1)
                        DataEntryMenu();
                    else if (result == 2)
                        DataListMenu();
                    else if (result == 3)
                        ListAllEvents();
                    else if (result == 4)
                        return 0;
                    else if (result == 5)
                        AddSeed();
                    else if (result == 6)
                        ListAllEventsAt();
                    else if (result == 9) cont = true;
                }
                catch (FormatException)
                {
                    // a key that wasn't a number
                }
            } while (!cont);

            return result;
        }

        /// <summary>
        /// 数据录入菜单
        /// </summary>
        static void DataEntryMenu()
        {
            ConsoleKeyInfo cki;
            int result = -1;
            bool cont = false;
            do
            {
                Console.Clear();
                WriteHeader("数据录入菜单");
                Console.WriteLine("\r\n关于数据录入今天咱来玩点啥？");
                Console.WriteLine("1. 添加组织");
                Console.WriteLine("2. 添加组织活动");
                Console.WriteLine("3. 添加组织联系人");
                Console.WriteLine("4. 没想好");
                Console.WriteLine("9. 返回主菜单");
                cki = Console.ReadKey();
                try
                {
                    result = Convert.ToInt16(cki.KeyChar.ToString());
                    if (result == 1)
                    {
                        AddOrganizer();
                    }
                    else if (result == 2)
                    {
                        AddEventWithOrganizer();
                    }
                    else if (result == 3)
                    {
                        AddContactWithOrganizer();
                    }
                    else if (result == 4)
                    {
                    }
                    else if (result == 9)
                    {
                        // We are exiting so nothing to do
                        cont = true;
                    }
                }
                catch (System.FormatException)
                {
                    // a key that wasn't a number
                }
            } while (!cont);
        }

        /// <summary>
        /// 数据列表菜单
        /// </summary>
        static void DataListMenu()
        {
            ConsoleKeyInfo cki;
            int result = -1;
            bool cont = false;
            do
            {
                Console.Clear();
                WriteHeader("数据列表菜单");
                Console.WriteLine("\r\n关于数据列表，这位爷今天你想看点啥？");
                Console.WriteLine("1. 组织列表");
                Console.WriteLine("2. 活动列表");
                Console.WriteLine("3. 组织活动列表");
                Console.WriteLine("4. 联系人列表");
                Console.WriteLine("5. 组织联系人列表");
                Console.WriteLine("9. 返回主菜单");
                cki = Console.ReadKey();
                try
                {
                    result = Convert.ToInt16(cki.KeyChar.ToString());
                    if (result == 1)
                    {
                        ListAllOrganizers();
                    }
                    else if (result == 2)
                    {
                        ListAllEvents();
                    }
                    else if (result == 3)
                    {
                        ListAllEventsAt();
                    }
                    else if (result == 4)
                    {
                        ListAllContacts();
                    }
                    else if (result == 5)
                    {
                        ListAllContactAt();
                    }
                    else if (result == 9)
                    {
                        // We are exiting so nothing to do
                        cont = true;
                    }
                }
                catch (System.FormatException)
                {
                    // a key that wasn't a number
                }
            } while (!cont);
        }

        /// <summary>
        /// 列出指定组织的全部活动
        /// </summary>
        private static void ListAllEventsAt()
        {
            Console.Clear();
            Console.WriteLine("组织列表");
            var newO = new Organizer();
            var newE = new Event();
            using (var context = new TryContext())
            {
                var olist = from oname in context.Organizers select oname;
                foreach (var o in olist) Console.WriteLine("ID:" + o.OrganizerId + " Name:" + o.Name);
            }

            Console.WriteLine("请参照上方列表输入要创建活动的组织ID后按Enter");
            var oId = Convert.ToInt16(Console.ReadLine()?.Trim());
            var exists = CheckForExistingOrganizerId(oId);
            if (exists)
            {
                Console.Clear();
                Console.WriteLine("活动列表");
                using (var context = new TryContext())
                {
                    var elist = from o in context.Organizers
                        from e in o.Events
                        where o.OrganizerId == oId
                        select e;
                    foreach (var e in elist) Console.WriteLine("ID:" + e.EventId + " Name:" + e.Name);
                }

                Console.WriteLine("\r\n按任意键继续...");
                Console.ReadKey();
            }
            else
            {
                Console.WriteLine("\r\n你输入的活动所属组织不存在，请重新输入\r\n按任意键继续...");
                Console.ReadKey();
            }
        }

        /// <summary>
        /// 添加种子数据
        /// </summary>
        private static void AddSeed()
        {
            Console.Clear();
            Console.WriteLine("准备添加种子数据...");
            using (var context = new TryContext())
            {
                var org = new Organizer {Name = "胖才"};
                context.Organizers.Add(org);
                var evt = new Event {Name = "扒蒜大赛"};
                org.Events.Add(evt);
                var con = new Contact {Name = "江崟才"};
                context.SaveChanges();
            }

            Console.WriteLine("\r\n添加种子数据完成！");
            Console.WriteLine("\r\n按任意键继续...");
            Console.ReadKey();
        }

        /// <summary>
        /// 添加组织
        /// </summary>
        public static void AddOrganizer()
        {
            Console.Clear();
            ConsoleKeyInfo cki;
            string result;
            var cont = false;
            var newO = new Organizer();
            var oName = "";

            WriteHeader("添加新组织");
            Console.WriteLine("输入组织名称后按Enter");
            oName = Console.ReadLine();
            newO.Name = oName;

            var exists = CheckForExistingOrganizer(newO.Name);
            if (exists)
            {
                Console.WriteLine("\r\n你输入的组织已经存在，请重新输入\r\n按任意键继续...");
                Console.ReadKey();
            }
            else
            {
                using (var context = new TryContext())
                {
                    Console.WriteLine("\r\n正在尝试保存...");
                    context.Organizers.Add(newO);
                    var i = context.SaveChanges();
                    if (i == 1)
                    {
                        Console.WriteLine("添加组织成功\r\n按任意键继续...");
                        Console.ReadKey();
                    }
                }
            }
        }

        /// <summary>
        /// 添加指定组织的活动
        /// </summary>
        public static void AddEventWithOrganizer()
        {
            Console.Clear();
            Console.WriteLine("组织列表");
            var newO = new Organizer();
            var newE = new Event();
            using (var context = new TryContext())
            {
                var olist = from oname in context.Organizers select oname;
                foreach (var o in olist) Console.WriteLine("ID:" + o.OrganizerId + " Name:" + o.Name);
            }

            Console.WriteLine("请参照上方列表输入要创建活动的组织ID后按Enter");
            var oId = Convert.ToInt16(Console.ReadLine()?.Trim());
            var exists = CheckForExistingOrganizerId(oId);
            if (exists)
            {
                Console.WriteLine("输入活动名称后按Enter");
                var eName = Console.ReadLine();
                newE.Name = eName;
                newE.OrganizerId = oId;
                using (var context = new TryContext())
                {
                    Console.WriteLine("\r\n正在尝试保存...");
                    context.Events.Add(newE);
                    var i = context.SaveChanges();
                    if (i == 1)
                    {
                        Console.WriteLine("添加活动成功\r\n按任意键继续...");
                        Console.ReadKey();
                    }
                }
            }
            else
            {
                Console.WriteLine("\r\n你输入的活动所属组织不存在，请重新输入\r\n按任意键继续...");
                Console.ReadKey();
            }
        }

        /// <summary>
        /// 添加指定组织的联系人
        /// </summary>
        public static void AddContactWithOrganizer()
        {
            Console.Clear();
            Console.WriteLine("组织列表");
            var newO = new Organizer();
            var newC = new Contact();
            using (var context = new TryContext())
            {
                var olist = from oname in context.Organizers select oname;
                foreach (var o in olist) Console.WriteLine("ID:" + o.OrganizerId + " Name:" + o.Name);
            }

            Console.WriteLine("请参照上方列表输入要创建联系人的组织ID后按Enter");
            var oId = Convert.ToInt16(Console.ReadLine()?.Trim());
            var exists = CheckForExistingOrganizerId(oId);
            if (exists)
            {
                Console.WriteLine("输入联系人姓名后按Enter");
                var cName = Console.ReadLine();
                Console.WriteLine("输入联系人电话后按Enter");
                var cPhone = Console.ReadLine();
                newC.Name = cName;
                newC.Phone = cPhone;
                newC.OrganizerId = oId;
                using (var context = new TryContext())
                {
                    Console.WriteLine("\r\n正在尝试保存...");
                    context.Contacts.Add(newC);
                    var i = context.SaveChanges();
                    if (i == 1)
                    {
                        Console.WriteLine("添加联系人成功\r\n按任意键继续...");
                        Console.ReadKey();
                    }
                }
            }
            else
            {
                Console.WriteLine("\r\n你输入的组织不存在，请重新输入\r\n按任意键继续...");
                Console.ReadKey();
            }
        }

        /// <summary>
        /// 列出全部组织
        /// </summary>
        public static void ListAllOrganizers()
        {
            Console.Clear();
            Console.WriteLine("组织列表");
            using (var context = new TryContext())
            {
                var olist = from oname in context.Organizers select oname;
                foreach (var o in olist) Console.WriteLine("ID:" + o.OrganizerId + " Name:" + o.Name);
            }

            Console.WriteLine("\r\n按任意键继续...");
            Console.ReadKey();
        }

        /// <summary>
        /// 列出全部事件
        /// </summary>
        public static void ListAllEvents()
        {
            Console.Clear();
            Console.WriteLine("活动列表");
            using (var context = new TryContext())
            {
                var elist = from ename in context.Events
                    join o in context.Organizers on ename.OrganizerId equals o.OrganizerId
                    select new
                    {
                        ename.EventId,
                        EventName = ename.Name,
                        OrganizerName = o.Name
                    };
                foreach (var e in elist)
                    Console.WriteLine("ID:" + e.EventId + " Name:" + e.EventName + " 组织方:" + e.OrganizerName);
            }

            Console.WriteLine("\r\n按任意键继续...");
            Console.ReadKey();
        }

        /// <summary>
        /// 列出全部联系人
        /// </summary>
        public static void ListAllContacts()
        {
            Console.Clear();
            Console.WriteLine("联系人列表");
            using (var context = new TryContext())
            {
                //var clist = from c in context.Contacts
                //            join o in context.Organizers on c.OrganizerId equals o.OrganizerId
                //            select new
                //            {
                //                c.ContactId,
                //                c.Name,
                //                c.Phone,
                //                OrganizerName = o.Name
                //            };
                //foreach (var c in clist)
                //    Console.WriteLine("ID:" + c.ContactId + " 组织方:" + c.OrganizerName + " 姓名:" + c.Name + " 电话:" + c.Phone);

                var oList = from organizer in context.Organizers orderby organizer.Name select organizer;
                foreach (var o in oList)
                {
                    Console.WriteLine(" 组织方:" + o.Name);
                    using (var cContext = new TryContext())
                    {
                        var cList = from c in cContext.Contacts where c.OrganizerId == o.OrganizerId select c;
                        foreach (var c in cList)
                        {
                            Console.WriteLine("ID:" + c.ContactId + " 姓名:" + c.Name + " 电话:" + c.Phone);
                        }
                    }
                }
            }

            Console.WriteLine("\r\n按任意键继续...");
            Console.ReadKey();
        }

        /// <summary>
        /// 列出指定组织的全部联系人
        /// </summary>
        private static void ListAllContactAt()
        {
            Console.Clear();
            Console.WriteLine("组织联系人列表");
            var newO = new Organizer();
            var newE = new Event();
            using (var context = new TryContext())
            {
                var olist = from oname in context.Organizers select oname;
                foreach (var o in olist) Console.WriteLine("ID:" + o.OrganizerId + " Name:" + o.Name);
            }

            Console.WriteLine("请参照上方列表输入要查询联系人的组织ID后按Enter");
            var oId = Convert.ToInt16(Console.ReadLine()?.Trim());
            var exists = CheckForExistingOrganizerId(oId);
            if (exists)
            {
                Console.Clear();
                Console.WriteLine("活动列表");
                using (var context = new TryContext())
                {
                    var clist = from c in context.Contacts
                        join o in context.Organizers on c.OrganizerId equals o.OrganizerId
                        where o.OrganizerId == oId
                        select new
                        {
                            c.ContactId,
                            c.Name,
                            c.Phone,
                            OrganizerName = o.Name
                        };
                    foreach (var c in clist)
                        Console.WriteLine("ID:" + c.ContactId + " 组织方:" + c.OrganizerName + " 姓名:" + c.Name + " 电话:" +
                                          c.Phone);
                }

                Console.WriteLine("\r\n按任意键继续...");
                Console.ReadKey();
            }
            else
            {
                Console.WriteLine("\r\n你输入的活动所属组织不存在，请重新输入\r\n按任意键继续...");
                Console.ReadKey();
            }
        }

        /// <summary>
        /// 检查输入的ID是否存在对应的组织
        /// </summary>
        /// <param id="id"></param>
        /// <returns></returns>
        private static bool CheckForExistingOrganizerId(int Id)
        {
            var exists = false;
            using (var context = new TryContext())
            {
                var oId = context.Organizers.Where(o => o.OrganizerId == Id);
                if (oId.Any())
                    //if (oName.Count() > 0) 这个方法也可以，但是要注意捕捉异常
                    exists = true;
            }

            return exists;
        }

        /// <summary>
        /// 检查输入的组织名是否存在对应的组织
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        private static bool CheckForExistingOrganizer(string name)
        {
            var exists = false;
            using (var context = new TryContext())
            {
                var oName = context.Organizers.Where(o => o.Name == name);
                if (oName.Any())
                    //if (oName.Count() > 0) 这个方法也可以，但是要注意捕捉异常
                    exists = true;
            }

            return exists;
        }

        /// <summary>
        /// 菜单的标题样式
        /// </summary>
        /// <param name="headerText"></param>
        private static void WriteHeader(string headerText)
        {
            Console.WriteLine("{0," + (Console.WindowWidth / 2 + headerText.Length / 2) + "}", headerText);
        }
    }

    //public static class TryProgram
    //{
    //    public static void Run()
    //    {
    //        using (var context = new TryContext())
    //        {
    //            var evsorg1 = from ev in context.Events
    //                from organizer in ev.Organizers
    //                select new {ev.EventId, organizer.OrganizerId};
    //            Console.WriteLine("Using nested from clauses...");
    //            foreach (var pair in evsorg1)
    //            {
    //                Console.WriteLine("EventId {0}, OrganizerId {1}",
    //                    pair.EventId,
    //                    pair.OrganizerId);
    //            }

    //            var evsorg2 = context.Events
    //                .SelectMany(e => e.Organizers,
    //                    (ev, org) => new {ev.EventId, org.OrganizerId});
    //            Console.WriteLine("\nUsing SelectMany()");
    //            foreach (var pair in evsorg2)
    //            {
    //                Console.WriteLine("EventId {0}, OrganizerId {1}",
    //                    pair.EventId, pair.OrganizerId);
    //            }
    //        }
    //    }
    //}
}