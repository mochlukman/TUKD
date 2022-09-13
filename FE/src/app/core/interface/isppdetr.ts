import { IDaftrekening } from './idaftrekening';
import { IJtrnlkas } from './ijtrnlkas';
import { ISpp } from './ispp';

export interface ISppdetr {
  idsppdetr : number;
  idrek : number;
  idkeg : number;
  idspp : number;
  idnojetra : number;
  nilai : number;
  createdate : null;
  createby : string;
  updatedate : null;
  updateby : string;
  idnojetraNavigation : IJtrnlkas;
  idrekNavigation : IDaftrekening;
  idsppNavigation : ISpp;
  totspd: number;
  sisa: number;
}
export interface ISppdetrPostMultiKeg{
  idrek : number;
  idkeg : number;
  idspp : number;
  idnojetra : number;
  nilai : number;
}
export interface ISppdetrTreeRoot{
  data: ISppdetrData;
  children: ISppdetrTreeRoot[];
  expanded: boolean;
}
export interface ISppdetrData{
  rowid : string;
  level : string;
  kode : string;
  uraian : string;
  idsppdetr : number;
  idrek : number;
  idkeg : number;
  idspp : number;
  idnojetra : number;
  nilai : number;
  totspd : number;
  sisa :number;
}