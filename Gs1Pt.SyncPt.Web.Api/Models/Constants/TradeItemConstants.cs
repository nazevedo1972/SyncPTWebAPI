namespace Gs1Pt.SyncPt.Web.Api.Models.Constants
{
    public static class TradeItemConstants
    {
        //Status and Reason Status
        public static string TRADEITEM_STATUS_PREPUBLISHED = "PREPUBLISHED";
        public static string TRADEITEM_STATUS_PUBLISHED = "PUBLISHED";
        public static string TRADEITEM_STATUS_DRAFT = "DRAFT";
        public static string TRADEITEM_STATUS_RECEIVED = "RECEIVED";
        public static string TRADEITEM_STATUS_ERROR = "ERROR";
        public static string TRADEITEM_STATUS_INACTIVE = "INACTIVE";
        public static string TRADEITEM_STATUS_DELETED = "DELETED";
        public static string TRADEITEM_REASON_STATUS_HIERARCHY = "HIERARCHY"; //Trade Item Status changed because exist a action that have this in your hierarchy
        public static string TRADEITEM_REASON_STATUS_TRADEITEM = "TRADEITEM"; //Trade Item Status changed by a action in this
        public static string TRADEITEM_REASON_STATUS_TRADEITEM_NEW_TRANSLATION = "TRADEITEM_NEW_TRANSLATION";
        public static string TRADEITEM_REASON_STATUS_TRADEITEM_NEW_VERSION = "TRADEITEM_NEW_VERSION";

        public static int TRADEITEM_INITIAL_VERSION = 1;
        public static string TRADEITEM_ALLERGEN_SPECIFICATION_AGENCY = "EU";
        public static string TRADEITEM_ALLERGEN_SPECIFICATION_NAME = "1169/2011";
        public static string TRADEITEM_DUTYFEETAX_AGENCY_CODE = "9";
        public const string TRADEITEM_LANGUAGE_DEFAULT = "pt";
        public static string TRADEITEM_TARGETMARKET_COUNTRYCODE_DEFAULT = "620";
        public static string TRADEITEM_PERMISSION_GLN_DEFAULT = "5600091484236";
        public static string TRADEITEM_MEASUREMENT_COMP_VALUE31_DEFAULT = "CMT";
        public static string TRADEITEM_MEASUREMENT_GROSSWEIGHT_VALUE31_DEFAULT = "GRM";
        public static int TRADEITEM_FILE_SIZE_MEASUREMENT_BYTE_ID = 1162;
        public static int TRADEITEM_FILE_TYPET_PRODUCTIMAGE_ID = 1365;
        public static string TRADEITEM_FILE_TYPE_PRODUCTIMAGE = "PRODUCT_IMAGE";
        public static string TRADEITEM_FILE_TYPE_VIDEO = "VIDEO";
        public static string TRADEITEM_PLATFORM_TYPE_CODE_DEFAULT = "11";

        public static int TRADEITEM_NUTRIENTHEADER_DEFAULT_MEASUREMENT = 100;
        public static int TRADEITEM_NUTRIENTHEADER_DEFAULT_PREPARATIONSTATECODE = 2359;

        //Is Packaging Recoverable
        public static string TRADEITEM_PACKAGINGMATERIAL_RECOVERABLE_GLASS = "GLASS";
        public static string TRADEITEM_PACKAGINGMATERIAL_RECOVERABLE_GLASS_COLOURED = "GLASS_COLOURED";

        //TradeItem Classes
        public static int TRADEITEM_CLASS_GENERAL = 3;
        public static string TRADEITEM_CLASS_GENERAL_CODE = "GENERAL";
        public static int TRADEITEM_CLASS_FISH = 7;
        public const string TRADEITEM_CLASS_RNC = "RNC";
        public const int TRADEITEM_CLASS_RNC_ID = 22;

        //TradeItem ViewingProfile
        public static int TRADEITEM_VIEWING_PROFILE_DEFAULT = 3;

        //Versions
        public static string TRADEITEM_VERSION_313 = "3.1.3";
        public static string TRADEITEM_VERSION_31 = "3.1";
        public static string TRADEITEM_VERSION_28 = "2.8";

        //Trade Item Unit Descriptor
        public const string TRADEITEM_UNIT_DESCRIPTOR_BASE_UNIT_OR_EACH = "BASE_UNIT_OR_EACH";
        public const string TRADEITEM_UNIT_DESCRIPTOR_PACK_OR_INNER_PACK = "PACK_OR_INNER_PACK";
        public const string TRADEITEM_UNIT_DESCRIPTOR_DISPLAY_SHIPPER = "DISPLAY_SHIPPER";
        public const string TRADEITEM_UNIT_DESCRIPTOR_CASE = "CASE";
        public const string TRADEITEM_UNIT_DESCRIPTOR_MIXED_MODULE = "MIXED_MODULE";
        public const string TRADEITEM_UNIT_DESCRIPTOR_PALLET = "PALLET";
        public const string TRADEITEM_UNIT_DESCRIPTOR_TRANSPORT_LOAD = "TRANSPORT_LOAD";

        //Trade Item Unit Descriptor Id's 

        public static int TRADEITEM_UNIT_DESCRIPTOR_BASE_UNIT_OR_EACH_ID = 2408;
        public static int TRADEITEM_UNIT_DESCRIPTOR_CASE_ID = 2409;
        public static int TRADEITEM_UNIT_DESCRIPTOR_DISPLAY_SHIPPER_ID = 2410;
        public static int TRADEITEM_UNIT_DESCRIPTOR_MIXED_MODULE_ID = 2411;
        public static int TRADEITEM_UNIT_DESCRIPTOR_PACK_OR_INNER_PACK_ID = 2412;
        public static int TRADEITEM_UNIT_DESCRIPTOR_PALLET_ID = 2413;
        public static int TRADEITEM_UNIT_DESCRIPTOR_TRANSPORT_LOAD_ID = 2414;
        public static int TRADEITEM_UNIT_DESCRIPTOR_GLN_ID = 49809;


        //Trade Item Unit Descriptor
        public static string TRADEITEM_UNIT_DESCRIPTOR_IMG_BASE_UNIT_OR_EACH = "Unidade_Base.png";
        public static string TRADEITEM_UNIT_DESCRIPTOR_IMG_PACK_OR_INNER_PACK = "Caixa_Intermedia.png";
        public static string TRADEITEM_UNIT_DESCRIPTOR_IMG_CASE = "Caixa.png";
        public static string TRADEITEM_UNIT_DESCRIPTOR_IMG_PALLET = "Palete.png";
        public static string TRADEITEM_UNIT_DESCRIPTOR_IMG_TRANSPORT_LOAD = "Shipping_Container.png";

        //AVP's
        public static string TRADEITEM_AVP_CODELISTNAMECODE_LANGUAGE_CODE = "LANGUAGE_CODE";

        public static string TRADEITEM_ACTIVITY_TYPE_BIRTH = "BIRTH";
        public static string TRADEITEM_ACTIVITY_TYPE_REARING = "REARING";
        public static string TRADEITEM_ACTIVITY_TYPE_SLAUGHTER = "SLAUGHTER";
        public static string TRADEITEM_ACTIVITY_TYPE_LAST_PROCESSING = "LAST_PROCESSING";
        public static string TRADEITEM_ACTIVITY_TYPE_CATCH_ZONE = "CATCH_ZONE";
        public static string TRADEITEM_APPROVAL_NUMBER_SLAUGHTERER = "slaughtererNumber";
        public static string TRADEITEM_APPROVAL_NUMBER_CUTTER = "cutterNumber";
        public static string TRADEITEM_AVP_CNP = "CNP";
        public static string TRADEITEM_AVP_PRODUCE_VARIETY_TYPE = "produceVarietyType";
        public static string TRADEITEM_AVP_NAME_NON_GTIN_PALLET_NETWEIGHT = "nonGTINPalletNetWeight";
        public static string TRADEITEM_AVP_NAME_NON_GTIN_PALLET_TARE = "nonGTINPalletTare";
        public static string TRADEITEM_AVP_NAME_NON_GTIN_PALLET_PLATFORM_TYPE_CODE = "nonGTINPalletPlatformTypeCode";
        public static string TRADEITEM_AVP_NAME_DATE_ON_PACKAGING_TYPE_LOCAL_CODE = "tradeItemDateOnPackagingTypeCodeLocal";
        public static string TRADEITEM_AVP_FEED_COMPOSITION_STATEMENT = "feedCompositionStatement";
        public static string TRADEITEM_AVP_FEED_ANALYLITICAL_CONSTITUENTS_STATEMENTS = "feedAnalyticalConstituentsStatement";
        public static string TRADEITEM_AVP_FEED_ADDITIVE_STATEMENTS = "feedAdditiveStatement";

        public static string TRADEITEM_AVP_CODELIST_MEASUREMENT_CODE = "MEASUREMENT_CODE";
        public static string TRADEITEM_AVP_AUDIT_STATE = "auditState";
        public static string TRADEITEM_AVP_AUDIT_SUBSTATE = "auditSubState";
        public static string TRADEITEM_AVP_AUDIT_STANDARD_ASSESSED = "auditStandardAssessed";
        public static string TRADEITEM_AVP_AUDIT_GLOBAL_COMMENTS = "auditGlobalComments";
        public static string TRADEITEM_AVP_AUDIT_ARTICLE_CORRECTED = "auditArticleCorrected";
        public static string TRADEITEM_AVP_AUDIT_IS_ARTICLE_SUPPLIER = "auditIsArticleSupplier";

        //DutyFeeTax_Tax_Quantity
        public static string TRADEITEM_AVP_DUTYFEETAX_TAX = "DUTYFEETAX_TAX";
        public static string TRADEITEM_AVP_DUTYFEETAX_QUANTITY = "DUTYFEETAX_QUANTITY";

        //CodeLists
        public static string TRADEITEM_PRODUCTION_METHOD_FOR_FISH_AND_SEAFOOD_MARINE_FISHERY = "MARINE_FISHERY";
        public static string TRADEITEM_PRODUCTION_METHOD_FOR_FISH_AND_SEAFOOD_INLAND_FISHERY = "INLAND_FISHERY";
        public static string TRADEITEM_PRODUCTION_METHOD_FOR_FISH_AND_SEAFOOD_AQUACULTURE = "AQUACULTURE";
        public static string TRADEITEM_UNSPECIFIED_CONTACT_TYPE = "DSU";
        public static string TRADEITEM_DATEONPACKAGING_FROZEN_ON_CODE = "FROZEN_ON";



        public static string TRADEITEM_UNDEFINED = "undefined";
        public static string ADMIN_EXCEL_EMAIL = "adminsyncptexcel@gs1pt.org";
        public static string CLASS_GENERAL = "GENERAL";
        public static string STRINGNA = "N/A";
        public static string BASE_UNIT_OR_EACH = "BASE_UNIT_OR_EACH";
        public static string EAN = "EAN_UPC";
        public static string PROVISORY_CLASSIFICATION_VALUE = "99999999";

        //CNP PREFIX
        public static string TRADEITEM_GTIN_PREFIX_TO_CNP = "560535";

        //MEDIA MAX FILE SIZE
        public const long TRADEITEM_MEDIA_MAX_FILE_SIZE_BYTES = 50 * 1024 * 1024;

        //IMPORT MEDIAS ZIP MAX FILE SIZE
        public const long IMPORTMEDIASBYZIP_MAX_FILE_SIZE_BYTES = 100 * 1024 * 1024;

        //IMPORT EXCEL MAX FILE SIZE
        public const long IMPORTEXCEL_MAX_FILE_SIZE_BYTES = 100 * 1024 * 1024;

        //MEDIA TYPE PLANOGRAM
        public static string TRADEITEM_MEDIA_TYPE_PLANOGRAM = "PLANOGRAM";

        //Media Access
        public static string TRADEITEM_MEDIA_ACCESS_PUBLIC = "PUBLIC";
        public static string TRADEITEM_MEDIA_ACCESS_PRIVATE = "PRIVATE";
        public static string TRADEITEM_MEDIA_ACCESS_EXTERNAL = "EXTERNAL";

        //Media format
        public static string TRADEITEM_MEDIA_FORMAT_CSV = "CSV";
        public static string TRADEITEM_MEDIA_FORMAT_EXCEL = "XLSX";
        public static string TRADEITEM_MEDIA_FORMAT_EXCEL2 = "XLS";
        public static string TRADEITEM_MEDIA_FORMAT_WORD = "DOCX";
        public static string TRADEITEM_MEDIA_FORMAT_WORD2 = "DOC";
        public static string TRADEITEM_MEDIA_FORMAT_PPT = "PPT";
        public static string TRADEITEM_MEDIA_FORMAT_PPTX = "PPTX";
        public static string TRADEITEM_MEDIA_FORMAT_PDF = "PDF";
        public static string TRADEITEM_MEDIA_FORMAT_PSD = "PSD";
        public static string TRADEITEM_MEDIA_FORMAT_fileName = "fileName";
        public static string TRADEITEM_MEDIA_FORMAT_TXT = "TXT";
        public static string TRADEITEM_MEDIA_FORMAT_GIF = "GIF";
        public static string TRADEITEM_MEDIA_FORMAT_JPEG = "JPEG";
        public static string TRADEITEM_MEDIA_FORMAT_PNG = "PNG";
        public static string TRADEITEM_MEDIA_FORMAT_JPG = "JPG";
        public static string TRADEITEM_MEDIA_FORMAT_BMP = "BMP";
        public static string TRADEITEM_MEDIA_FORMAT_TIFF = "TIFF";
        public static string TRADEITEM_MEDIA_FORMAT_TIF = "TIF";
        public static string TRADEITEM_MEDIA_FORMAT_JFIF = "JFIF";

        public static string TRADEITEM_MEDIA_FORMAT_VIDEO = "VIDEO";

        public static List<string> NotImageFormats = new List<string>
        {
            TRADEITEM_MEDIA_FORMAT_TXT,
            TRADEITEM_MEDIA_FORMAT_CSV,
            TRADEITEM_MEDIA_FORMAT_EXCEL,
            TRADEITEM_MEDIA_FORMAT_EXCEL2,
            TRADEITEM_MEDIA_FORMAT_WORD,
            TRADEITEM_MEDIA_FORMAT_WORD2,
            TRADEITEM_MEDIA_FORMAT_PPT,
            TRADEITEM_MEDIA_FORMAT_PPTX,
            TRADEITEM_MEDIA_FORMAT_PDF,
            TRADEITEM_MEDIA_FORMAT_PSD,
            TRADEITEM_MEDIA_FORMAT_VIDEO
        };

        public static bool MediaIsImage(string mediaFormat)
        {
            mediaFormat = RemoveWhitespace(mediaFormat);
            return !NotImageFormats.Contains(mediaFormat.ToUpper());
        }

        public static bool MediaIsVideo(string mediaFormat)
        {
            mediaFormat = RemoveWhitespace(mediaFormat);
            return mediaFormat.ToUpper() == TRADEITEM_MEDIA_FORMAT_VIDEO;
        }

        public static bool MediaIsDocument(string mediaFormat)
        {
            return !MediaIsImage(mediaFormat) && !MediaIsVideo(mediaFormat);
        }

        public static string MediaAccessConversion(bool isPublic)
        {
            if (isPublic)
                return TRADEITEM_MEDIA_ACCESS_PUBLIC;
            return TRADEITEM_MEDIA_ACCESS_PRIVATE;
        }

        private static string RemoveWhitespace(this string input)
        {
            return new string(input.ToCharArray()
                .Where(c => !Char.IsWhiteSpace(c))
                .ToArray());
        }
    }
}
