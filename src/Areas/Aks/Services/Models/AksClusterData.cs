// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

#nullable disable

using System.Text.Json.Serialization;

namespace AzureMcp.Areas.Aks.Services
{
    /// <summary>
    /// A class representing the ContainerServiceManagedCluster data model.
    /// Managed cluster.
    /// </summary>
    internal sealed class AksClusterData : TrackedResourceData
    {
        /// <summary> The managed cluster SKU. </summary>
        [JsonPropertyName("sku")]
        public string Sku { get; set; }
        /// <summary> The extended location of the Virtual Machine. </summary>
        [JsonPropertyName("extendedLocation")]
        public ExtendedLocation ExtendedLocation { get; set; }
        /// <summary> The identity of the managed cluster, if configured. </summary>
        [WirePath("identity")]
        public ManagedClusterIdentity ClusterIdentity { get; set; }
        /// <summary> The current provisioning state. </summary>
        [WirePath("properties.provisioningState")]
        public string ProvisioningState { get; }
        /// <summary> The Power State of the cluster. </summary>
        internal ContainerServicePowerState PowerState { get; }
        /// <summary> Tells whether the cluster is Running or Stopped. </summary>
        [WirePath("properties.powerState.code")]
        public ContainerServiceStateCode? PowerStateCode
        {
            get => PowerState?.Code;
        }

