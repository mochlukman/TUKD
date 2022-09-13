using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TUKD.API.Interface;
using TUKD.API.Models;

namespace TUKD.API.Repository
{
    public class Uow : IUow
    {
        private readonly TukdContext _tukdContext;
        private readonly IMapper _mapper;
        public IAuthRepo AuthRepo { get; set; }
        public IWebgroupRepo WebgroupRepo { get; set; }
        public ITahunRepo TahunRepo { get; set; }
        public IWebuserRepo WebuserRepo { get; set; }
        public IWebotorRepo WebotorRepo { get; set; }
        public IWebroleRepo WebroleRepo { get; set; }
        public IMenuRepo MenuRepo { get; set; }
        public IDafturusRepo DafturusRepo { get; set; }
        public IDaftunitRepo DaftunitRepo { get; set; }
        public IRkabRepo RkabRepo { get; set; }
        public IRkadetbRepo RkadetbRepo { get; set; }
        public IRkadanabRepo RkadanabRepo { get; set; }
        public IRkadRepo RkadRepo { get; set; }
        public IRkadetdRepo RkadetdRepo { get; set; }
        public IRkadanadRepo RkadanadRepo { get; set; }
        public IRkarRepo RkarRepo { get; set; }
        public IRkadetrRepo RkadetrRepo { get; set; }
        public IRkadanarRepo RkadanarRepo { get; set; }
        public IDpaRepo DpaRepo { get; set; }
        public IDpabRepo DpabRepo { get; set; }
        public IDpablnbRepo DpablnbRepo { get; set; }
        public IDpadetbRepo DpadetbRepo { get; set; }
        public IDpadanabRepo DpadanabRepo { get; set; }
        public IDpadRepo DpadRepo { get; set; }
        public IDpablndRepo DpablndRepo { get; set; }
        public IDpadetdRepo DpadetdRepo { get; set; }
        public IDpadanadRepo DpadanadRepo { get; set; }
        public IDparRepo DparRepo { get; set; }
        public IDpablnrRepo DpablnrRepo { get; set; }
        public IDpadetrRepo DpadetrRepo { get; set; }
        public IDpadanarRepo DpadanarRepo { get; set; }
        public IDaftrekeningRepo DaftrekeningRepo { get; set; }
        public IMkegiatanRepo MkegiatanRepo { get; set; }
        public IJdanaRepo JdanaRepo { get; set; }
        public IStdhargaRepo StdhargaRepo { get; set; }
        public IKegunitRepo KegunitRepo { get; set; }
        public IMpgrmRepo MpgrmRepo { get; set; }
        public IUrusanunitRepo UrusanunitRepo { get; set; }
        public IPgrmunitRepo PgrmunitRepo { get; set; }
        public IBulanRepo BulanRepo { get; set; }
        public IJsatuanRepo JsatuanRepo { get; set; }
        public ISpdRepo SpdRepo { get; set; }
        public ISpddetbRepo SpddetbRepo { get; set; }
        public ISpddetrRepo SpddetrRepo { get; set; }
        public IDaftbankRepo DaftbankRepo { get; set; }
        public IJusahaRepo JusahaRepo { get; set; }
        public IDaftphk3Repo Daftphk3Repo { get; set; }
        public IJbankRepo JbankRepo { get; set; }
        public IKontrakRepo KontrakRepo { get; set; }
        public IKontrakdetrRepo KontrakdetrRepo { get; set; }
        public IStattrsRepo StattrsRepo { get; set; }
        public ITagihanRepo TagihanRepo { get; set; }
        public ITagihandetRepo TagihandetRepo { get; set; }
        public IJtermorlunRepo JtermorlunRepo { get; set; }
        public ISifatkegRepo SifatkegRepo { get; set; }
        public IPegawaiRepo PegawaiRepo { get; set; }
        public IDpaprogramRepo DpaprogramRepo { get; set; }
        public IDpakegiatanRepo DpakegiatanRepo { get; set; }
        public IJabttdRepo JabttdRepo { get; set; }
        public IDaftdokRepo DaftdokRepo { get; set; }
        public IPemdaRepo PemdaRepo { get; set; }
        public IGolonganRepo GolonganRepo { get; set; }
        public IJbendRepo JbendRepo { get; set; }
        public IBendRepo BendRepo { get; set; }
        public ITahapRepo TahapRepo { get; set; }
        public IPaguskpdRepo PaguskpdRepo { get; set; }
        public IBkbkasRepo BkbkasRepo { get; set; }
        public IStrurekRepo StrurekRepo { get; set; }
        public ISppRepo SppRepo { get; set; }
        public IZkodeRepo ZkodeRepo { get; set; }
        public ISppdetbRepo SppdetbRepo { get; set; }
        public IJtrnlkasRepo JtrnlkasRepo { get; set; }
        public IJkegRepo JkegRepo { get; set; }
        public ISppdetrRepo SppdetrRepo { get; set; }
        public ISpptagRepo SpptagRepo { get; set; }
        public IAdendumRepo AdendumRepo { get; set; }
        public ISpjRepo SpjRepo { get; set; }
        public ISpjsppRepo SpjsppRepo { get; set; }
        public IBpkspjRepo BpkspjRepo { get; set; }
        public IBpkdetrRepo BpkdetrRepo { get; set; }
        public ISpmRepo SpmRepo { get; set; }
        public ISpmdetdRepo SpmdetdRepo { get; set; }
        public ISpmdetbRepo SpmdetbRepo { get; set; }
        public IJnspajakRepo JnspajakRepo { get; set; }
        public IPajakRepo PajakRepo { get; set; }
        public ISppdetrpRepo SppdetrpRepo { get; set; }
        public ISp2dRepo Sp2dRepo { get; set; }
        public IBpkRepo BpkRepo { get; set; }
        public IJbayarRepo JbayarRepo { get; set; }
        public IJtransferRepo JtransferRepo { get; set; }
        public ISp2dbpkRepo Sp2dbpkRepo { get; set; }
        public IBkbankRepo BkbankRepo { get; set; }
        public IBkbankdetRepo BkbankdetRepo { get; set; }
        public ITbpdetbRepo TbpdetbRepo { get; set; }
        public ITbpdetdRepo TbpdetdRepo { get; set; }
        public ITbpdetrRepo TbpdetrRepo { get; set; }
        public ITbpdettRepo TbpdettRepo { get; set; }
        public ITbpdettkegRepo TbpdettkegRepo { get; set; }
        public ITbpldetkegRepo TbpldetkegRepo { get; set; }
        public ITbpldetRepo TbpldetRepo { get; set; }
        public ITbplRepo TbplRepo { get; set; }
        public ITbpRepo TbpRepo { get; set; }
        public IBkubankRepo BkubankRepo { get; set; }
        public IBkubpkRepo BkubpkRepo { get; set; }
        public IBkudRepo BkudRepo { get; set; }
        public IBkukRepo BkukRepo { get; set; }
        public IBkupajakRepo BkupajakRepo { get; set; }
        public IBkusp2dRepo Bkusp2DRepo { get; set; }
        public IBkustsRepo BkustsRepo { get; set; }
        public IBkutbpRepo BkutbpRepo { get; set; }
        public IBpkpajakRepo BpkpajakRepo { get; set; }
        public IPanjarRepo PanjarRepo { get; set; }
        public IPanjardetRepo PanjardetRepo { get; set; }
        public IBkupanjarRepo BkupanjarRepo { get; set; }
        public IBkuRepo BkuRepo { get; set; }
        public IBkpajakRepo BkpajakRepo { get; set; }
        public IBkpajakdetstrRepo BkpajakdetstrRepo { get; set; }
        public ISkpRepo SkpRepo { get; set; }
        public ISkpdetRepo SkpdetRepo { get; set; }
        public IStsRepo StsRepo { get; set; }
        public IStsdetbRepo StsdetbRepo { get; set; }
        public IStsdetdRepo StsdetdRepo { get; set; }
        public IStsdetrRepo StsdetrRepo { get; set; }
        public IStsdettRepo StsdettRepo { get; set; }
        public ITbpstsRepo TbpstsRepo { get; set; }
        public ISkpstsRepo SkpstsRepo { get; set; }
        public IJbmRepo JbmRepo { get; set; }
        public IBktmemRepo BktmemRepo { get; set; }
        public IBktmemdetbRepo BktmemdetbRepo { get; set; }
        public IBktmemdetdRepo BktmemdetdRepo { get; set; }
        public IBktmemdetrRepo BktmemdetrRepo { get; set; }
        public IBktmemdetnRepo BktmemdetnRepo { get; set; }
        public IJnsakunRepo JnsakunRepo { get; set; }
        public IJurnalRepo JurnalRepo { get; set; }
        public IPrognosisbRepo PrognosisbRepo { get; set; }
        public IPrognosisdRepo PrognosisdRepo { get; set; }
        public IPrognosisrRepo PrognosisrRepo { get; set; }
        public ISaldoawallraRepo SaldoawallraRepo { get; set; }
        public ISaldoawalnrcRepo SaldoawalnrcRepo { get; set; }
        public ISaldoawalloRepo SaldoawalloRepo { get; set; }
        public IJnslakRepo JnslakRepo { get; set; }
        public IDaftreklakRepo DaftreklakRepo { get; set; }
        public ILpjRepo LpjRepo { get; set; }
        public ISpjlpjRepo SpjlpjRepo { get; set; }
        public ISpjtrRepo SpjtrRepo { get; set; }
        public IBkutbpspjtrRepo BkutbpspjtrRepo { get; set; }
        public IBkustsspjtrRepo BkustsspjtrRepo { get; set; }
        public IJbkasRepo JbkasRepo { get; set; }
        public IDpRepo DpRepo { get; set; }
        public IDpdetRepo DpdetRepo { get; set; }
        public IJkinkegRepo JkinkegRepo { get; set; }
        public IKinkegRepo KinkegRepo { get; set; }
        public IKegsbdanaRepo KegsbdanaRepo { get; set; }
        public IRkatapddRepo RkatapddRepo { get; set; }
        public IRkatapdbRepo RkatapdbRepo { get; set; }
        public IRkatapdrRepo RkatapdrRepo { get; set; }
        public IRkatapddetbRepo RkatapddetbRepo { get; set; }
        public IRkatapddetdRepo RkatapddetdRepo { get; set; }
        public IRkatapddetrRepo RkatapddetrRepo { get; set; }
        public IRkasahRepo RkasahRepo { get; set; }
        public IWebsetRepo WebsetRepo { get; set; }
        public ICheckdokRepo CheckdokRepo { get; set; }
        public ISppcheckdokRepo SppcheckdokRepo { get; set; }
        public IStruunitRepo StruunitRepo { get; set; }
        public IJrekRepo JrekRepo { get; set; }
        public IBpkpajakdetRepo BpkpajakdetRepo { get; set; }
        public IBpkpajakstrRepo BpkpajakstrRepo { get; set; }
        public IBpkpajakstrdetRepo BpkpajakstrdetRepo { get; set; }
        public ISp2dcheckdokRepo Sp2DcheckdokRepo { get; set; }
        public ISp2dNtpnRepo Sp2dNtpnRepo { get; set; }
        public ISkptbpRepo SkptbpRepo { get; set; }
        public Uow(IMapper mapper, TukdContext tukdContext)
        {
            _mapper = mapper;
            _tukdContext = tukdContext;
            AuthRepo = new AuthRepo(_tukdContext);
            WebgroupRepo = new WebgroupRexpo(_tukdContext);
            TahunRepo = new TahunRepo(_tukdContext);
            WebuserRepo = new WebuserRepo(_tukdContext);
            WebotorRepo = new WebotorRepo(_tukdContext);
            WebroleRepo = new WebroleRepo(_tukdContext);
            MenuRepo = new MenuRepo(_tukdContext, _mapper);
            DafturusRepo = new DafturusRepo(_tukdContext);
            DaftunitRepo = new DaftunitRepo(_tukdContext);
            RkabRepo = new RkabRepo(_tukdContext);
            RkadanabRepo = new RkadanabRepo(_tukdContext);
            RkadetbRepo = new RkadetbRepo(_tukdContext);
            RkadRepo = new RkadRepo(_tukdContext);
            RkadanadRepo = new RkadanadRepo(_tukdContext);
            RkadetdRepo = new RkadetdRepo(_tukdContext);
            RkarRepo = new RkarRepo(_tukdContext);
            RkadetrRepo = new RkadetrRepo(_tukdContext);
            RkadanarRepo = new RkadanarRepo(_tukdContext);
            DpaRepo = new DpaRepo(_tukdContext);
            DpabRepo = new DpabRepo(_tukdContext);
            DpablnbRepo = new DpablnbRepo(_tukdContext);
            DpadetbRepo = new DpadetbRepo(_tukdContext);
            DpadanabRepo = new DpadanabRepo(_tukdContext);
            DpadRepo = new DpadRepo(_tukdContext);
            DpablndRepo = new DpablndRepo(_tukdContext);
            DpadetdRepo = new DpadetdRepo(_tukdContext);
            DpadanadRepo = new DpadanadRepo(_tukdContext);
            DparRepo = new DparRepo(_tukdContext);
            DpablnrRepo = new DpablnrRepo(_tukdContext);
            DpadetrRepo = new DpadetrRepo(_tukdContext);
            DpadanarRepo = new DpadanarRepo(_tukdContext);
            DaftrekeningRepo = new DaftrekeningRepo(_tukdContext);
            MkegiatanRepo = new MKegiatanRepo(_tukdContext);
            JdanaRepo = new JdanaRepo(_tukdContext);
            StdhargaRepo = new StdhargaRepo(_tukdContext);
            KegunitRepo = new KegunitRepo(_tukdContext);
            MpgrmRepo = new MpgrmRepo(_tukdContext);
            UrusanunitRepo = new UrusanunitRepo(_tukdContext);
            PgrmunitRepo = new PgrmunitRepo(_tukdContext);
            BulanRepo = new BulanRepo(_tukdContext);
            JsatuanRepo = new JsatuanRepo(_tukdContext);
            SpdRepo = new SpdRepo(_tukdContext);
            SpddetbRepo = new SpddetbRepo(_tukdContext);
            SpddetrRepo = new SpddetrRepo(_tukdContext);
            JusahaRepo = new JusahaRepo(_tukdContext);
            DaftbankRepo = new DaftbankRepo(_tukdContext);
            Daftphk3Repo = new Daftphk3Repo(_tukdContext);
            JbankRepo = new JbankRepo(_tukdContext);
            KontrakRepo = new KontrakRepo(_tukdContext);
            KontrakdetrRepo = new KontrakdetrRepo(_tukdContext);
            StattrsRepo = new StattrsRepo(_tukdContext);
            TagihanRepo = new TagihanRepo(_tukdContext);
            TagihandetRepo = new TagihandetRepo(_tukdContext);
            JtermorlunRepo = new JtermorlunRepo(_tukdContext);
            SifatkegRepo = new SifatkegRepo(_tukdContext);
            PegawaiRepo = new PegawaiRepo(_tukdContext);
            DpaprogramRepo = new DpaprogramRepo(_tukdContext);
            DpakegiatanRepo = new DpakegiatanRepo(_tukdContext);
            DaftdokRepo = new DaftdokRepo(_tukdContext);
            JabttdRepo = new JabttdRepo(_tukdContext);
            PemdaRepo = new PemdaRepo(_tukdContext);
            GolonganRepo = new GolonganRepo(_tukdContext);
            JbendRepo = new JbendRepo(_tukdContext);
            BendRepo = new BendRepo(_tukdContext);
            TahapRepo = new TahapRepo(_tukdContext);
            PaguskpdRepo = new PaguskpdRepo(_tukdContext);
            BkbkasRepo = new BkbkasRepo(_tukdContext);
            StrurekRepo = new StrurekRepo(_tukdContext);
            SppRepo = new SppRepo(_tukdContext);
            ZkodeRepo = new ZkodeRepo(_tukdContext);
            SppdetbRepo = new SppdetbRepo(_tukdContext);
            JtrnlkasRepo = new JtrnlkasRepo(_tukdContext);
            JkegRepo = new JkegRepo(_tukdContext);
            SppdetrRepo = new SppdetrRepo(_tukdContext);
            AdendumRepo = new AdendumRepo(_tukdContext);
            SpjRepo = new SpjRepo(_tukdContext);
            SpjsppRepo = new SpjsppRepo(_tukdContext);
            BpkspjRepo = new BpkspjRepo(_tukdContext);
            SpmRepo = new SpmRepo(_tukdContext);
            SpmdetdRepo = new SpmdetdRepo(_tukdContext);
            SpmdetbRepo = new SpmdetbRepo(_tukdContext);
            JnspajakRepo = new JnspajakRepo(_tukdContext);
            PajakRepo = new PajakRepo(_tukdContext);
            SppdetrpRepo = new SppdetrpRepo(_tukdContext);
            Sp2dRepo = new Sp2dRepo(_tukdContext);
            BpkRepo = new BpkRepo(_tukdContext, _mapper);
            JbayarRepo = new JbayarRepo(_tukdContext);
            JtransferRepo = new JtransferRepo(_tukdContext);
            Sp2dbpkRepo = new Sp2dbpkRepo(_tukdContext);
            BpkdetrRepo = new BpkdetrRepo(_tukdContext);
            BkbankRepo = new BkbankRepo(_tukdContext);
            BkbankdetRepo = new BkbankdetRepo(_tukdContext);
            TbpdetbRepo = new TbpdetbRepo(_tukdContext);
            TbpdetdRepo = new TbpdetdRepo(_tukdContext);
            TbpdetrRepo = new TbpdetrRepo(_tukdContext);
            TbpdettRepo = new TbpdettRepo(_tukdContext);
            TbpdettkegRepo = new TbpdettkegRepo(_tukdContext);
            TbpRepo = new TbpRepo(_tukdContext);
            TbplRepo = new TbplRepo(_tukdContext);
            TbpldetRepo = new TbpldetRepo(_tukdContext);
            TbpldetkegRepo = new TbpldetkegRepo(_tukdContext);
            BkubankRepo = new BkubankRepo(_tukdContext);
            BkubpkRepo = new BkubpkRepo(_tukdContext);
            BkudRepo = new BkudRepo(_tukdContext);
            BkukRepo = new BkukRepo(_tukdContext);
            BkupajakRepo = new BkupajakRepo(_tukdContext);
            Bkusp2DRepo = new Bkusp2dRepo(_tukdContext);
            BkustsRepo = new BkustsRepo(_tukdContext);
            BkutbpRepo = new BkutbpRepo(_tukdContext);
            BpkpajakRepo = new BpkpajakRepo(_tukdContext);
            PanjarRepo = new PanjarRepo(_tukdContext);
            PanjardetRepo = new PanjardetRepo(_tukdContext);
            BkupanjarRepo = new BkupanjarRepo(_tukdContext);
            BkuRepo = new BkuRepo(_tukdContext);
            BkpajakRepo = new BkpajakRepo(_tukdContext);
            BkpajakdetstrRepo = new BkpajakdetstrRepo(_tukdContext);
            SkpRepo = new SkpRepo(_tukdContext);
            SkpdetRepo = new SkpdetRepo(_tukdContext);
            StsRepo = new StsRepo(_tukdContext); ;
            StsdetbRepo = new StsdetbRepo(_tukdContext);
            StsdetdRepo = new StsdetdRepo(_tukdContext);
            StsdetrRepo = new StsdetrRepo(_tukdContext);
            StsdettRepo = new StsdettRepo(_tukdContext);
            TbpstsRepo = new TbpstsRepo(_tukdContext);
            SkpstsRepo = new SkpstsRepo(_tukdContext);
            JbmRepo = new JbmRepo(_tukdContext);
            BktmemRepo = new BktmemRepo(_tukdContext);
            BktmemdetbRepo = new BktmemdetbRepo(_tukdContext);
            BktmemdetrRepo = new BktmemdetrRepo(_tukdContext);
            BktmemdetdRepo = new BktmemdetdRepo(_tukdContext);
            BktmemdetnRepo = new BktmemdetnRepo(_tukdContext);
            JnsakunRepo = new JnsakunRepo(_tukdContext);
            JurnalRepo = new JurnalRepo(_tukdContext);
            PrognosisbRepo = new PrognosisbRepo(_tukdContext);
            PrognosisdRepo = new PrognosisdRepo(_tukdContext);
            PrognosisrRepo = new PrognosisrRepo(_tukdContext);
            SaldoawallraRepo = new SaldoawallraRepo(_tukdContext);
            SaldoawalnrcRepo = new SaldoawalnrcRepo(_tukdContext);
            SaldoawalloRepo = new SaldoawalloRepo(_tukdContext);
            JnslakRepo = new JnslakRepo(_tukdContext);
            DaftreklakRepo = new DaftreklakRepo(_tukdContext);
            LpjRepo = new LpjRepo(_tukdContext);
            SpjlpjRepo = new SpjlpjRepo(_tukdContext);
            SpjtrRepo = new SpjtrRepo(_tukdContext);
            BkutbpspjtrRepo = new BkutbpspjtrRepo(_tukdContext);
            BkustsspjtrRepo = new BkustsspjtrRepo(_tukdContext);
            JbkasRepo = new JbkasRepo(_tukdContext);
            DpRepo = new DpRepo(_tukdContext);
            DpdetRepo = new DpdetRepo(_tukdContext);
            JkinkegRepo = new JkinkegRepo(_tukdContext);
            KinkegRepo = new KinkegRepo(_tukdContext);
            KegsbdanaRepo = new KegsbdanaRepo(_tukdContext);
            RkatapdbRepo = new RkatapdbRepo(_tukdContext);
            RkatapddRepo = new RkatapddRepo(_tukdContext);
            RkatapdrRepo = new RkatapdrRepo(_tukdContext);
            RkatapddetbRepo = new RkatapddetbRepo(_tukdContext);
            RkatapddetdRepo = new RkatapddetdRepo(_tukdContext);
            RkatapddetrRepo = new RkatapddetrRepo(_tukdContext);
            RkasahRepo = new RkasahRepo(_tukdContext);
            WebsetRepo = new WebsetRepo(_tukdContext);
            CheckdokRepo = new CheckdokRepo(_tukdContext);
            SppcheckdokRepo = new SppcheckdokRepo(_tukdContext);
            StruunitRepo = new StruunitRepo(_tukdContext);
            JrekRepo = new JrekRepo(_tukdContext);
            BpkpajakdetRepo = new BpkpajakdetRepo(_tukdContext);
            BpkpajakstrRepo = new BpkpajakstrRepo(_tukdContext);
            BpkpajakstrdetRepo = new BpkpajakstrdetRepo(_tukdContext);
            Sp2DcheckdokRepo = new Sp2dcheckdokRepo(_tukdContext);
            Sp2dNtpnRepo = new Sp2dNtpnRepo(_tukdContext);
            SkptbpRepo = new SkptbpRepo(_tukdContext);
        }
        public async Task<bool> Complete()
        {
            return await _tukdContext.SaveChangesAsync() > 0;
        }
    }
}
