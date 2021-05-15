import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Document } from '../models/document';

@Injectable()
export class DocumentsService {

  constructor(private http: HttpClient) { }

  public async getDocuments(query: string): Promise<Document[]> {
    let ids = await this.http.get<string[]>("http://localhost:5000/SearchEngine/",
      { params: { "input": query } })
      .toPromise()
    let results : Document[] = []
    for (const x of ids) {
      await this.http.get<Document>(`http://localhost:5000/SearchEngine/${x}`)
        .toPromise()
        .then(data => results.push(data))
    }
    return results
  }
}
