const searchClient = algoliasearch('UD04GSBJM1', 'YOUR_SEARCH_ONLY_API_KEY');

const search = instantsearch({
    indexName: 'Products',
    numberLocale: 'tr',
    searchClient,
    searchFunction(helper) {
        var searchResultsProduct = $('#hits');
        var searchResultsCustomer = $('#hits2');
        var searchResultsCategory = $('#hits3');
        var pagination = $('#pagination');
        if (helper.state.query) {
            helper.search();
            searchResultsCategory.show();
            searchResultsCustomer.show();
            searchResultsProduct.show();
            pagination.show();
        } else {
            searchResultsCategory.hide();
            searchResultsCustomer.hide();
            searchResultsProduct.hide();
            pagination.hide();
        }
    }
});

search.addWidgets([
    instantsearch.widgets.configure({
        distinct: true,
        clickAnalytics: true,
        enablePersonalization: true,
    }),
    instantsearch.widgets.searchBox({
        container: '#searchbox',
        autofocus: true,
        showLoadingIndicator: true,
        queryHook(query, search) {
            search(query);
        }
    }),
    instantsearch.widgets.hits({
        container: "#hits",
        hitsPerPage: 8,
        templates: {
            item: '<h4>{{#helpers.highlight}}{ "attribute": "Name" }{{/helpers.highlight}}</h4> <p>{{ Price }}₺</p>',
            empty: ''

        }
    }),
    instantsearch.widgets.pagination({
        container: "#pagination"
    }),
    instantsearch.widgets
        .index({ indexName: 'Customers' })
        .addWidgets([
            instantsearch.widgets.hits({
                container: '#hits2',
                hitsPerPage: 8,
                templates: {
                    item: '<p>{{#helpers.highlight}}{ "attribute": "Firstname" }{{/helpers.highlight}} {{#helpers.highlight}}{ "attribute": "Lastname" }{{/helpers.highlight}}</p>',
                    empty:''
                }
            }),
            instantsearch.widgets.pagination({
                container: "#pagination"
            })
        ]),
instantsearch.widgets
    .index({ indexName: 'Categories' })
    .addWidgets([
        instantsearch.widgets.hits({
            container: '#hits3',
            hitsPerPage: 8,
            templates: {
                item: '<p>{{#helpers.highlight}}{ "attribute": "CategoryName" }{{/helpers.highlight}}</p>',
                empty: ''

            }
        }),
        instantsearch.widgets.pagination({
            container: "#pagination"
        })
    ])
]);

search.start();