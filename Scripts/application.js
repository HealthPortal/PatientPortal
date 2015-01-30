$(function () {

    var Entry = Backbone.Model.extend({});
    var EntryCollection = Backbone.Collection.extend({
        model: Entry,
        url: '/echo/json/'
    });

    var EntryView = Backbone.View.extend({

        render: function () {
            this.$el.html(this.model.get('title'));
            return this;
        }

    });

    var EntryListView = Backbone.View.extend({
        events: {
            'click button': 'addEntry'
        },
        initialize: function () {
            _.bindAll(this, 'render');

            this.listenTo(this.collection, 'reset', this.render);
            this.listenTo(this.collection, 'add', this.render);
        },
        render: function () {
            var els = [];
            this.collection.each(function (item) {
                els.push(new EntryView({ model: item }).render().el);
            });

            this.$el.empty();
            this.$el.append(els);
            this.$el.append('<button>New</button>');
            return this;
        },
        addEntry: function () {
            entries.add(new Entry({
                title: "New entry",
                text: "This entry was inserted after the view was rendered"
            }));
        }
    });

    var entries = new EntryCollection();
    var view = new EntryListView({
        collection: entries,
        el: '#entries'
    });
    view.render();

    entries.reset([
  { title: "Entry 1" },
  { title: "Entry 2" }
]);



});