        /// <summary> The max number of agent pools for the managed cluster. </summary>
        [WirePath("properties.maxAgentPools")]
        public int? MaxAgentPools { get; }
        /// <summary> Both patch version &lt;major.minor.patch&gt; (e.g. 1.20.13) and &lt;major.minor&gt; (e.g. 1.20) are supported. When &lt;major.minor&gt; is specified, the latest supported GA patch version is chosen automatically. Updating the cluster with the same &lt;major.minor&gt; once it has been created (e.g. 1.14.x -&gt; 1.14) will not trigger an upgrade, even if a newer patch version is available. When you upgrade a supported AKS cluster, Kubernetes minor versions cannot be skipped. All upgrades must be performed sequentially by major version number. For example, upgrades between 1.14.x -&gt; 1.15.x or 1.15.x -&gt; 1.16.x are allowed, however 1.14.x -&gt; 1.16.x is not allowed. See [upgrading an AKS cluster](https://docs.microsoft.com/azure/aks/upgrade-cluster) for more details. </summary>
        [WirePath("properties.kubernetesVersion")]
        public string KubernetesVersion { get; set; }
        /// <summary> If kubernetesVersion was a fully specified version &lt;major.minor.patch&gt;, this field will be exactly equal to it. If kubernetesVersion was &lt;major.minor&gt;, this field will contain the full &lt;major.minor.patch&gt; version being used. </summary>
        [WirePath("properties.currentKubernetesVersion")]
        public string CurrentKubernetesVersion { get; }
        /// <summary> This cannot be updated once the Managed Cluster has been created. </summary>
        [WirePath("properties.dnsPrefix")]
        public string DnsPrefix { get; set; }
        /// <summary> This cannot be updated once the Managed Cluster has been created. </summary>
        [WirePath("properties.fqdnSubdomain")]
        public string FqdnSubdomain { get; set; }
        /// <summary> The FQDN of the master pool. </summary>
        [WirePath("properties.fqdn")]
        public string Fqdn { get; }
        /// <summary> The FQDN of private cluster. </summary>
        [WirePath("properties.privateFQDN")]
        public string PrivateFqdn { get; }
        /// <summary> The Azure Portal requires certain Cross-Origin Resource Sharing (CORS) headers to be sent in some responses, which Kubernetes APIServer doesn't handle by default. This special FQDN supports CORS, allowing the Azure Portal to function properly. </summary>
        [WirePath("properties.azurePortalFQDN")]
        public string AzurePortalFqdn { get; }
        /// <summary> The agent pool properties. </summary>
        [WirePath("properties.agentPoolProfiles")]
        public IList<ManagedClusterAgentPoolProfile> AgentPoolProfiles { get; }
        /// <summary> The profile for Linux VMs in the Managed Cluster. </summary>
        [WirePath("properties.linuxProfile")]
        public ContainerServiceLinuxProfile LinuxProfile { get; set; }
        /// <summary> The profile for Windows VMs in the Managed Cluster. </summary>
        [WirePath("properties.windowsProfile")]
        public ManagedClusterWindowsProfile WindowsProfile { get; set; }
        /// <summary> Information about a service principal identity for the cluster to use for manipulating Azure APIs. </summary>
        [WirePath("properties.servicePrincipalProfile")]
        public ManagedClusterServicePrincipalProfile ServicePrincipalProfile { get; set; }
        /// <summary> The profile of managed cluster add-on. </summary>
        [WirePath("properties.addonProfiles")]
        public IDictionary<string, ManagedClusterAddonProfile> AddonProfiles { get; }
        /// <summary> See [use AAD pod identity](https://docs.microsoft.com/azure/aks/use-azure-ad-pod-identity) for more details on AAD pod identity integration. </summary>
        [WirePath("properties.podIdentityProfile")]
        public ManagedClusterPodIdentityProfile PodIdentityProfile { get; set; }
        /// <summary> The OIDC issuer profile of the Managed Cluster. </summary>
        [WirePath("properties.oidcIssuerProfile")]
        public ManagedClusterOidcIssuerProfile OidcIssuerProfile { get; set; }
        /// <summary> The name of the resource group containing agent pool nodes. </summary>
        [WirePath("properties.nodeResourceGroup")]
        public string NodeResourceGroup { get; set; }
        /// <summary> Whether to enable Kubernetes Role-Based Access Control. </summary>
        [WirePath("properties.enableRBAC")]
        public bool? EnableRbac { get; set; }
        /// <summary> The support plan for the Managed Cluster. If unspecified, the default is 'KubernetesOfficial'. </summary>
        [WirePath("properties.supportPlan")]
        public KubernetesSupportPlan? SupportPlan { get; set; }
        /// <summary> (DEPRECATED) Whether to enable Kubernetes pod security policy (preview). PodSecurityPolicy was deprecated in Kubernetes v1.21, and removed from Kubernetes in v1.25. Learn more at https://aka.ms/k8s/psp and https://aka.ms/aks/psp. </summary>
        [WirePath("properties.enablePodSecurityPolicy")]
        public bool? EnablePodSecurityPolicy { get; set; }
        /// <summary> The network configuration profile. </summary>
        [WirePath("properties.networkProfile")]
        public ContainerServiceNetworkProfile NetworkProfile { get; set; }
        /// <summary> The Azure Active Directory configuration. </summary>
        [WirePath("properties.aadProfile")]
        public ManagedClusterAadProfile AadProfile { get; set; }
        /// <summary> The auto upgrade configuration. </summary>
        [WirePath("properties.autoUpgradeProfile")]
        public ManagedClusterAutoUpgradeProfile AutoUpgradeProfile { get; set; }
        /// <summary> Settings for upgrading a cluster. </summary>
        internal ClusterUpgradeSettings UpgradeSettings { get; set; }
        /// <summary> Settings for overrides. </summary>
        [WirePath("properties.upgradeSettings.overrideSettings")]
        public UpgradeOverrideSettings UpgradeOverrideSettings
        {
            get => UpgradeSettings is null ? default : UpgradeSettings.OverrideSettings;
            set
            {
                if (UpgradeSettings is null)
                    UpgradeSettings = new ClusterUpgradeSettings();
                UpgradeSettings.OverrideSettings = value;
            }
        }

