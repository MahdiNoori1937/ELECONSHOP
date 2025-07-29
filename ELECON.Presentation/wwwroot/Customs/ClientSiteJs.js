function ModalShow(url) {
    // fetch(url).then(response => {
    //     return response.json().then(data => {
    //         if (data.status === 400) {
    //             location.href = "/Admin/NotFound";
    //         }
    //
    //     })
    // })

    $.get(url, function (res) {
        $("#EditModal").modal('show');
        $("#EditBodyModal").html(res);
    });
}

function SecondModalShow(url) {
    $.get(url, function (res) {
        $("#secondModal").modal('show');
        $("#secondModalBody").html(res);
    })
}


function OnSuccessSecondModalShow(Result) {
    if (Result && Result.status === 200) {
        $("#EditModal").modal('hide');
        swal.fire({
            title: "عملیات موفق",
            text: Result.message,
            icon: `${Result.type}`
        })
        setTimeout(() => {
            $("#secondModal").modal('hide');
            ModalShow(Result.link);
        }, 200)
    } else {
        Swal.fire({
            title: "عملیات نا موفق",
            text: Result.message,
            icon: `${Result.type}`
        });
    }

}

function OnsuccessSendNoModal(result) {
    if (result && result.status === 200) {
        Swal.fire({
            title: "عملیات موفق",
            text: result.message,
            icon: `${result.type}`
        });
        setTimeout(() => {
            if (result.link) {
                location.href = result.link
            } else {
                location.reload()
            }
        }, 2000)
    }
    if (result && result.status === 400) {
        Swal.fire({
            title: "عملیات نا موفق",
            text: result.message,
            icon: `${result.type}`
        });
        if (result.link) {
            setTimeout(() => {
                location.href = result.link;
            }, 2000)
        }
    }

}

///OnsuccessWithModal
function OnsuccessWithModal(result) {
    if (result && result.status === 200) {
        Swal.fire({
            title: "عملیات موفق",
            text: result.message,
            icon: `${result.type}`
        });
        $("EditModal").modal('hide')
        setTimeout(() => {
            location.reload()
        }, 2000)
    }
    if (result && result.status === 400) {
        Swal.fire({
            title: "عملیات نا موفق",
            text: result.message,
            icon: `${result.type}`
        });
        if (result.link) {
            setTimeout(() => {
                location.href = result.link;
            }, 2000)
        }
    }
}

/////SubmitSmtpSender
const smtpCodeSender = (e) => {
    e.preventDefault()
    const InputEl = document.querySelector('#username');
    const senderInput = document.querySelector('.input-sender')
    senderInput.value = InputEl.value
    $(".Smtp-Submit").submit()

}
const LoginPassword = (e) => {
    if ($(".step-passwd #passwd").val() !== "") {
        e.preventDefault()
        const InputEl = document.querySelector('#username');
        const senderInput = document.querySelector('.input-sender-password')
        senderInput.value = InputEl.value
        $(".Login-Password").submit()
    } else {
        $(".step-passwd #passwd").addClass("border-danger border-2");
    }
}

function SwalConfirmChangeReload(url, e) {
    e.preventDefault()
    swal.fire({
        title: "سوال",
        text: "آیا از انجام این عملیات مطمئن هستید؟",
        icon: "question",
        showCancelButton: true,
        confirmButtonColor: "#3085d6",
        cancelButtonColor: "#d33",
        confirmButtonText: "بله",
        cancelButtonText: "خیر"
    })
        .then((res) => {
            if (res.isConfirmed) {
                fetch(`${url}`).then(data => {
                    return data.json()
                }).then(result => {
                    if (result && result.status === 200) {
                        Swal.fire({
                            title: "عملیات موفق",
                            text: result.message,
                            icon: `${result.type}`
                        });
                        setTimeout(() => {
                            location.reload()
                        }, 2000)
                    } else {
                        Swal.fire({
                            title: "خطا",
                            text: result.message,
                            icon: `${result.type}`
                        });
                    }
                })
            }
        })
}

function SwalConfirmChangeNoReload(url, e, id) {
    e.preventDefault()
    swal.fire({
        title: "سوال",
        text: "آیا از انجام این عملیات مطمئن هستید؟",
        icon: "question",
        showCancelButton: true,
        confirmButtonColor: "#3085d6",
        cancelButtonColor: "#d33",
        confirmButtonText: "بله",
        cancelButtonText: "خیر"
    })
        .then((res) => {
            if (res.isConfirmed) {
                fetch(`${url}`).then(data => {
                    return data.json()
                }).then(result => {
                    if (result && result.status === 200) {
                        Swal.fire({
                            title: "عملیات موفق",
                            text: result.message,
                            icon: `${result.type}`
                        });
                        setTimeout(() => {
                            $("#rowLink_" + id).hide('slow')

                        }, 1000)
                        
                    } else {
                        Swal.fire({
                            title: "خطا",
                            text: result.message,
                            icon: `${result.type}`
                        });
                    }
                })
            }
        })
}

