clear
clc
close all

ccFuelConsumptionRoad1 = 4.0463;
ccFuelConsumptionRoad2 = 5.4251;
ccFuelConsumptionRoad3 = 3.8473;
ccFuelConsumptionRoad4 = 5.8678;
ccFuelConsumptionRoad5 = 3.8265;
normalizationType = 'count';
numberOfBins = 100;
totalNumberOfRoads = 5;
bruteforceRunsFilesPath = './Data/Benchmark data/benchmarkDataRoad';

for roadNumber = 1:totalNumberOfRoads
    roadSpecificFilePathBenchmark = strcat(bruteforceRunsFilesPath, num2str(roadNumber), '.txt');
    optimizationRoadSpecificFilePathBatch = strcat('./Results/BatchRunResult_Road',num2str(roadNumber),'.txt');
    batchRunFuelConsumptionData = load(roadSpecificFilePathBenchmark);
    fileID = fopen(optimizationRoadSpecificFilePathBatch,'r');
    rawData = textscan(fileID,'%f %f %*[^\n]','MultipleDelimsAsOne',1,'CommentStyle','%%');
    
    optimizationRunsFuelConsumptionData = [rawData{1,1}, rawData{1,2}];
    optimizationRunsFuelConsumptionData(:,2) = 1./optimizationRunsFuelConsumptionData(:,2);
    minFuelConsumptionPlotLength = [0 59]; % Only for plotting the dashed line over two histograms
    %% Plot sectiom
    figure('units','normalized','outerposition',[0 0.05 0.95 0.95]);
    subplot(2,1,1)
    histogramHandle = histogram(batchRunFuelConsumptionData(:,2), 50, 'Normalization', normalizationType,...
        'edgecolor','none','edgealpha',0.05,'BinWidth',0.0095);
    binLimits = histogramHandle.BinLimits;
    hold on
    [maxFrequency, maxIndex] = max(histogramHandle.BinCounts);
    binWidth = 0.5*histogramHandle.BinWidth;
    
    if(roadNumber == 1)
        [~,indexOfClosestBinToCC] = min(abs(histogramHandle.BinEdges - ccFuelConsumptionRoad1));
        ccBinFrequency = histogramHandle.Values(indexOfClosestBinToCC);
        tempData = ccFuelConsumptionRoad1 * ones(ccBinFrequency,1);
        histogram(tempData, 1, 'Normalization', normalizationType,...
            'BinWidth', binWidth,'FaceAlpha',1,'FaceColor','r','EdgeColor','r');
        minFuelConsumptionPlotLength = [0 59];
    elseif(roadNumber == 2)
        [~,indexOfClosestBinToCC] = min(abs(histogramHandle.BinEdges - ccFuelConsumptionRoad2));
        ccBinFrequency = histogramHandle.Values(indexOfClosestBinToCC);
        tempData = ccFuelConsumptionRoad2 * ones(ccBinFrequency,1);
        histogram(tempData, 1, 'Normalization', normalizationType, ...
            'BinWidth', binWidth,'FaceAlpha',1,'FaceColor','r','EdgeColor','r');
        minFuelConsumptionPlotLength = [0 108];
    elseif(roadNumber == 3)
        [~,indexOfClosestBinToCC] = min(abs(histogramHandle.BinEdges - ccFuelConsumptionRoad3));
        ccBinFrequency = histogramHandle.Values(indexOfClosestBinToCC);
        tempData = ccFuelConsumptionRoad3 * ones(ccBinFrequency,1);
        histogram(tempData, 1, 'Normalization', normalizationType, ...
            'BinWidth', binWidth,'FaceAlpha',1,'FaceColor','r','EdgeColor','r');
        minFuelConsumptionPlotLength = [0 72];
    elseif(roadNumber == 4)
        [~,indexOfClosestBinToCC] = min(abs(histogramHandle.BinEdges - ccFuelConsumptionRoad4));
        ccBinFrequency = histogramHandle.Values(indexOfClosestBinToCC);
        tempData = ccFuelConsumptionRoad4 * ones(ccBinFrequency,1);
        histogram(tempData, 1, 'Normalization', normalizationType, ...
            'BinWidth', binWidth,'FaceAlpha',1,'FaceColor','r','EdgeColor','r');
        minFuelConsumptionPlotLength = [0 83];
    elseif(roadNumber == 5)
        [~,indexOfClosestBinToCC] = min(abs(histogramHandle.BinEdges - ccFuelConsumptionRoad5));
        ccBinFrequency = histogramHandle.Values(indexOfClosestBinToCC);
        tempData = ccFuelConsumptionRoad5 * ones(ccBinFrequency,1);
        histogram(tempData, 1, 'Normalization', normalizationType, ...
            'BinWidth', binWidth,'FaceAlpha',1,'FaceColor','r','EdgeColor','r');
        minFuelConsumptionPlotLength = [0 83];
    end
    hold on
    minFuelConsumption = min(batchRunFuelConsumptionData(:,2)) * ones(maxFrequency,1);
    titleHandle = title(strcat('Road profile ',num2str(roadNumber)),'FontWeight','Normal',...
        'Units', 'normalized', 'Position', [0.93, 1.03, 0]);
    
    xlabel('Fuel consumption (L)','FontName','Times New Roman');
    ylabel('Frequency','FontName','Times New Roman')
    set(gca, 'FontSize',24,'FontName','Times New Roman')
    grid on
    
    subplot(2,1,2)
    GA_HistogramHandle = histogram(optimizationRunsFuelConsumptionData(:,2), 50, ...
        'Normalization', normalizationType,'BinWidth', 0.0095,'BinLimits', binLimits, ...
        'FaceColor',[0.850, 0.325, 0.098],'edgecolor','none','edgealpha',0.05);
    maxGAHistogramPlotLimit = ylim;
    hold on
    absoluteMinPlotHandle = plot([minFuelConsumption(1)-0.0075 , minFuelConsumption(1)-0.0075], minFuelConsumptionPlotLength,'--k',...
        'linewidth',2);
    ylim([0 maxGAHistogramPlotLimit(2)]);
    
    xlabel('Fuel consumption (L)','FontName','Times New Roman');
    ylabel('Frequency','FontName','Times New Roman')
    set(gca,'Clipping','off')
    set(gca, 'FontSize',24,'FontName','Times New Roman')
    grid on
    %
    set(gcf,'renderer','painters')
end
%print(gcf,'-depsc2','FuelConsumptionHistogramRoad5.eps')