        /// <summary> Parameters to be applied to the cluster-autoscaler when enabled. </summary>
        [WirePath("properties.autoScalerProfile")]
        public ManagedClusterAutoScalerProfile AutoScalerProfile { get; set; }
        /// <summary> The access profile for managed cluster API server. </summary>
        [WirePath("properties.apiServerAccessProfile")]
        public ManagedClusterApiServerAccessProfile ApiServerAccessProfile { get; set; }
        /// <summary> This is of the form: '/subscriptions/{subscriptionId}/resourceGroups/{resourceGroupName}/providers/Microsoft.Compute/diskEncryptionSets/{encryptionSetName}'. </summary>
        [WirePath("properties.diskEncryptionSetID")]
        public ResourceIdentifier DiskEncryptionSetId { get; set; }
        /// <summary> Identities associated with the cluster. </summary>
        [WirePath("properties.identityProfile")]
        public IDictionary<string, ContainerServiceUserAssignedIdentity> IdentityProfile { get; }
        /// <summary> Private link resources associated with the cluster. </summary>
        [WirePath("properties.privateLinkResources")]
        public IList<ContainerServicePrivateLinkResourceData> PrivateLinkResources { get; }
        /// <summary> If set to true, getting static credentials will be disabled for this cluster. This must only be used on Managed Clusters that are AAD enabled. For more details see [disable local accounts](https://docs.microsoft.com/azure/aks/managed-aad#disable-local-accounts-preview). </summary>
        [WirePath("properties.disableLocalAccounts")]
        public bool? DisableLocalAccounts { get; set; }
        /// <summary> Configurations for provisioning the cluster with HTTP proxy servers. </summary>
        [WirePath("properties.httpProxyConfig")]
        public ManagedClusterHttpProxyConfig HttpProxyConfig { get; set; }
        /// <summary> Security profile for the managed cluster. </summary>
        [WirePath("properties.securityProfile")]
        public ManagedClusterSecurityProfile SecurityProfile { get; set; }
        /// <summary> Storage profile for the managed cluster. </summary>
        [WirePath("properties.storageProfile")]
        public ManagedClusterStorageProfile StorageProfile { get; set; }
        /// <summary> Allow or deny public network access for AKS. </summary>
        [WirePath("properties.publicNetworkAccess")]
        public ContainerServicePublicNetworkAccess? PublicNetworkAccess { get; set; }
        /// <summary> Workload Auto-scaler profile for the managed cluster. </summary>
        [WirePath("properties.workloadAutoScalerProfile")]
        public ManagedClusterWorkloadAutoScalerProfile WorkloadAutoScalerProfile { get; set; }
        /// <summary> Azure Monitor addon profiles for monitoring the managed cluster. </summary>
        internal ManagedClusterAzureMonitorProfile AzureMonitorProfile { get; set; }
        /// <summary> Metrics profile for the Azure Monitor managed service for Prometheus addon. Collect out-of-the-box Kubernetes infrastructure metrics to send to an Azure Monitor Workspace and configure additional scraping for custom targets. See aka.ms/AzureManagedPrometheus for an overview. </summary>
        [WirePath("properties.azureMonitorProfile.metrics")]
        public ManagedClusterMonitorProfileMetrics AzureMonitorMetrics
        {
            get => AzureMonitorProfile is null ? default : AzureMonitorProfile.Metrics;
            set
            {
                if (AzureMonitorProfile is null)
                    AzureMonitorProfile = new ManagedClusterAzureMonitorProfile();
                AzureMonitorProfile.Metrics = value;
            }
        }

        /// <summary> Service mesh profile for a managed cluster. </summary>
        [WirePath("properties.serviceMeshProfile")]
        public ServiceMeshProfile ServiceMeshProfile { get; set; }
        /// <summary> The resourceUID uniquely identifies ManagedClusters that reuse ARM ResourceIds (i.e: create, delete, create sequence). </summary>
        [WirePath("properties.resourceUID")]
        public ResourceIdentifier ResourceId { get; }
    }
}
