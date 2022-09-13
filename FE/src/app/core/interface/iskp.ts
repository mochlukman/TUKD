import { Ibend } from './ibend';
import { IDaftunit } from './idaftunit';
import { IStattrs } from './istattrs';

export interface ISkp {
  idskp : number;
  idunit : number;
  noskp : string;
  kdstatus : string;
  idbend : number;
  npwpd : string;
  idxkode : number;
  tglskp : null;
  penyetor : string;
  alamat : string;
  uraiskp : string;
  tgltempo : null;
  bunga : number;
  kenaikan : number;
  tglvalid : null;
  idbendNavigation : Ibend;
  idunitNavigation : IDaftunit;
  kdstatusNavigation: IStattrs
}