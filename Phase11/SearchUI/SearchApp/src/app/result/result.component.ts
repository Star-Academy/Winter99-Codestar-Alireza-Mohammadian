import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { Document } from './models/document'
import { DocumentsService } from './service/documents.service';

@Component({
  selector: 'app-result',
  templateUrl: './result.component.html',
  styleUrls: ['./result.component.scss']
})
export class ResultComponent implements OnInit {

  public query = '';
  public documents : Document[] = [];

  constructor(private route: ActivatedRoute,
              private service: DocumentsService) { }

  async ngOnInit(): Promise<void> {
    this.route.queryParamMap.subscribe((params) => {
      this.query = params.get('q')!
    })
    this.documents = await this.service.getDocuments(this.query);
    console.log(this.documents);
    
  }

  // public getDocuments() : Document[] {
  //   return []
  // }

}
