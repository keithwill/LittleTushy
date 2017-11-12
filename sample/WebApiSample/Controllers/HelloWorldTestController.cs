using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using LittleTushy.Server;
using LittleTushy.Client;
using System.Diagnostics;
using System.Net.Http;

namespace WebApiSample.Controllers
{
    [Route("api/[controller]")]
    public class HelloWorldTestController : Controller
    {
        private readonly ServiceClient client;

        public HelloWorldTestController(ServiceClient client)
        {
            this.client = client;
        }

        [HttpGet]
        [Route("/LoremIpsum/{name}")]
        public async Task<string> LoremIpsum(string name)
        {
            return @"Lorem ipsum dolor sit amet, consectetur adipiscing elit. Suspendisse mi tellus, semper vitae auctor vel, sollicitudin quis sem. Maecenas ultricies vehicula accumsan. Praesent laoreet fringilla turpis. Aenean venenatis faucibus erat nec consectetur. Etiam rhoncus cursus tellus, rutrum molestie libero laoreet sed. Nam turpis neque, tempor id eleifend id, molestie in mi. Ut erat ante, tempus eu ullamcorper eu, porta in purus. Praesent pretium ex tincidunt purus tempor semper. Quisque eros lectus, varius vel est vitae, hendrerit auctor magna. Vivamus hendrerit tellus a nisl pulvinar maximus. Suspendisse feugiat magna felis, a volutpat odio imperdiet nec. Integer vel orci quis enim ultrices dapibus. Pellentesque tincidunt sem orci, vitae cursus augue faucibus eget. In accumsan sem ac nisi tincidunt dignissim.

Praesent pharetra consectetur risus nec facilisis. Sed mollis tincidunt fermentum. Integer id congue lectus, et pharetra est. Mauris luctus malesuada tortor eget bibendum. Duis quis placerat ante, eget cursus neque. Fusce lacinia sapien lectus, faucibus pharetra augue cursus at. Ut turpis felis, maximus tempor sollicitudin ac, sodales ac mi. Quisque auctor volutpat quam at laoreet. Morbi vel libero bibendum, elementum arcu at, egestas urna. Vivamus pellentesque, diam a placerat lacinia, urna nibh faucibus nisi, eu bibendum dui odio sed sapien. Interdum et malesuada fames ac ante ipsum primis in faucibus. Integer gravida facilisis pharetra. Aliquam fringilla nisi sed egestas bibendum. Donec eget porta tortor, vel porta tortor. Morbi non laoreet ligula. Mauris sed consectetur sapien.

Maecenas accumsan aliquet massa, sed efficitur ligula consequat non. Maecenas venenatis diam sed justo fermentum, eu interdum purus finibus. Mauris vehicula eleifend nibh quis porttitor. Nullam egestas metus at magna pellentesque pharetra. Donec commodo, felis eu lacinia accumsan, augue mauris euismod ligula, sit amet efficitur augue turpis ac risus. In ut hendrerit ipsum. Quisque elementum est eros, id tincidunt eros placerat in. Duis eu pharetra risus. Phasellus imperdiet, diam sit amet tempor tempus, felis mi consequat lacus, eu sollicitudin orci massa et quam. Etiam dignissim elit felis, at ullamcorper magna tincidunt ut. Ut arcu mauris, consectetur ac venenatis eu, egestas vitae risus. Nullam rhoncus auctor ligula id auctor.

Cras aliquam libero ut lectus aliquet, nec iaculis odio hendrerit. Interdum et malesuada fames ac ante ipsum primis in faucibus. Nam et sapien et justo ornare pellentesque a euismod libero. Nulla lacinia urna id enim rhoncus luctus. Nam fringilla vehicula ligula at tincidunt. Curabitur nisl libero, consectetur at finibus eu, posuere sit amet ipsum. Phasellus metus orci, eleifend quis auctor non, porta vitae nisi. Duis dignissim id lorem et cursus. Nam tincidunt orci sagittis urna bibendum, non egestas justo cursus. Suspendisse porttitor iaculis interdum. Maecenas sodales lacus lacus, vitae tempor felis congue eget. Pellentesque habitant morbi tristique senectus et netus et malesuada fames ac turpis egestas. Etiam sapien purus, volutpat at cursus ut, luctus eu felis. Quisque cursus dignissim lacus eu egestas. Maecenas non consequat dolor.

Cras vel massa ut erat auctor pharetra. In hac habitasse platea dictumst. Proin nec sollicitudin sem. Duis ultricies fringilla purus, eget consequat sapien tincidunt quis. Praesent leo leo, dictum sit amet fringilla ac, fringilla eget velit. Phasellus quis mi tempus, cursus lectus pellentesque, tempor velit. Duis et metus ex. Ut ultrices scelerisque justo, eu convallis purus efficitur a. Aliquam in dui vulputate velit sagittis iaculis in suscipit sapien.

Vivamus sed libero risus. Nam nec lectus nec turpis porttitor molestie. Nam quis purus id diam convallis pretium sit amet sit amet nisi. Nam molestie rutrum nisi. Nulla finibus eu metus a lacinia. Cras in porta magna. Ut interdum augue at nisi euismod posuere. Aenean bibendum diam at nibh pulvinar efficitur.

Aenean ligula massa, vestibulum vitae dolor hendrerit, tempus placerat diam. Phasellus volutpat turpis pharetra eros sollicitudin, interdum efficitur ex molestie. Quisque non aliquam nunc. Etiam euismod ultrices eros, non rhoncus turpis iaculis quis. Vestibulum nec orci vitae diam ultricies egestas ut viverra libero. Lorem ipsum dolor sit amet, consectetur adipiscing elit. Praesent tincidunt erat eu leo ullamcorper, quis aliquet tellus imperdiet. Mauris id leo quam.

Fusce ipsum ligula, lobortis placerat dapibus vel, venenatis in orci. Curabitur ornare aliquet pretium. Aenean ultrices risus quis sollicitudin sodales. Suspendisse a interdum diam. Fusce ipsum dui, gravida non lectus id, vulputate fermentum risus. Nullam eleifend ultrices mi sit amet congue. Etiam sollicitudin id sem ut eleifend. Aenean elementum ipsum sem, vitae varius magna porta non. Ut vitae ipsum ac turpis maximus bibendum. Donec accumsan, elit vitae vulputate rhoncus, nisi ex ullamcorper orci, vitae ultrices ante metus id mauris. Proin scelerisque lobortis volutpat.

Nullam non libero non est eleifend pulvinar at at massa. Class aptent taciti sociosqu ad litora torquent per conubia nostra, per inceptos himenaeos. Vestibulum congue odio id magna viverra, rutrum porta ante blandit. Nulla eu varius dolor, sit amet lobortis lorem. Etiam id nisl tellus. Aenean aliquam quis augue eu pellentesque. Suspendisse posuere nec nisl at pellentesque. Curabitur eget purus ex. Proin vitae sodales felis, nec ultrices tellus.

Mauris vel vestibulum nulla, et sagittis enim. Class aptent taciti sociosqu ad litora torquent per conubia nostra, per inceptos himenaeos. Integer id vehicula magna. Proin ultricies mi feugiat nisl porta, a maximus lectus dignissim. Suspendisse eget sem semper, facilisis eros a, aliquet mi. Pellentesque habitant morbi tristique senectus et netus et malesuada fames ac turpis egestas. Nam egestas lacinia elit, ut ultrices tellus feugiat ut. Maecenas egestas aliquam sem, sed iaculis nisi sodales mattis. Aenean maximus molestie enim vitae iaculis. Duis vitae metus metus. Aliquam erat volutpat. Sed interdum, justo quis imperdiet tristique, felis leo pharetra elit, in dignissim risus risus vel odio. Vivamus luctus eget urna ut semper. Maecenas sodales eros eget luctus pharetra. Suspendisse nibh leo, faucibus ut nibh ac, placerat volutpat justo.

Pellentesque at luctus sem. Curabitur posuere ullamcorper facilisis. Curabitur tincidunt elementum nisl ac lobortis. Class aptent taciti sociosqu ad litora torquent per conubia nostra, per inceptos himenaeos. Suspendisse dolor enim, pharetra nec ante id, elementum rhoncus mi. In leo purus, rhoncus eu eleifend vel, tristique a ante. Donec non rhoncus lectus. Duis tristique sed urna viverra fermentum. Quisque vel velit dui. Nam nunc augue, commodo eget aliquam sit amet, suscipit id nunc. Nulla tortor urna, pellentesque sit amet massa in, ultricies consequat ipsum.

Curabitur sit amet ante malesuada, facilisis leo quis, mollis purus. Donec quis purus rutrum, tempor libero at, molestie mauris. Class aptent taciti sociosqu ad litora torquent per conubia nostra, per inceptos himenaeos. Curabitur eleifend diam a tincidunt euismod. Aliquam a leo in mi fermentum tincidunt. Nam sodales nisl in neque porta porttitor. Aenean porta justo ante, sit amet blandit metus finibus quis. Maecenas quis justo ut nisl hendrerit interdum. Integer eu lacinia urna. Suspendisse tempus sollicitudin ultricies. Phasellus vel erat faucibus, gravida nunc at, tincidunt elit. Cras venenatis leo felis, ac lacinia magna congue et. Duis sem ante, mollis in interdum nec, lacinia sed enim. Maecenas rhoncus felis id nisi lacinia tristique.

Nam sed purus urna. Aliquam maximus nulla ac blandit semper. Pellentesque vitae diam eget mi pellentesque posuere et nec nibh. Morbi id mollis nisi. Vestibulum sit amet scelerisque orci. Praesent consectetur leo sagittis suscipit venenatis. Sed eget tellus aliquam, fermentum urna at, pulvinar ligula. Ut a malesuada quam, et dignissim ex. Sed magna arcu, dictum in arcu vitae, scelerisque iaculis ipsum. Duis quis nulla hendrerit, consequat felis at, rhoncus urna. Cras cursus hendrerit volutpat. Integer consectetur dolor eget nisl varius laoreet. Donec in dignissim diam. Praesent nec tempus dui.

Nam vel nibh quis velit tristique vulputate a non risus. Phasellus scelerisque nec diam id dictum. Nullam urna lacus, laoreet quis nunc nec, lobortis elementum erat. Orci varius natoque penatibus et magnis dis parturient montes, nascetur ridiculus mus. Aliquam aliquam eros varius interdum ultrices. Praesent tristique, turpis dignissim fermentum viverra, leo lacus vehicula tortor, sed suscipit massa augue a est. Interdum et malesuada fames ac ante ipsum primis in faucibus. Integer vulputate commodo lorem non tincidunt. Pellentesque vestibulum ac velit eu rhoncus.

Nam lacus libero, facilisis in orci ut, tincidunt placerat sem. Maecenas nec nibh quam. Etiam tempus dui sit amet erat dignissim, vitae tristique sapien mattis. Aenean euismod velit lacus, et ornare nisl placerat ac. Nunc suscipit est id scelerisque cursus. Praesent in risus ultricies nisi consectetur laoreet. Maecenas pretium purus vitae nunc condimentum lobortis. Etiam dictum erat diam, non maximus velit dignissim non. Cras nunc eros, lobortis a tellus a, ullamcorper semper urna. Nulla facilisi.

Pellentesque efficitur sapien pretium accumsan lacinia. Vivamus nisi nisl, iaculis eu elit id, placerat vehicula ante. Ut bibendum tortor non dolor fermentum, vitae ullamcorper nisl tincidunt. Proin gravida lectus quis accumsan vestibulum. Phasellus porta non orci non varius. Sed sed purus at odio porta auctor a at tortor. Donec tincidunt lorem venenatis sem tempus volutpat.

Nullam vehicula, arcu a suscipit dictum, eros quam egestas elit, et tincidunt dui felis a diam. Fusce tincidunt ac felis id interdum. Nunc dui ante, imperdiet id lacus non, efficitur varius eros. Orci varius natoque penatibus et magnis dis parturient montes, nascetur ridiculus mus. Donec augue mi, luctus sed leo eget, tempor mattis tellus. Praesent eget ullamcorper libero. Praesent luctus sapien vitae sapien ultrices, at tincidunt velit iaculis. Aenean vehicula facilisis eros a consectetur. Donec iaculis mattis nisi ac faucibus. Mauris vitae scelerisque ligula. Praesent sollicitudin dolor velit, sodales semper turpis tincidunt sit amet.

Praesent sit amet ultricies velit, et dignissim est. Quisque luctus nulla vitae massa ullamcorper scelerisque. Aenean ultricies nulla id rhoncus porttitor. Maecenas maximus, magna nec hendrerit bibendum, lorem purus scelerisque nunc, vel laoreet lacus arcu non magna. Aliquam et accumsan neque. Praesent pellentesque elit ut nunc varius venenatis. Nullam condimentum venenatis facilisis. Quisque rutrum malesuada mauris at feugiat. Ut pretium ex interdum eleifend ultricies.

Nam quis magna ultrices, tincidunt ante vel, euismod diam. Praesent fermentum ligula dolor, at cursus nibh lobortis aliquet. Phasellus scelerisque, nisi quis ornare tincidunt, tellus lorem semper lectus, a ultricies ante nibh ut est. Sed maximus viverra sagittis. Nullam ut scelerisque eros. Quisque ac arcu ac leo aliquet tristique vitae a erat. Fusce placerat, nibh eget dapibus dignissim, lorem urna dapibus leo, ut fringilla eros sapien a dui. Pellentesque et semper odio.

Nulla quis ligula a ante scelerisque pellentesque at vel purus. Maecenas porta auctor ultricies. Praesent hendrerit congue elit, quis pulvinar purus. Mauris suscipit metus in urna tincidunt lobortis. Integer sit amet tortor at libero tempor vestibulum vel sed arcu. Fusce laoreet lacinia aliquam. Nulla fringilla neque arcu, ut faucibus tellus porttitor sit amet. Maecenas sed consectetur diam. In porttitor, neque vitae finibus rhoncus, lacus odio tincidunt eros, vitae ultricies erat nulla vel augue.

Nulla lorem lectus, facilisis eu arcu in, consequat pharetra metus. Sed venenatis faucibus fermentum. Donec rhoncus lobortis metus, eu aliquet nisl sodales et. Aliquam suscipit dictum eros, vel mattis massa bibendum eget. Integer nec erat ut nibh feugiat tincidunt. Donec a risus massa. Vivamus a augue vel massa ornare aliquet.

Donec porttitor commodo libero a convallis. Duis a tincidunt massa. Aliquam ac auctor turpis. Fusce eget auctor odio, non euismod nisi. Praesent elit lectus, varius nec pretium et, varius non nisi. Vestibulum placerat arcu sit amet lorem hendrerit pretium. Nullam non ultrices purus. In quis est neque. Curabitur hendrerit rhoncus venenatis.

Morbi eu ligula dapibus augue placerat rutrum vitae sit amet turpis. In facilisis facilisis mauris, nec luctus libero luctus eget. Suspendisse venenatis quis quam eget tincidunt. Donec nec massa turpis. Duis non laoreet nulla. Proin molestie condimentum erat ut dapibus. Aenean vitae ultricies augue.

Mauris vel eros ullamcorper, semper tellus at, elementum ante. Phasellus tincidunt felis non dapibus tristique. Phasellus ac orci consequat mauris porta pellentesque et dictum erat. Etiam lacus augue, rhoncus in felis nec, semper efficitur lectus. Nam id turpis eu tortor aliquet egestas. Sed id augue vulputate, convallis lacus et, mattis leo. Vivamus lacinia tellus sit amet tortor pellentesque interdum. In hac habitasse platea dictumst. Duis eget est id nisi suscipit vestibulum sit amet quis orci. Mauris non turpis sollicitudin, vehicula sapien placerat, euismod dolor. Maecenas aliquam metus non augue laoreet posuere. In tristique, tortor et molestie interdum, leo purus consectetur ex, ac rhoncus magna purus a est. Maecenas lobortis, felis eu condimentum euismod, magna tellus lacinia arcu, in egestas neque nisl ut tellus.

Nullam gravida mauris nec ipsum volutpat tristique. Aliquam varius egestas justo vel cursus. Donec congue erat lorem, non venenatis justo molestie ac. Aliquam eget diam molestie, congue mi at, feugiat eros. Maecenas quis velit egestas, accumsan diam sed, cursus quam. Fusce augue ante, aliquam quis odio in, maximus convallis dui. Phasellus purus arcu, luctus vel ligula quis, suscipit suscipit nunc. Maecenas leo augue, ultrices sed metus quis, volutpat feugiat eros. Aliquam tristique tempor dignissim. Praesent pretium purus vel elit posuere feugiat. Etiam ut velit erat. Donec ut mi a metus hendrerit sodales id sit amet neque. Duis euismod nunc dui, id venenatis mi finibus vel.

Sed purus metus, aliquam at ante molestie, rutrum auctor tortor. Donec orci dui, lobortis eget nisi quis, rhoncus pellentesque orci. Donec a est rhoncus, efficitur dolor vel, sodales erat. Aliquam tempus justo non purus venenatis, nec commodo ante posuere. Nullam a purus vitae odio accumsan sagittis a a justo. Sed placerat id tortor ut fermentum. In vestibulum turpis et dui dignissim tincidunt.

Phasellus metus felis, sagittis sollicitudin augue vel, vehicula pulvinar dui. Suspendisse ligula augue, dignissim eget ante in, auctor varius metus. Pellentesque laoreet risus metus, quis condimentum ex luctus lacinia. Pellentesque id diam et lacus porttitor tristique in id diam. Nunc massa nisl, ornare dignissim augue quis, eleifend luctus leo. Suspendisse sed pellentesque justo, ut volutpat eros. Fusce tincidunt fringilla commodo.

Etiam ac nulla semper, consequat ligula eu, pellentesque turpis. In ut ultricies libero. Aliquam vitae lacus et diam bibendum ornare eget in nisi. Sed porta tincidunt libero id rutrum. Praesent a nibh metus. Etiam suscipit aliquet diam et egestas. Praesent ut nunc quis arcu tristique vestibulum rhoncus sed justo. Maecenas molestie sit amet tellus eget lacinia. Vivamus justo nisi, scelerisque ut elit sit amet, suscipit feugiat lectus. Vestibulum eu ante risus. In non lectus eget massa euismod accumsan congue quis justo. Praesent neque diam, rhoncus et dolor eget, fringilla eleifend metus. Nulla luctus blandit tellus, in iaculis ante sodales in. Sed faucibus elit vel eleifend sollicitudin. Donec in sapien ipsum.

Vestibulum turpis lacus, tempus eget rutrum vitae, tincidunt sit amet ipsum. Vestibulum finibus magna et lobortis consequat. Nullam laoreet, magna ac dapibus auctor, justo eros tristique eros, vel condimentum odio felis non sapien. Mauris sed sagittis lorem. Praesent at elit eu est auctor finibus sit amet vel sapien. Quisque venenatis eu risus sed fermentum. Mauris lobortis neque nibh, ac tempus mi porttitor varius. Morbi elementum lectus eu eros ultricies posuere. Nam tempor sem eget risus cursus lacinia. Nunc tristique magna eget diam laoreet, nec scelerisque massa pellentesque. Donec vestibulum elementum eleifend. Morbi ac ante non ante tincidunt imperdiet. Praesent rutrum lectus ut tincidunt interdum. Donec porta finibus leo vel dignissim.

Maecenas consectetur nisi eu diam maximus sollicitudin. Pellentesque cursus metus ut dolor varius tempor. Integer non nisi tortor. Duis ultricies ornare enim, nec consequat arcu tempor ac. Morbi nisi diam, feugiat eget leo sit amet, mattis auctor libero. Donec ac aliquet lorem. In magna odio, malesuada et magna vel, sollicitudin varius neque. Integer et lobortis mauris. Nam diam mi, finibus ac ullamcorper sed, dictum et dolor. Aenean varius lobortis arcu, et tristique nisl. Sed non pellentesque mi. Fusce eu urna consequat, molestie augue ac, rhoncus mi.

Quisque ac nulla ultricies, pharetra dui sed, molestie tellus. Mauris ultrices mi sit amet laoreet fringilla. Aenean finibus ornare est sed ultrices. Ut ultrices pulvinar commodo. Nulla magna sem, ullamcorper at neque sit amet, dapibus placerat mauris. Interdum et malesuada fames ac ante ipsum primis in faucibus. Duis viverra dictum orci. Donec quis metus leo. Vivamus vitae diam tellus. Pellentesque pretium massa sed eros dictum fringilla. Vivamus at nisi in sapien vehicula pulvinar nec sit amet sapien. Aliquam lacinia hendrerit ante, ac finibus erat laoreet sed. Ut lectus magna, fringilla quis convallis eu, lobortis eu orci. Aliquam lacinia felis non tellus laoreet, eu posuere mi tincidunt. Etiam sed posuere turpis.

Sed cursus convallis felis et efficitur. Curabitur pharetra rhoncus fermentum. Curabitur quis dictum ante, sed varius nisi. Etiam finibus interdum turpis, blandit pellentesque lorem blandit a. Duis ultrices, ligula et mollis malesuada, est enim fermentum elit, vitae euismod magna nunc tincidunt massa. Phasellus erat ipsum, laoreet a velit a, cursus semper metus. Nunc porttitor velit non velit gravida, in bibendum diam sagittis.

Aliquam vehicula in diam ut sodales. Proin mi nulla, dignissim sed pellentesque nec, facilisis vel odio. Maecenas feugiat ex a lacus varius feugiat ut vel metus. Donec sit amet mollis nulla, at lobortis mauris. Mauris semper enim nec aliquet blandit. Donec eget nulla erat. Phasellus eu imperdiet mauris, sed sollicitudin mauris. Quisque ullamcorper nisi odio, at gravida ex scelerisque ac. Donec sed libero sodales mauris sagittis sagittis. Maecenas est odio, eleifend quis nulla quis, imperdiet suscipit lacus.

Nulla congue, lectus vel faucibus dictum, mi lectus tempor dolor, tincidunt molestie nibh magna eget dolor. Mauris feugiat sagittis ex id pharetra. Vivamus congue risus diam, a iaculis lectus pulvinar a. Phasellus sagittis velit justo. Pellentesque volutpat, enim ac elementum volutpat, odio elit luctus ex, quis placerat enim magna porttitor dolor. Vivamus sit amet sem lacinia, condimentum nisi vitae, scelerisque sem. Nam sit amet velit at arcu tincidunt tristique. Sed vel sodales lorem, ac varius risus. Aenean maximus eros ut lacus interdum eleifend. Integer venenatis nulla in odio sodales viverra. Ut in tristique nisi. Vivamus ut vestibulum turpis, at ornare risus. Aliquam erat volutpat. Vivamus tempor vitae nulla vel mattis.

Etiam ut aliquam ante. Ut commodo malesuada orci luctus aliquam. Ut vel tellus mollis, gravida diam faucibus, vulputate sapien. Phasellus congue sed turpis ut sollicitudin. Maecenas malesuada libero quis mauris pellentesque euismod et in ante. Aenean scelerisque volutpat lacus id vestibulum. Nunc bibendum bibendum sapien, in congue est sollicitudin et. Aliquam aliquam consequat nibh, elementum vestibulum nisi semper rutrum. Quisque ut est euismod, aliquet orci sed, dictum tortor. Nunc fringilla, metus et euismod consequat, nulla tortor dapibus mauris, ut porta lorem metus a elit. Interdum et malesuada fames ac ante ipsum primis in faucibus.

Morbi sed augue non tellus feugiat sagittis eu eget dolor. Donec sed leo ac libero maximus molestie eget vel velit. Integer lacinia turpis sed sem convallis gravida. Morbi mi lectus, sollicitudin vitae cursus nec, ullamcorper ut diam. Ut egestas at elit at ultricies. Duis vel tortor ac urna egestas lobortis. Orci varius natoque penatibus et magnis dis parturient montes, nascetur ridiculus mus. Praesent nec ligula dictum, dapibus mauris at, placerat nisi. Aenean dapibus purus sit amet neque viverra, ut fermentum libero sodales. Donec aliquam suscipit erat, quis euismod arcu lobortis non. In volutpat suscipit arcu vitae imperdiet. Ut a lorem neque.

Fusce volutpat euismod convallis. Suspendisse justo mauris, sagittis id malesuada quis, vulputate in urna. Phasellus consectetur risus dui, et efficitur augue congue vitae. Curabitur non aliquet sem. Ut elementum ultricies tellus, eget sollicitudin ligula pretium nec. Proin et elit metus. Aliquam vehicula lectus dolor, vel fringilla dui feugiat a. Vestibulum ante ipsum primis in faucibus orci luctus et ultrices posuere cubilia Curae; Morbi ut interdum ipsum. Quisque non nisl ut ipsum pretium blandit. Vivamus sit amet mauris facilisis, semper dolor at, dignissim risus.

Maecenas suscipit dictum purus, quis vulputate massa finibus in. Curabitur ac cursus lectus. Lorem ipsum dolor sit amet, consectetur adipiscing elit. Phasellus nec augue sed mauris tincidunt laoreet. Donec maximus felis ligula, et semper lectus volutpat vel. Maecenas sit amet turpis ac sem molestie mollis sollicitudin eu massa. Maecenas et rutrum dui. Sed suscipit, neque posuere venenatis mollis, ligula ex molestie nibh, viverra posuere tellus felis et felis. Phasellus orci massa, pharetra a sem ut, pulvinar blandit felis. Pellentesque porta urna vitae pellentesque fringilla. Pellentesque eu risus ex. Sed auctor ut nibh in blandit.

Cras mattis nulla in nisl condimentum, in fermentum leo lobortis. Nullam luctus accumsan diam, vel tempus metus imperdiet at. Aenean eget aliquet dolor. Duis sit amet tellus laoreet, fringilla felis et, tristique justo. Nam ut tortor in est semper lobortis. Proin sagittis dapibus neque, non tincidunt erat scelerisque interdum. Maecenas faucibus, risus id dignissim tempus, mauris odio pellentesque sapien, at sagittis nibh turpis ac tortor. Suspendisse vulputate, elit et vehicula sodales, felis arcu faucibus ligula, sed mattis tellus ipsum vel est. Proin pretium rhoncus fermentum. Aliquam tristique consequat pulvinar.

Nulla porttitor, erat in facilisis consectetur, tellus magna tempus purus, ut bibendum elit felis at felis. Vivamus non viverra ligula. Sed nec lectus eget nunc egestas viverra. Nullam vulputate porta justo. Nam dolor ligula, tincidunt sed luctus quis, pellentesque nec massa. Proin in rhoncus lorem. Curabitur nisi leo, consectetur sit amet eros egestas, iaculis euismod erat. Suspendisse convallis felis sodales, sodales mauris ac, semper ante.

Nam semper mauris nec nunc condimentum, ut pellentesque lacus imperdiet. Donec eget dapibus magna. Etiam maximus nisl fringilla egestas blandit. Nunc gravida facilisis odio, id vulputate nisi gravida fermentum. Maecenas auctor nec nibh a dapibus. Praesent sit amet velit rhoncus, ornare elit eu, luctus felis. Proin ac malesuada nisl. Vestibulum dictum, sem vitae bibendum semper, turpis odio consequat nisi, ac congue mauris risus non sem. Curabitur eu egestas mauris. Etiam elementum condimentum varius. Aliquam malesuada elit ac magna efficitur, nec fermentum tortor iaculis. Maecenas et feugiat justo. Suspendisse potenti. Suspendisse pharetra tortor at turpis facilisis, ac scelerisque enim semper.

Mauris eu fringilla orci. Donec vitae quam at leo dictum blandit sed eget leo. Proin facilisis elit eget viverra ultricies. Nunc magna neque, molestie ac eros eu, pellentesque consectetur massa. Donec pellentesque libero ac nisl cursus, ut hendrerit nunc finibus. Vestibulum a odio a erat feugiat scelerisque nec ut augue. Vivamus pulvinar massa id nisl hendrerit elementum.

Mauris sed dapibus nibh. Praesent non blandit leo. Etiam urna quam, aliquam non nisl et, lacinia dapibus diam. Integer iaculis, dolor a egestas egestas, sapien arcu tempus urna, nec consectetur quam lacus in dui. Nam ac sodales quam, at malesuada libero. Praesent rutrum suscipit enim, eget vehicula velit interdum et. Aenean sit amet porttitor quam, ac hendrerit arcu. Cras fringilla erat vel erat accumsan imperdiet. Aenean semper euismod felis quis scelerisque.

Curabitur ac neque sem. Quisque lobortis nisi pharetra augue gravida, eget lacinia lacus aliquet. Lorem ipsum dolor sit amet, consectetur adipiscing elit. Quisque odio turpis, consectetur lobortis sem mollis, imperdiet convallis dui. Duis et risus ac nisl molestie elementum eget vitae lectus. Aenean luctus ultricies blandit. Duis scelerisque neque eros, non convallis leo congue ut. Nullam consectetur urna vel diam mattis, id cursus tellus bibendum. Maecenas ac elit et tortor finibus efficitur quis nec massa. In hac habitasse platea dictumst. Nulla facilisi. Curabitur tincidunt, odio a dapibus tincidunt, tortor mi pretium nunc, ac malesuada libero magna euismod quam. Pellentesque sit amet malesuada urna, sit amet mattis erat. Duis sollicitudin erat eros, nec consectetur turpis tempor sit amet.

Proin auctor mauris urna, at aliquam tortor vestibulum in. Mauris rutrum nibh eu dui pellentesque, eget vehicula nisi fringilla. Pellentesque quis nunc et mi aliquam aliquet. Fusce non rutrum libero. Sed auctor ligula sed libero porta, pretium finibus leo eleifend. Aliquam facilisis molestie mauris, sed fringilla tellus mattis non. Cras vulputate dignissim ex, dignissim porta dui venenatis ut. Class aptent taciti sociosqu ad litora torquent per conubia nostra, per inceptos himenaeos. Fusce vitae ornare diam. Nulla fermentum elementum mauris, et laoreet mauris aliquet rhoncus. Donec tempus fringilla est, non dapibus purus feugiat quis. Sed consectetur dolor ex, vel pulvinar justo ullamcorper ut. Pellentesque ac hendrerit metus. Vivamus lobortis elit sit amet pulvinar venenatis. Pellentesque tempor enim in fermentum porta.

Vivamus scelerisque risus elementum pellentesque venenatis. Etiam accumsan lacinia enim, eget gravida odio dictum quis. Sed nisl eros, pharetra ut tempor et, volutpat eu quam. Suspendisse id libero nec ante congue ornare quis vitae diam. Nulla luctus nisi eu dapibus malesuada. Suspendisse et commodo justo, id euismod odio. Interdum et malesuada fames ac ante ipsum primis in faucibus. Sed aliquam, purus sit amet mattis commodo, purus nisi finibus nunc, eu imperdiet elit erat et nunc.

Phasellus sit amet turpis quis enim laoreet molestie sit amet non odio. Praesent pretium eleifend pulvinar. Pellentesque dolor justo, scelerisque in tellus id, porttitor hendrerit felis. Ut faucibus ut risus quis venenatis. Phasellus ac dignissim mauris. Mauris eros dolor, finibus et tortor a, molestie dapibus diam. Sed eu nisi felis.

Aliquam erat volutpat. Etiam mollis dapibus ultricies. Suspendisse sapien nulla, suscipit eu gravida et, vestibulum eget dolor. Ut quis felis tortor. Vivamus ornare eros vel suscipit iaculis. Donec vestibulum lacus at eros posuere bibendum. Ut commodo, felis et ullamcorper suscipit, purus leo tincidunt leo, sed lacinia nunc mauris non arcu. Quisque euismod at purus a consectetur. Mauris non condimentum erat. Aenean tempus est in libero iaculis, id congue elit tristique. Donec volutpat quam vitae dolor porttitor scelerisque. Curabitur ut tellus cursus, ultricies dolor ut, malesuada dui.

Vivamus aliquet lectus eget diam tempor blandit. Aliquam erat volutpat. Curabitur at odio vehicula risus viverra faucibus. Morbi sapien nulla, rhoncus a magna ut, tincidunt varius orci. Class aptent taciti sociosqu ad litora torquent per conubia nostra, per inceptos himenaeos. Fusce nulla libero, varius a metus sed, molestie ornare est. Donec convallis massa a commodo lobortis. Donec convallis leo id vulputate vehicula. Maecenas ultricies venenatis est porttitor volutpat. Proin aliquam ipsum ut quam varius rhoncus. Pellentesque non consequat risus. Vivamus vulputate, justo vel sodales fermentum, nisi ipsum euismod orci, eget tincidunt quam augue quis eros. Proin mi lectus, porttitor sit amet convallis et, tristique eget sem. Donec in porttitor lectus.

Maecenas mattis, metus vitae lobortis condimentum, sem mi tempus tellus, id ornare nulla nisl nec leo. Interdum et malesuada fames ac ante ipsum primis in faucibus. Fusce eu accumsan neque, sit amet pharetra metus. Donec vestibulum convallis velit, ac mattis purus elementum sit amet. Duis nec ipsum maximus, auctor elit a, blandit urna. Sed in lorem nec justo congue posuere et ac velit. Suspendisse elementum bibendum risus in porttitor.

Proin pellentesque velit tellus, eu ornare erat cursus ac. Vivamus efficitur, ligula id rhoncus accumsan, lectus metus sollicitudin ligula, sed viverra elit nisi a magna. Integer aliquam pharetra neque, quis pretium tellus euismod eu. Aenean eleifend consectetur condimentum. Sed cursus ullamcorper sapien, et euismod arcu condimentum eget. In in ipsum ut ex dapibus malesuada. Integer vitae ultricies lacus. Morbi mauris ipsum, accumsan id orci at, finibus ultricies mauris. Nulla vitae justo euismod diam gravida vestibulum. Cras ullamcorper, mi eleifend rutrum luctus, nulla ante varius sapien, sed semper massa tellus ac urna. Orci varius natoque penatibus et magnis dis parturient montes, nascetur ridiculus mus. Phasellus scelerisque ex tellus. Aliquam erat volutpat. Etiam in tortor vel erat vehicula condimentum quis at est. Aliquam vel urna convallis libero iaculis fermentum at id justo. Mauris consequat mauris sit amet nulla feugiat, at blandit risus gravida.

Aliquam vestibulum quam non nibh lacinia molestie. Fusce pulvinar libero porttitor ornare laoreet. Suspendisse pharetra neque quis nibh condimentum, sed lacinia nisl ornare. Praesent ut odio id lorem scelerisque cursus nec a diam. Morbi laoreet risus urna. In quis lacinia quam. Curabitur dictum malesuada libero, eu fringilla velit pharetra in.

Vivamus et vulputate nisl. Phasellus congue augue nec nulla iaculis lobortis. Maecenas sit amet pellentesque est, ac posuere arcu. Sed condimentum consectetur ante non rutrum. Etiam vitae enim in nulla sodales lobortis eget id sem. Sed sollicitudin lectus ut dui cursus fermentum. Integer scelerisque sagittis est, quis dapibus felis placerat at. Etiam id risus ligula. Cras viverra nisi sed vestibulum accumsan. Donec risus orci, fringilla sit amet erat id, cursus auctor massa. Aenean feugiat diam vel metus fringilla elementum. Etiam ipsum enim, facilisis eu feugiat et, sagittis at sapien. Vestibulum ac erat neque.

Mauris blandit efficitur dignissim. Phasellus vel eleifend lectus. Pellentesque in velit sit amet dui iaculis lobortis eget ac nibh. Integer vel lorem vitae orci consequat rutrum. Nullam erat erat, egestas eu nibh vel, vehicula pharetra enim. Mauris convallis, urna sed sollicitudin cursus, purus nisi commodo dolor, malesuada porta nibh tortor eget nulla. Mauris a ligula vehicula, lobortis risus sit amet, porttitor eros.

Nulla id lacinia sapien. Proin faucibus aliquet nisi efficitur vulputate. Quisque bibendum mattis mi nec scelerisque. Sed feugiat, risus ut fringilla fermentum, nunc ligula dignissim ligula, at vestibulum dui arcu at augue. Donec volutpat non erat et ornare. Proin nisl metus, finibus a semper ornare, blandit quis libero. Cras non nisi eget sem viverra dictum. Aliquam vitae turpis risus. Donec tempor et enim in sagittis. Suspendisse ac justo a ligula vehicula vestibulum nec vitae diam. Aliquam ultricies dui non interdum faucibus. Integer ultricies viverra orci, non fermentum risus sodales ut. Donec facilisis nulla vel rutrum accumsan. Sed sit amet pulvinar nulla.

Phasellus vel vestibulum ante, sit amet mollis nunc. Maecenas ut nibh semper, lacinia magna a, mollis enim. Class aptent taciti sociosqu ad litora torquent per conubia nostra, per inceptos himenaeos. Maecenas eu dignissim nulla. Etiam a iaculis nisi. Mauris mollis ex lacus, non consequat ante dignissim sit amet. Etiam nec tortor non libero tempus congue non et justo. Suspendisse quis tempus neque. Quisque id sagittis augue. Ut diam mauris, fringilla vel lorem vitae, egestas posuere nibh. Aliquam ipsum lacus, rutrum quis porttitor malesuada, lacinia finibus dolor. Morbi vel dolor vel orci placerat auctor et sed quam. Aenean blandit magna nec dapibus eleifend. Cras magna metus, facilisis sed suscipit sed, pellentesque eget nulla.

Nunc rutrum erat a odio auctor dignissim. Sed bibendum sapien sed urna ultrices sollicitudin. In tellus nisi, maximus sed diam sed, rhoncus hendrerit sem. Donec eget lorem nec nunc commodo eleifend. Nunc sit amet felis vitae urna ultricies mattis. Sed in lorem eleifend, sagittis justo sed, consectetur augue. Curabitur quis magna vitae dolor tristique elementum ut eu dui. Maecenas vitae lectus libero.

Sed at tincidunt magna. Maecenas eget pellentesque dolor. Quisque eleifend erat id felis iaculis dictum. Mauris volutpat ante vel lacus scelerisque accumsan. Praesent rhoncus risus sit amet dui lobortis, tempor consectetur dui fermentum. Integer cursus et nisi vel ornare. Aliquam ac felis congue, feugiat ipsum nec, blandit justo. Aliquam et justo tincidunt, convallis nisl a, mattis tellus. Sed imperdiet nibh velit, sed faucibus nibh tincidunt vitae. Donec nisi erat, porttitor vitae sapien nec, condimentum posuere enim. Curabitur varius mauris quis sollicitudin ullamcorper. Mauris eget tristique ante. Vestibulum in semper urna. Fusce at mattis nibh, sit amet porttitor justo. Praesent congue interdum ex. Donec volutpat, mauris id dictum dictum, felis odio vulputate lorem, eget vestibulum orci.";
        }