function SwalConfirmChangeReloadWithHide(url, e, id) {
    e.preventDefault()
    swal.fire({
        title: "سوال",
        text: "آیا از انجام این عملیات مطمئن هستید؟",
        icon: "question",
        showCancelButton: true,
        confirmButtonColor: "#3085d6",
        cancelButtonColor: "#d33",
        confirmButtonText: "بله",
        cancelButtonText: "خیر"
    })
        .then((res) => {
            if (res.isConfirmed) {
                fetch(`${url}`).then(data => {
                    return data.json()
                }).then(result => {
                    if (result && result.status === 200) {
                        Swal.fire({
                            title: "عملیات موفق",
                            text: result.message,
                            icon: `${result.type}`
                        });
                        $("#rowLink_" + id).hide('slow')
                        setTimeout(() => {
                           location.reload()
                        }, 1500)
                        
                    } else {
                        Swal.fire({
                            title: "خطا",
                            text: result.message,
                            icon: `${result.type}`
                        });
                    }
                })
            }
        })
}

function FormValidatorSubmit() {
    const bsValidationForms = document.querySelectorAll('.Validate-forms');
    Array.prototype.slice.call(bsValidationForms).forEach(function (form) {
        form.addEventListener(
            'submit',
            function (event) {
                if (!form.checkValidity()) {
                    event.preventDefault();
                    event.stopPropagation();
                } else {
                    $(".needs-validation").submit()
                }
                form.classList.add('was-validated');
            },
            false
        );
    });
}

function FormValidatorSubmitProduct() {
    const bsValidationForms = document.querySelectorAll('.Validate-forms');
    Array.prototype.slice.call(bsValidationForms).forEach(function (form) {
        form.addEventListener(
            'submit',
            function (event) {
                if (!form.checkValidity()) {
                    event.preventDefault();
                    event.stopPropagation();
                } else {
                    $(".needs-validation").submit()
                }
                form.classList.add('was-validated');
            },
            false
        );
    });
}

///To Select Category with sending TopCategory For SubCategory
function SelectCategory(url) {
    const SubCategoryEL = document.querySelector('.SubCategory-Select');
    const dropiEl = document.querySelector('.dropi');
    fetch(url)
        .then(res => res.json())
        .then(data => {
            if (data !== null && data.length > 0) {
                const removeclas = document.querySelectorAll('.remove-class')
                if (removeclas.length > 0) {
                    removeclas.forEach(el => el.remove())
                }
                for (let i = 0; i < data.length; i++) {
                    const DropDownEl = `<option value="${data[i].id}" class="remove-class">${data[i].title}</option>`
                    SubCategoryEL.innerHTML += DropDownEl
                }

            } else {
                SubCategoryEL.innerHTML = '<option class="remove-class" value="">زیر دسته‌ای یافت نشد</option>';
            }
            dropiEl.innerHTML = SubCategoryEL.outerHTML
        })
}

///SelectBrandBySubCategory
function SelectBrand(url) {
    const SubCategoryEL = document.querySelector('.Brand-Select');
    fetch(url)
        .then(res => {
            return res.json()
        }).then(data => {
        SubCategoryEL.innerHTML = ""
        if (data) {
            data.forEach(el => SubCategoryEL.innerHTML += `<option value="${el.id}" class="remove-class">${el.title}</option>`)
        } else {
            SubCategoryEL.innerHTML = '<option class="remove-class" value="">برندی یافت نشد</option>'
        }
    })
}

const Editor = document.querySelector('#full-editor')
const Content = document.querySelector('#editorContent')

Editor.innerText = Content.value;

Editor.addEventListener('input', () => {
    Content.value = Editor.innerText;
})


function SubmitProductShowForm1() {
    $(".filter-product-Form1").submit()

}

function SubmitProductShowForm2() {
    $(".filter-product-Form2").submit()

}

function SubmitProductShowFormWithEnum(id) {
    const EnumSelect = document.querySelector('.Enum-Select')
    EnumSelect.value = id
    SubmitProductShowForm2()

}

////GetColorForSelectProductShow
function GetColorDetail(url) {
    fetch(url).then(res => {
        return res.json()
    }).then(data => {
        if (data) {
            const Price = document.querySelector('.price')
            const Discounted = document.querySelector('.discountedPrice')
            Price.innerText = data.price;
            Discounted.innerText = data.discountedPrice;
        }
    })
}

