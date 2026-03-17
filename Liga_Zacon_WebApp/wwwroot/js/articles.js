let currentPage = 0;
const size = 10;
let page = 0;
let searchTerm = '';

$(document).ready(function () {
    currentPage = document.getElementById('page').value;

    const pagination = document.querySelectorAll('.page-item');
    pagination[0].addEventListener('click', function () {
        if (page <= 0)
            return;

        page = page - 1;
        getArticles();
    });

    pagination[1].addEventListener('click', function () {
        page = page + 1;
        getArticles();
    });

    let timeout = null;
    const input = document.querySelector('#searchInput');
    if (input) {
        input.addEventListener('input', (e) => {
            searchTerm = e.target.value;
            page = 0;

            clearTimeout(timeout);
            timeout = setTimeout(async () => {
                getArticles();
            }, 400);
        });
    }

    const tagInput = document.querySelectorAll('.btn');
    if (tagInput) {
        tagInput.forEach(btn => {
            btn.addEventListener('click', (e) => {
                searchTerm = e.target.innerText;
                page = 0;
                getArticles();
            })
        });
    }

});

function getArticles() {
    const url = currentPage === '1'
        ? `/api/articles/paged?page=${page}&size=${size}&searchTerm=${searchTerm}`
        : `/api/articles/paged-by-tags?page=${page}&size=${size}&tag=${searchTerm}`;

    $.ajax({
        url: url,
        type: 'POST',
        success: function (data) {

            if (data.trim().length === 0) {
                page = page - 1;
                return;
            }

            const tbody = document.querySelector('#articlesTable tbody');
            tbody.innerHTML = data;
        },
        error: function (xhr, status, error) {
            // Code to handle errors
            console.error("An error occurred: " + error);
        }
    });
}