        [HttpGet]
        [Route("/SayHello/{name}")]
        public async Task<string> SayHello(string name)
        {
            return $"Hello {name}";
        }

        /// <summary>
        /// This is a web api operation that uses the Little Tushy client to connect to localhost
        /// and call a controller action (hosted in this same web application using Little Tushy
        /// server) that just gets back a hello string that includes the name passed to the
        /// Service Controller
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<string> Get()
        {


            Stopwatch sw = new Stopwatch();
            sw.Start();
            for(int i = 0; i < 1000; i++)
            {
                    var tushyResult = (

                    await client.RequestAsync<string>
                    (
                        "HelloWorld", //controller
                        "LoremIpsum"
                    )
                    
                ).Result;

            }
            
            var tushy_elapsed = sw.Elapsed;
            
            sw.Reset();
            sw.Start();

            

            using (var httpClient = new HttpClient())
            {
                
                for(int i = 0; i < 1000; i++)
                {
                    var httpResult = await 
                    httpClient.GetStringAsync(new Uri("http://littletushysample.azurewebsites.net/api/HelloWorldTest/LoremIpsum"));
                }
            }

            var httpElapsed = sw.Elapsed;

            return $"Tushy: {tushy_elapsed} - Http: {httpElapsed}";


        }


    }
}
