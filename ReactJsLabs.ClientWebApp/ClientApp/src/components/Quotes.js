import * as React from "react";
import './Quotes.css';

export class Quotes extends React.Component {
    constructor(props){
        super(props);

        this.state = {
            quoteText: null,
            quoteBy: null
        };

        this.fetchSimpsonsQuotes();
    }

    fetchSimpsonsQuotes(){
        fetch("https://thesimpsonsquoteapi.glitch.me/quotes")
			.then(response => response.json())
			.then(data => {
				this.setState({
                    quoteText: data[0].quote,
                    quoteBy: data[0].character
                });
            });
    }

	render() {
		return (
			<div>
				<h1>Simpsons Quotes!</h1>
				<div className="quoteBlock">
					<blockquote>"{this.state.quoteText}"</blockquote>
					<label>~ {this.state.quoteBy}</label>
				</div>
			</div>
		);
	}
}
