import { Component, Input, OnInit } from '@angular/core';
import { Document } from '../models/document';

@Component({
  selector: 'app-results',
  templateUrl: './results.component.html',
  styleUrls: ['./results.component.scss']
})
export class ResultsComponent implements OnInit {

  @Input()
  public documents: Document[] = []

  // public documents: Document[] = [
  //   {
  //     id: '1',
  //     content: `Lorem ipsum dolor sit amet, consectetur adipiscing elit. Morbi nunc
  //     nisl, dapibus eu tristique at, semper id velit.Integer velit nunc,
  //     aliquet at lacus id, auctor consectetur dolor.Quisque ut rutrum velit.
  //     Vivamus in aliquam diam.Duis placerat tellus eget laoreet maximus.
  //     Quisque volutpat, neque vitae euismod lobortis, purus tellus condimentum
  //     enim, quis luctus metus erat eu massa.Vivamus eget magna ante.
  //     Vestibulum pretium lacus vitae justo sodales, eu ultrices ante posuere.
  //     Fusce tristique quam arcu.Aliquam tempor nisl eros, laoreet egestas
  //     massa condimentum id.Donec fringilla metus in nisl porttitor, suscipit
  //     sollicitudin nunc mattis.Curabitur pellentesque sagittis enim quis
  //     ultricies.`
  //   },
  //   {
  //     id: '3',
  //     content: `Lorem ipsum dolor sit amet, consectetur adipiscing elit. Morbi nunc
  //     nisl, dapibus eu tristique at, semper id velit.Integer velit nunc,
  //     aliquet at lacus id, auctor consectetur dolor.Quisque ut rutrum velit.
  //     Vivamus in aliquam diam.Duis placerat tellus eget laoreet maximus.
  //     Quisque volutpat, neque vitae euismod lobortis, purus tellus condimentum enim, quis luctus metus erat eu massa.Vivamus eget magna ante.
  //     Vestibulum pretium lacus vitae justo sodales, eu ultrices ante posuere.
  //     Fusce tristique quam arcu.Aliquam tempor nisl eros, laoreet egestasmassa condimentum id.Donec fringilla metus in nisl porttitor, suscipitsollicitudin nunc mattis.Curabitur pellentesque sagittis enim quis
  //     ultricies.`
  //   }
  // ];

  constructor() { }

  ngOnInit(): void {
  }

}