//////
function setUserTicketIdByAdmin() {
    const tag = document.querySelector('#TagifyUserList').value
    const userid = document.querySelector('.user-ticket-id')
    const data = JSON.parse(tag)
    userid.value = data[0].value
}

document.querySelector('.submit-ticket').addEventListener('click', setUserTicketIdByAdmin)

//////
function ShowPic(picUrl, e) {
    e.preventDefault()
    const ModalBody = document.querySelector('.modal-body-pic')
    ModalBody.innerHTML = ` <img alt="کاربر-آواتار" width="500px" height="500px"  src="/SaveImages/${picUrl}">`
    $("#modalCenter").modal('show');
}

//////UserModalShow
function ShowUserFileAttachment(pickUrl, e) {
    e.preventDefault()
    const ModalBody = document.querySelector('.modal-body')
    ModalBody.innerHTML = ` <img alt="کاربر-آواتار" width="700px" height="700px"  src="/SaveImages/${pickUrl}">`
    $("#EditModal").modal('show');

}

////LikeProductCount
function AddLikeReactionToComment(url, id) {
    const LikeElement = document.querySelector('#LikeCount_' + id)
    const DisLikeElement = document.querySelector('#DisLikeCount_' + id)
    fetch(url)
        .then(res => res.json())
        .then(data => {
            if (data && data.status === 200) {
                LikeElement.innerHTML = String(parseInt(LikeElement.innerHTML) + 1)
            }
            if (data && data.status === 300) {
                LikeElement.innerHTML = String(parseInt(LikeElement.innerHTML) + 1)
                DisLikeElement.innerHTML = String(parseInt(LikeElement.innerHTML) - 1)
            }
        })
}

function AddDisLikeReactionToComment(url, id) {

    fetch(url)
        .then(res => res.json())
        .then(data => {
            const LikeElement = document.querySelector('#LikeCount_' + id)
            const DisLikeElement = document.querySelector('#DisLikeCount_' + id)
            if (data && data.status === 200) {
                DisLikeElement.innerHTML = String(parseInt(LikeElement.innerHTML) + 1)
            }
            if (data && data.status === 300) {
                LikeElement.innerHTML = String(parseInt(LikeElement.innerHTML) - 1)
                DisLikeElement.innerHTML = String(parseInt(LikeElement.innerHTML) + 1)

            }
        })
}

async function ChangeOrderDetailCount(id) {
    const input = document.querySelector('#Counter-Count-'+id);
    let value = parseInt(input.value, 10);

    if (value > 100) {
        input.value = 100;
    } else if (value < 1) {
        input.value = 1;
    }
    const OrderDetailValue = document.querySelector('#Counter-Count-'+id);
    const Response = await fetch(`/UserOrder/ChangeOrderDetailCount?OrderDetailId=${id}&Count=${value}`);
    const result = await Response.json();
    if (result.status === 200)
    {
        location.reload()
    }
    else
    {
        Swal.fire({
            title: "عملیات نا موفق",
            text: result.message,
            icon: `${result.type}`
        });
    }
}
function SetPaymentValue(Id)
{
    const SelectedPayment = document.querySelector('.Payment')
        SelectedPayment.value = Id
}
 function SendPaymentValue(e)
{
    e.preventDefault()
    const PaymentValue = document.querySelector('.Payment');
    let Value = parseInt(PaymentValue.value, 10);
    if (isNaN(Value))
    {
      swal.fire({
            title: "عملیات ناموفق",
            text:"لطفا درگاه بانکی راانتخاب کنید",
            icon: `warning`,
        })
    }
    fetch(`/UserCart/UserOrderPayment?PaymentGateWay=${Value}`).then(res => res.json()).then(Response => {
        if (Response.status === 200)
        {
            document.location.href=Response.url
        }
        else
        {
            swal.fire({
                title: "عملیات ناموفق",
                text: Response.message,
                icon: `warning`
            })
        }
    })
  
   
}
function RegisterValidation(e)
{
    e.preventDefault();
    const RegisterInput =document.querySelector('#Register-input').value;

    const emailRegex = /^[^\s@]+@[^\s@]+\.[^\s@]+$/;
    const phoneRegex = /^(\+98|0)?9\d{9}$/;


    if (!emailRegex.test(RegisterInput) && !phoneRegex.test(RegisterInput))
    {
        swal.fire({
            title: "عملیات ناموفق",
            text: "لطفا به صورت موبایل یا ایمیل وارد کنید",
            icon: `warning`
        })
    }
    
}