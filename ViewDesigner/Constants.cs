namespace Cinteros.XTB.ViewDesigner.Utils
{
    /// <summary>
    /// Structure holding constants
    /// </summary>
    internal struct Constants
    {
        #region Internal Fields

        /// <summary>
        /// Encoded large icon for plugins
        /// </summary>
        internal const string B64_IMAGE_LARGE = "iVBORw0KGgoAAAANSUhEUgAAAFAAAABQCAIAAAABc2X6AAAACXBIWXMAAAsTAAALEwEAmpwYAAAPJklEQVR4nO1aaXRcxZW+VfVe9+tdW7clYcmWjSV5EwbjBWMw49hm8Zh4GTLknAnxQEgyMJxMMkkgOSwGQkh8PJlwSII9wAECBGIM8Qz7YhsMHu823lfJlm3JUku21Or1vVdVd35US5bk7pZjCGCNPr3TR12vblV9dW9V3XurCSLC/yfQL3sAXzQGCPd3DBDu7xgg3N8xQLi/Y4Bwf8cA4f6OAcL9HQOE+zu0HO8QQUgJQACQUUrImVdCIiJC96K0DBAClJCz3yhIxHSTZwkCAUqAZpPsGhIiAFACqg9ElAgAwLJ32h3kPFI8iNhn20IiPWvsEjE3n7RglqELiYzmEu+zAmQjLBEogaMtsadWH2I6E7a4bfqIiqBXIqqZXXewZfXuk81RM25xiaiswNBZyGdcUpZ39chBIb8BabWloQxie33bezsaTrQn45boMh+HxoJe5+jSwFUjQ+WFnq7KPYeElJDTceut7SfW7g/XtsQiSRsA/C59eNA7tTI4+7LBRV5nn3Oa2aSVuR4ORx95fjO4nRA3p40urgh6hUSdkbuXb1+8fAcgAiW9jRMBAArzXd+bUbVoQY3OqBq6lEgpeey9/T98eiNyAZRmEET0+o1brhr2yM2X5bn07pyV6p768PADyz9tDEeBEGCdLSCskfKpd/YVF3nvv2ncv3xtRG4959q0HBrVAi7D79QCLodGAUBndNfx9sWv7aJuXQ8YoFFA6PEwyjyOU0n+yxe2fP0/P7S4RECJSClpiqTufnk7MqoHXKCz3oKUEI8zJuQfVu6e8cj7bXELIW18isCv3tx7+2NrGyNJ5jOAEhASBIJAEBIIYT5nU8y84/G1D67cxSgRMus67WPT4kKCQC5kl+Fvqz9NuGAu3U7xWZcOvmJEUdeKi6X4rhNt7+1oBAnOkPftj+t+fXHwvrljTS6dGtnXGDETtubS7RS/ojo0Y0yJ1qmHpC32N0Te2dmYSgkj6Nm66+SPX9r29HcmC4mAwCjZVHfqZ89v0fyGlChsMW9KxdXVoeKACwBaoqlPDrSs2HiUENDyXIv+tG36yEFXVYWy6TkX4YzgAhFASOk0tBfvuLLI6+xV4aP94fm/WRNJ2tRnPP7BwX+7rtrr1AHA4pJIpU1Y+s+TasryegnuPN5+45I1x1tiWp7x/Lq6++eNHVLo4UICkF++vhu4BAN0Aq/86Jo5lw7uLnjXzKr3rxk+d8kaCwAQH/mf3e/8ZHo2mz7PcxgBKCOmLblAm0sukAvkEi0up1WHfj6vRiRs5mAtrfEtR053LUUEQASgRCBygZbdKSjQ4rKmLG/JP42XtqCM2lHzo/1hANAYDUdTq/c0U4/OY+Z3Z1XNuXSwxSWX2PVYXM4cU3LXDaN41KRux9r94Ya2BKVEZtqPP4PjgUAIaIyceShhlEiJ19eUEreOEgkXR1piZ4sS6CnIiM6oRLymepCvwG1zCQi1zVFVeUd9WzSSpJSAxv5x0hCJyCjRuj2MEon4jYnlYGiUQjKa2nLkNKjJ/TwJZwIlQCkp9Do8Ll0gIkI0yc9JkgAlJODWi7xO5UnETFu9OdQcBS6FQI/fOTzkpYT0OnhUydCgtyBgcI4g8FBTFL4Ywurc1RhVBxIAZLSrbGCU6IwCIpAzw22KpAAAJea7HX6XI0uf4HdpBR4nSAQCJ9sT2bq4AHzpuMmBACAaOjN0CtDbM1XfNEodGgVEAJKw5dnVFC4Awp8vLgDCHqcGCEBIyhYppbqeq0R940KatgBCANCts7OrKVwAhIsDBgAQRtoSVkfSylADAQCiKd6WsIARQCjNd2Vr7QIgPGKQHzTKKIl3mHXhmJTYayOUiBLxSEusLZJilAAjIwb5ADIEr/DVJ4wA44bk+fJcUiBw8dKGekqJkGk/Rz0qoly++RimOCIYPuOyoQVwgRK2uAz6jBmjimXC0n3Gk+8f+O9tJxwaVX6OehwaXbWn6Xdv7tO8TpmwplWHBhe4pcwcJ/7VvvQXDkSAn80Z/Zf1R1GiDTD3Pz6cP7H8qurQRfluAGiKJNcdaFmxsV4CaowCIfd+fSxk3rAAchMmBDRGNUaAnZksSojGKGFEYznjbEqUYJekao0xQjBXhK5RojHaJcgIRYkThhUuvmXCT59cDx6H5mCv/e+R1z6p64qHgRLm1tFGuz354MKJUyuDArOGxLkIW1zySJLbEuKmxaUqTFqcR5LAhe3QsnlRiNgSNSFmQtxKWLxHa9wBtuDZ49XWmMkjSTB5l2upFu1PbhhZ4HE88OftDWclAEBIETWLg94Hbp30/ekjBCLLPqWZCavZqSz2P3zbZKYxwUVlsV+9mlIZfOj2K6jONEYDaUevd+sep77km5elbCFtMX1UsSocVRp46DuTmc6kxMEFbui5qZDOz0Xza051pFDIydUhAKAUAECFB7dNGz7/8rI3tp/4+EC4LhyLpDgCBgy9osg7tTI4Z/zgQq9T5mQL55fE+7LwN0ziKWRM00pEKbEzSMjaOpcICABIaTq46dYasOxa4BJV3NAl2HtIXWlaIACA8HmnaSXimS2PqDD4TNOIIBFJ9nwyIqg/JU4J6dFgT9BO/SAAIqqKBDIkulW/Z2gQODe+fRHOmGw/87bb8j07P6qI0b5s7GyoFGevwu7mmm1UGQV7oW8NH26ORlO2IqNRiogFPmdpnksxTFhiW23rRUFvRZEnI/9TMTMcSZlCgkSXUxs+yLe/McKFinhRIpAuxSJUlfgNnSpV7TrefuxU3BayyOsceVGgsGfyLGmLQyc70usNwanR4cU+R2dW+HwIq5z7nX/c9OS7BzgXIBAAQCPQYX57Xs2z371CSIybfPqjH2zdfdLjM1798d/NGlsiJTJKVB5/38mO+/68fdXepvb2FCBC0h5WFdq46Lrif31VdCRBo0ApOBhwCbYAABC4fsmNk4cXrdnXfPcLWzbXtkLMAkRwsGDI971ZVQ/Or0EESsnBkx03LllzsCECXKRXh86qSwO/WTjh+prS3Ln4zMeSkqkLx5547wBQMn7koDyXQyJqjNhxe9yQfERglLy6+djWTxtKKgpONnY8uHLXtWNLCEnPVF04Nu3hd1uOtTtD3suqQx6HJixeWZbn0OissSWRuMk0Wt+aON7UUVTgrirxA4IUGPIbB5uiN/x6VaotUT6saMKwQpeDHWmJrdvb9ItnNnGJj940DgCeWVt7cH+4aGj+mMF5BIAQOHE6sf9w6z88tnbf4jllhZ5cnDEThJSIuOPYafLNPxbd8UosZWesMGvxKpj79Ls7G8f+/E1683OHm6OIaHGBiLc+uR5uWHblQ+/WNkczdoGIi9/YA9cuvWXZuu6F31q6Dm5Ydt3iVe1xq6vw2Y9rtZufcy78U104ioh3PLuJXrf07pe3dVVImHziA2/D7P9atvogItpcZus0V/BACUHlo/WcLBWdHD+VWLPzZGl5/qyxJTPHFOPpxMqtxwGAUZK0xPu7TxJDf2jBJcNC3m69p6fY5lIi2hJVaxLR4lIiJi3x0f4wGPo9c8YE3LpKx0rEb08ddkllyGyNrz0QBgAEkBJ1jQqJpi0tLl0ONm5IAeWyNWrmYATnEi1JCfEUT1oiYYqkJVJ2+h7j9e0n7HB03vjBQuJ1Y0vQpb+8sV7ZUmN7oqEt4cx3VZf6JaK62uw6zNR/tNspok4dSkhzR6qxLeEIGMOCHkRQIZE6mMcODhBbHFa5W0Q1MEaJU6cOjZ6KmZtqW6VOlQ+XMZulkMuXlgig0VMxc9Q9b6gBSYuXDfKtv/9aYOzFDUeJx3HbtOGMkpljSiqrQlv2Nu883j6uPL8jaUtbejwOt84oIefiy6k6CZNzWwa8DsOhEQKA6d2bUPC7dARUN4Zqa/a5tJc31P/2Lzupx7GvsaP9RHvJsMLrLylFhBzeZd8aRkjfcXcmhIlDZ4eaohv2NpeU5dsCtx09vfN42/gh+SRmrth0DNIjUnddf53fSikAAdl1jwbpxhBA2dUZzxHBobGjrbGN206sPxAucOkLvjbivZ9OD/oMzBL6K+TSMCUAXBYVuPb8arbf0NU1sJBIAF7ZVC9N3tjcMelHK9O1dQZe5ytbjj20oKYkYBguPRq3Ikk74HZg555JIKsfooaY53a6XXo8ZXck7UKvEwApUEQkQMIdJiEk6DMUW6AkkrDumTP6mY9qD9W23j6j8p7ZowDSd7i5SOV62TkUj1Nz6sxwMENnHqcmEZdvPg4aXXDlsFv/fvTC2aMWzh61cFZVUZHnYN2pDYdbi/NclcU+bE++sb1Bo0RnlFHCKMnhBhFCECDod14c8kF7ct2hFkaJcuB1Ri0uN9W2oEtXV3Bq+VtcunT24p1TXV7nfc9vfnNHI5xD3j9nAgBADZRLREx7xZSSPQ2R3YdaLi7PX3HXVd3r//DFrY+/sGXF5mNTRgRvm3bxD3Y23rv802jKnloZNHSGCA6NXlKe32WWlBDWGSEQACFQY+SmieU7P214YMWOAo9z0vBCjdHmSPLR1/fUH20rH1pwdVUIADRG1B0FAlw+tGDp96fcsnj1rU98suEXsyuKch7CuQkLiSJutbt06DQ5KYEAPP3RYdEYmTd3jES0OGqMKEd33viy37607dm1dffPrblzRuW2+tPPvb3v3ifWgUMDRsHmzqC35fc3+VyaCgqSthAxM26mMwSMEkT4wbVVb+1oWL+x/saH3zXy3Q6NdkRN6EhqAdfvb5ngd+kAkLSEiJlJixOAlC2+NaVi6zfGPfbMppmPfvDJ/deG/EYOB5MtWrQos3oJSdriQDg+bmjB/PFlOqOgViDCW5825Be4//3GMcUBFyVpK2CEFOe5jsYtv86mVIWKA8bc8WVTRhUXBj3BIm95qb/iorxLKwrnX16mNKMOoYjJr64pvbIyqBQOAE6d3Tx5qMtvJAUmuECAskL39ROGLLv9iumjirmQlJITp5OWxBnjy8aV5ytTnDm6uFmiHbcMlz6+ogAh689u/oYJgD79+D6l4iYXEj1OTa2Cc/kRUJ/og7CUCGfFuspnIpkCUCF7xMbqpxIUzngC3ZtCdfxkaB9UFq6rVB0NtFt4iIg9wnIA2bPOeRL+coGdaYPPqtZu+ErnpT+z/WbAV/3m4XPHAOH+jgHC/R0DhPs7Bgj3dwwQ7u8YINzfMUC4v2OAcH/HAOH+jv8DGRdJO0o/P5IAAAAASUVORK5CYII=";

        /// <summary>
        /// Encoded small icon for plugins
        /// </summary>
        internal const string B64_IMAGE_SMALL = "iVBORw0KGgoAAAANSUhEUgAAACAAAAAgCAIAAAD8GO2jAAAAFXRFWHRDcmVhdGlvbiBUaW1lAAfhBQoODBmh0CQlAAAAB3RJTUUH4QUKDg8Dop5t7AAAAAlwSFlzAAALEgAACxIB0t1+/AAAA1RJREFUeNpj/P//PwMtARNNTR+1YNQCkgAwuf/9hyXFs8BZZ++/DZhwaGeZEwszY/iUIw/ffgXq4eVgddeV7I40EuBiW33qYdmK8x++/WZkYBDn50iyVy7x1GRkZHz9+Uf2wtPbLj77+vOPnDB3sadGrqs6UBzdgp9//j15/+333387Lr+49uzj/FRLNhamp++/Nay7BJRalG4179BdoDXdEUb/Gf6fvPsWaJkEP2estWLa3JNnH7ybkWAmxs9x8PrL/CVnlUR5fQyl0S2AA6BPudiYoywVINxn778vPHIPIq4qwRtiJgdkh5rJ77v2Ytfl50EmslsuPJ2WYBZjrQgUd9OR3HvtxcqTD+EWEI4DUV727z//YoqL8XF8//0H6MU/QIvFeeHiquJ8wOAlIZLhoYk9kYBlgYGGHNvIGihNRdKCXMCouvrkI9x0YPwpi/FgSUXkAU425jAzucb1l3/9/SfOx3HwxqsLD9/3RhphsYCHg8VUURioQZKf00hBCC4uwc8B4apL8PFyssLFgVygYiADGMNVqy5M2X3z848/CiLcSzOtHbUkECFM6/oAJYh+/v77688/JiZGbnaWX3+AKYcRGL6QkP3+6y8DI5DB8A+cXYFRy8PBChT88O0XMGSAWv79/w/MaMA4ByZx5HSBsODx269WTTs1pPiBpm8osPfuPQA0d3e587uvv/z6DzAzMZori3z89vvwzVdARwADLcFWqWzleWCYvPjwfXupU8nyc3defgaarC0tMCvJHIsFb778tFARXZ1rC2TffvFZkJsNmLNuvfi8+8pzL33paj8diLI5B+68/PQDyHXu2LM2z1ZDkr9989VVJx8+evt1VY4tsAjRKNuMM4j2X39h27LLQVOclZlJQ5IPmIOWn3gADBbkZIdw0OefwOQAZEgJcN5//QXImLrn5qbzT/Pc1JGVoeQDR02JwzVuTUF6W84/AVnOxLjx7BN1Sb79119++fEbHCsIoCLOe+reW2Awnr7/Vhmck6OtFHk5WHwNZbBbwMPOoinFB2Q8effNWVuiKVgfiIAMc2VhKUFOYJQASzFwquWUFeICMlpD9Du3XjOp3/Hj919gVtCTFQQWHsDQ7995HTll0jyZDp0abdSCUQtoBwA40GvwupdjYgAAAABJRU5ErkJggg==";

        internal const int U_HEADER_MAINWIDTH = 200;

        /// <summary>
        /// Text for solution that is not available
        /// </summary>
        internal const string U_ITEM_NA = "N/A";

        /// <summary>
        /// Default solution unique name
        /// </summary>
        internal const string U_SOLUTION_DEFAULT = "Default";

        #endregion Internal Fields

        #region Internal Structs

        /// <summary>
        /// Structure holding CRM related constants
        /// </summary>
        internal struct Crm
        {
            #region Internal Structs

            /// <summary>
            /// Structure holding CRM Attributes related constants
            /// </summary>
            internal struct Attributes
            {
                #region Internal Fields

                internal const string CULTURE = "culture";
                internal const string EVENT_HANDLER = "eventhandler";
                internal const string FRIENDLY_NAME = "friendlyname";
                internal const string IS_HIDDEN = "ishidden";
                internal const string IS_MANAGED = "ismanaged";
                internal const string IS_VISIBLE = "isvisible";
                internal const string ISOLATION_MODE = "isolationmode";
                internal const string NAME = "name";
                internal const string PLUGIN_TYPE_ID = "plugintypeid";
                internal const string PUBLIC_KEY_TOKEN = "publickeytoken";
                internal const string SOLUTION_ID = "solutionid";
                internal const string STATE_CODE = "statecode";
                internal const string STATUS_CODE = "statuscode";
                internal const string UNIQUE_NAME = "uniquename";
                internal const string PRIMARY_OBJECT_TYPE_CODE = "primaryobjecttypecode";
                internal const string VERSION = "version";

                #endregion Internal Fields
            }

            /// <summary>
            /// Structure holding CRM Entities related constantns
            /// </summary>
            internal struct Entities
            {
                #region Internal Fields

                /// <summary>
                /// Name of the 'pluginassembly' entity
                /// </summary>
                internal const string PLUGIN_ASSEMBLY = "pluginassembly";

                /// <summary>
                /// Name of the 'plugintype' entity
                /// </summary>
                internal const string PLUGIN_TYPE = "plugintype";

                /// <summary>
                /// Name of the 'sdkmessageprocessingstep' entity
                /// </summary>
                internal const string PROCESSING_STEP = "sdkmessageprocessingstep";

                /// <summary>
                /// Name of the 'sdkmessage' entity
                /// </summary>
                internal const string MESSAGE = "sdkmessage";

                /// <summary>
                /// Name of the 'sdkmessagefilter' entity
                /// </summary>
                internal const string MESSAGE_FILTER = "sdkmessagefilter";

                /// <summary>
                /// Name of the 'solution' entity
                /// </summary>
                internal const string SOLUTION = "solution";

                #endregion Internal Fields
            }

            #endregion Internal Structs
        }

        /// <summary>
        /// Structure holding UI related constants
        /// </summary>
        internal struct UI
        {
            #region Internal Fields

            /// <summary>
            /// Name of the plugin toolstrip
            /// </summary>
            internal const string MENU = "tsMenu";

            #endregion Internal Fields

            #region Internal Structs

            /// <summary>
            /// Structure holding UI Buttons related constants
            /// </summary>
            internal struct Buttons
            {
                #region Internal Fields

                /// <summary>
                /// Name of toolstrip's Back button
                /// </summary>
                internal const string BACK = "tsbBack";

                /// <summary>
                /// Name of toolstrip's Compare button
                /// </summary>
                internal const string COMPARE = "tsbCompare";

                /// <summary>
                /// Name of toolstrip's Open drop down button
                /// </summary>
                internal const string OPEN = "tsddOpen";

                internal const string OPEN_CURRENT_CONNECTION = "tsmiCurrentConnection";

                internal const string OPEN_REFERENCE_FILE = "tsmiReferenceFile";

                /// <summary>
                /// Name of toolstrip's Save button
                /// </summary>
                internal const string SAVE = "tsbSave";

                #endregion Internal Fields
            }

            /// <summary>
            /// Structure holding UI Labels related constants
            /// </summary>
            internal struct Labels
            {
                #region Internal Fields

                /// <summary>
                /// Text for solutions group
                /// </summary>
                internal const string ASSEMBLIES = "Assemblies";

                /// <summary>
                /// Text for solutions group
                /// </summary>
                internal const string SOLUTIONS = "Solutions";

                #endregion Internal Fields
            }

            #endregion Internal Structs
        }

        /// <summary>
        /// Structure holding XML related constants
        /// </summary>
        internal struct Xml
        {
            #region Internal Fields

            internal const string ASSEMBLIES = "assemblies";
            internal const string ASSEMBLY = "assembly";
            internal const string FRIENDLY_NAME = "friendly-name";
            internal const string SOLUTION = "solution";
            internal const string SOLUTIONS = "solutions";
            internal const string STEP = "step";
            internal const string STEPS = "steps";
            internal const string UNIQUE_NAME = "unique-name";
            internal const string VERSION = "version";
            internal const string PLUGIN = "plugin";
            internal const string PLUGINS = "plugins";
            internal const string ID = "id";
            internal const string IMAGE = "image";

            #endregion Internal Fields
        }

        #endregion Internal Structs
    }
}