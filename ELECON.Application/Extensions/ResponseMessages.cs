namespace ELECON.Application.Extensions;

public class ResponseMessages
{
    public class SuccessMessages
    {
        public static string  SelectPaymenetAndAddressWarning = "لطفا آدرس و درگاه را انتخاب کنید";
        public static string  SentSMTPCodeForLogin = "برای ثبت نام لطفا کد یکبار مصرف ارسالی را وارد کنید";
        public static string  RemoveUser = "کاربر با موفقیت حذف شد";
        public static string  AddUser = "کاربر با موفقیت اضافه شد";
        public static string  UpdateUser = "کاربر با موفقیت ویرایش شد";
        public static string BanUser = "کاربر با موفقیت مسدود شد";
        public static string  ActiveUser = "کاربر با موفقیت آزاد شد";
        public static string  UserProfile = "عکس کاربر با موفقیت ویرایش شد";
        public static string  UserPassword = "رمز کاربر با موفقیت ویرایش شد";
        public static string  AddTopCategory = "بالا دسته با موفقیت اضافه شد";
        public static string  AddCategory = " دسته با موفقیت اضافه شد";
        public static string  SubAddCategory = "زیر دسته با موفقیت اضافه شد";
        public static string  UpdateTopCategory = "بالا دسته با موفقیت ویرایش شد";
        public static string  DeleteTopCategory = "بالا دسته با موفقیت حذف شد";
        public static string  DeletTopCategory = " دسته با موفقیت حذف شد";
        public static string  DeleteCategory = " دسته با موفقیت حذف شد";
        public static string  DeleteDetail= " ویژگی با موفقیت حذف شد";
        public static string  DeleteBrand= " برند با موفقیت حذف شد";
        public static string  UpdateCategory = " دسته با موفقیت ویرایش شد";
        public static string  AddDetail = " ویژگی با موفقیت اضافه شد";
        public static string  AddBrand = " برند با موفقیت اضافه شد";
        public static string  UpdateDetail = " ویژگی با موفقیت ویرایش شد";
        public static string  UpdateBrand = " برند با موفقیت اضافه شد";
        public static string  UpdateProduct = " محصول با موفقیت ویرایش شد";
        public static string  UpdateProductImage = " عکس با موفقیت ویرایش شد";
        public static string  UpdateProductColor = " رنگ با موفقیت ویرایش شد";
        public static string  DeleteColor = " رنگ با موفقیت حذف شد";
        public static string  AddProductColor = " رنگ با موفقیت اضافه شد";
        public static string  AddProduct = " محصول با موفقیت اضافه شد";
        public static string  AddProductImage = "عکس محصول با موفقیت اضافه شد";
        public static string  DeletProduct = " محصول با موفقیت حذف شد";
        public static string  DeleteProductImage = "عکس محصول با موفقیت حذف شد";
        public static string  AddTicket = "تیکت با موفقیت ایجاد شد";
        public static string  AddTicketByUsere = "تیکت با موفقیت ایجاد شد,کارشناسان ما بعد از بررسی به تیکت شما پاسخ میدهند";
        public static string  AddAnsewrTicket = "پاسخ تیکت با موفقیت ارسال شد";
        public static string  CloseTicket = "پاسخ تیکت با موفقیت بسته شد";
        public static string  OpenTicket = "پاسخ تیکت با موفقیت باز شد";
        public static string  CommentApprove = "کامنت تایید شد";
        public static string  CommentAnswered = "کامنت جواب داده شد";
        public static string  CommentDenied = "کامنت رد شد";
        public static string  AddCategoryBlog = "دسته بندی بلاگ افزوده شد";
        public static string  UpdateCategoryBlog = "دسته بندی بلاگ ویرایش شد";
        public static string  DeleteCategoryBlog = "دسته بندی بلاگ حذف شد";
        public static string  BlogInsert = " بلاگ اضافه شد";
        public static string  BlogUpdate = " بلاگ ویرایش شد";
        public static string  BlogDelete = " بلاگ حذف شد";
        public static string  CommentBlogDelete = "کامنت بلاگ حذف شد";
        public static string  BlogShowApprove = " بلاگ به نمایش در آمد";
        public static string  CommentBlogShowApprove = "کامنت بلاگ به نمایش در آمد";
        public static string  BlogShowDeneid = " بلاگ از نمایش حذف شد";
        public static string  CommentBlogShowDeneid = "کامنت بلاگ از نمایش حذف شد";
        public static string  DynamicShopAdd = " تم فروشگاه با موفقیت افزوده شد";
        public static string  DynamicShopUpdate = " تم فروشگاه با موفقیت ویرایش شد";
        public static string  DynamicShopDelete = " تم فروشگاه با موفقیت حذف شد";
        public static string  DynamicShopActive = " تم فروشگاه با موفقیت فعال شد";
        public static string  DynamicBannerInsert = " بنر فروشگاه با موفقیت افزوده شد";
        public static string  DynamicBannerUpdate = " بنر فروشگاه با موفقیت ویرایش شد";
        public static string  DynamicBannerDelete = " بنر فروشگاه با موفقیت حذف شد";
        public static string  DynamicSweeperInsert = "محصولات برای نمایش با موفقیت اضافه شدند";
        public static string  DynamicSweeperDelete = "محصولات  نمایشی با موفقیت حذف شد";


    }
    public class ErrorMessages
    {
        public static string USerAddressNotFound = "آدرس شما پیدا نشد";
        public  static string UserNotFound = "کاربر پیدا نشد";
        public static string EmialIsInUse = "ایمیل قبلا استفاده شده است";
        public static string NationalCodeInUse = "کد ملی قبلا استفاده شده است";
        public static string MobileIsInUse = "تلفن همراه قبلا استفاده شده است";
        public static string RoleNotFound = "نقش پیدا نشد";
        public static string TopCategoryNotFound = "بالا دسته پیدا نشد";
        public static string ImageNotFound = "عکس محصول پیدا نشد";
        public static string SubCategoryNotFound = "زیر دسته پیدا نشد";
        public static string TopDetailNotFound = "ویژگی  پیدا نشد";
        public static string CategoryNotFound = " دسته پیدا نشد";
        public static string CategoryDuplicate = "بالا دسته بندی تکراری است";
        public static string SubCategoryDuplicate = "زیر دسته بندی تکراری است";
        public static string DeleteTopCategory = "بالا دسته حذف نشد";
        public static string DeleteDetailTopCategory = "ویژگی حذف نشد";
        public static string DeleteBrand = "برند حذف نشد";
        public static string DeleteCategory = " دسته حذف نشد";
        public static string PriorityInUse = "اولویت از قبل انتخاب شده است";
        public static string DetailDuplicate = "نام ویژگی برای این دسته بندی تکراری است";
        public static string NameDuplicate = "نام تکراری است";
        public static string BrandDuplicate = "نام برند برای این دسته بندی تکراری است";
        public static string DetailNotFound  = " ویژگی پیدا نشد";
        public static string BrandNotFound  = " برند پیدا نشد";
        public static string inputWrong = "موارد خواسته شده را با دقت وارد کنید";
        public static string SlugExist = "اسلاگ برای تکراری است";
        public static string ProductTitleExist = "نام برای تکراری است";
        public static string ProductTagExist = "تگ برای تکراری است";
        public static string ProductNotCreate = "محصول ایجاد نشد";
        public static string ProductNotfound = "محصول پیدا نشد";
        public static string ProductNotEdit = "محصول ویرایش نشد";
        public static string ColorNotFound = "رنگ پیدا نشد";
        public static string ProductDiscountTimeNorValid = "زمان تخفیف محصول مناسب نیست";
        public static string TicketNotInsert = "تیکت ایجاد نشد";
        public static string TicketNotfound = "تیکت پیدا نشد";
        public static string  AddAnsewrTicket = "پاسخ تیکت  ارسال نشد";
        public static string  CommentInsert = "کامنت شما ایجاد نشد";
        public static string  CommentNotfound = "کامنت  پیدا نشد";
        public static string  CategoryBlogNotInsert = "دسته بندی بلاگ اضافه نشد";
        public static string  BlogNotInsert = " بلاگ اضافه نشد";
        public static string  BlogNotFound = " بلاگ اضافه پیدا نشد";
        public static string  CategoryBlogNotFound = "دسته بندی بلاگ پیدا نشد";
        public static string  DynamicShopDuplicate = "نام تم فروشگاه تکراری است";
        public static string  DynamicShopNotInsert = " تم فروشگاه افزوده نشد";
        public static string  DynamicBannerNotInsert = " بنر فروشگاه افزوده نشد";
        public static string  DynamicShopNotFound = " تم فروشگاه پیدا نشد";
        public static string  DynamicBannerNotFound = " بنر فروشگاه پیدا نشد";
        public static string  ProductSweeperDuplicate = "محصول نمایشی برای دسته بندی تکراری است";
        public static string  ProductSweeperNotFound = "محصول نمایشی پیدا نشد";
        public static string  BannerNotFound = "بنر پیدا نشد";
        public static string  EmailNotRegistered = "ایمیل ثبت نشده است لطفا با شماره موبایل خود ثبت نام کنید";

    
       
        
    }
    public class WarningMessages
    {
        public static string InputWarning = "لطفا موارد خواسته شده را با دقت وارد کنید";
        public static string SomethingsGoesWrong = "خطایی پیش امده است";
        public static string TopCategoryInUse = "بالا دسته در حال استفاده شدن است ";
        public static string CategoryInUse = " دسته در حال استفاده شدن است ";
        public static string DetailInUse = " ویژگی در حال استفاده شدن است ";
        public static string BrandInUse = " برند در حال استفاده شدن است ";
        

       
    }
    public class InfoMessages
    {
        public static string  UserGotBanned = "نام کاربری شما در سایت مسدود شده است لطفا با پشتیبانی تماس حاصل فرماید";
       
    }
}