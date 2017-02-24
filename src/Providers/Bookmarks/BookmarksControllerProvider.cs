﻿using System.IO;
using System.Reflection;

namespace DialToolsForVS
{
    [DialControllerProvider(order: 7)]
    internal class BookmarksControllerProvider : IDialControllerProvider
    {
        public const string Moniker = "Bookmarks";

        public IDialController TryCreateController(IDialControllerHost host)
        {
            string folder = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            string iconFilePath = Path.Combine(folder, "Providers\\Bookmarks\\icon.png");

            host.AddMenuItem(Moniker, iconFilePath);

            return new BookmarksController(host);
        }
    }
}
