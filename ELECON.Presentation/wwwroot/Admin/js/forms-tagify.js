/**
 * Tagify
 */


    // Basic
    // Read only
    //------------------------------------------------------
   

    // Users List suggestion
let TagifyUserList; // تعریف متغیر در سطح گلوبال برای استفاده از Tagify

function GetUserListForTicket() {
    const TagifyUserListEl = document.querySelector('#TagifyUserList');
    if (!TagifyUserListEl) return;

    // اگر Tagify مقداردهی شده باشد، ابتدا آن را حذف کنیم
    if (TagifyUserList) {
        TagifyUserList.destroy(); // حذف کامل Tagify
        TagifyUserList = null;    // مقداردهی مجدد برای جلوگیری از خطا
    }

    // دریافت مقدار جستجو از input
    const searchQuery = TagifyUserListEl.value.trim();
    console.log("Search query:", searchQuery);

    if (!searchQuery) return;

    fetch(`/Admin/Ticket/GetList?search=${encodeURIComponent(searchQuery)}`)
        .then(res => res.json())
        .then(data => {
            console.log("Raw data:", data);

            if (data && data.length) {
                let usersList = data.map(e => ({
                    value: e.id,
                    name: e.fullName,
                    avatar: e.image || '/images.jpg',
                    email: e.mobile
                }));

                // مقداردهی مجدد Tagify بعد از حذف
                TagifyUserList = new Tagify(TagifyUserListEl, {
                    tagTextProp: 'name',
                    enforceWhitelist: true,
                    skipInvalid: true,
                    dropdown: {
                        closeOnSelect: false,
                        enabled: 0,
                        classname: 'users-list',
                        searchKeys: ['name', 'email']
                    },
                    templates: {
                        tag: tagTemplate,
                        dropdownItem: suggestionItemTemplate,
                        dropdownHeader: dropdownHeaderTemplate
                    },
                    whitelist: usersList
                });

                TagifyUserList.on('dropdown:select', onSelectSuggestion)
                    .on('edit:start', onEditStart);
            } else {
                console.log("No data found.");
            }
        })
        .catch(error => console.error("Error fetching data:", error));
}

document.querySelector('.GetUserList').removeEventListener('click', GetUserListForTicket);
document.querySelector('.GetUserList').addEventListener('click', GetUserListForTicket);



// توابع کمکی
function tagTemplate(tagData) {
    return `
        <tag title="${tagData.title || tagData.email}"
            contenteditable='false'
            spellcheck='false'
            tabIndex="-1"
            class="${this.settings.classNames.tag} ${tagData.class ? tagData.class : ''}"
            ${this.getAttributes(tagData)}
        >
            <x title='' class='tagify__tag__removeBtn' role='button' aria-label='remove tag'></x>
            <div>
                <div class='tagify__tag__avatar-wrap'>
                    <img onerror="this.style.visibility='hidden'" src="/saveimages/${tagData.avatar}">
                </div>
                <span class='tagify__tag-text'>${tagData.name}</span>
            </div>
        </tag>
    `;
}

function suggestionItemTemplate(tagData) {
    return `
        <div ${this.getAttributes(tagData)}
            class='tagify__dropdown__item align-items-center ${tagData.class ? tagData.class : ''}'
            tabindex="0"
            role="option"
        >
            ${tagData.avatar ? `<div class='tagify__dropdown__item__avatar-wrap'>
                <img onerror="this.style.visibility='hidden'" src="/saveimages/${tagData.avatar}">
            </div>` : ''}
            <div class="fw-medium">${tagData.name}</div>
            <span>${tagData.email}</span>
        </div>
    `;
}

function dropdownHeaderTemplate(suggestions) {
    return `
        <div class="${this.settings.classNames.dropdownItem} ${this.settings.classNames.dropdownItem}__addAll">
            <strong>${this.value.length ? `افزودن باقی موارد` : 'افزودن همه'}</strong>
            <span>${suggestions.length} کاربر</span>
        </div>
    `;
}

function onSelectSuggestion(e) {
    if (e.detail.elm.classList.contains(`${TagifyUserList.settings.classNames.dropdownItem}__addAll`)) {
        TagifyUserList.dropdown.selectAll();
    }
}

function onEditStart({detail: {tag, data}}) {
    TagifyUserList.setTagTextNode(tag, `${data.name} <${data.email}>`);
}

// اتصال رویداد به دکمه برای جلوگیری از اجرای چندباره



